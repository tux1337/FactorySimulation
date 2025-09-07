using Amazon;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class S3Wrapper
    {
        //Configuration
        private Vault vault = new Vault();
        //MinIO ServiceURL
        private string serviceUrl = "https://url.domain:9000/";
        private string bucketName = "factorysimulation";

        private AmazonS3Config s3config;
        private AmazonS3Client s3client;


        //Singleton forces only 1 instance
        private S3Wrapper()
        {
            this.s3config = new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                ForcePathStyle = true,
            };

            this.updateS3Cred();
        }

        private void updateS3Cred()
        {
            Log.write("Update S3 Credentials from Vault.", false);
            vault.UpdateSecret();
            this.s3client = new AmazonS3Client(vault.getAccessKey(), vault.getSecretKey(), s3config);
        }

        private static S3Wrapper instance;
        public static S3Wrapper create()
        {
            if (S3Wrapper.instance == null)
            {
                instance = new S3Wrapper();
            }

            return instance;
        }

        public void printS3Details(int? partID = null)
        {
            Log.write("S3 with Access Key: " + vault.getAccessKey() + "; Secret Key: " + vault.getSecretKey(), partID);
        }

        public bool uploadFile(String fileName, ProductionPart part, String ContentBody = "")
        {
            PutObjectRequest putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileName,
                FilePath = fileName,
                ContentType = "text/plain",
                //needed for ONTAP, minio don't like the hash
                //ContentBody = ContentBody,
                //ChecksumSHA256 = SHA256Manager.sha256(ContentBody),
            };

            Hashtable metadata = part.getDataAsHashtable();
            //Add Metadaten for every request
            foreach (string key in metadata.Keys)
            {
                putRequest.Metadata.Add(key, Convert.ToString(metadata[key]));
            }

            try
            {
                Task<PutObjectResponse> response = s3client.PutObjectAsync(putRequest);

                printS3Details(part.Id);

                response.Wait();

                if (response.Result.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.writeError("Error in S3 communication: " + ex.Message, part.Id);
                Log.writeError("Upload of " + fileName + " failed.", part.Id);
                updateS3Cred();
            }
            return false;
        }


        /// <summary>
        /// Search for Objects newer than date
        /// </summary>
        /// <param name="date">search for objects newer than this date</param>
        /// <returns>List of string with object keys that are newer</returns>
        public List<String> getAllObjectsNewerDate(DateTime date)
        {
            try
            {
                string continuationToken = null;

                List<String> objectKeys = new List<String>();

                do
                {
                    ListObjectsV2Request s3request = new ListObjectsV2Request
                    {
                        BucketName = bucketName,
                        ContinuationToken = continuationToken,
                    };

                    Task<ListObjectsV2Response> s3response = s3client.ListObjectsV2Async(s3request);
                    s3response.Wait();

                    foreach (S3Object obj in s3response.Result.S3Objects)
                    {
                        if (obj.LastModified >= date)
                        {
                            objectKeys.Add(obj.Key);
                        }
                    }

                    continuationToken = s3response.Result.IsTruncated ? s3response.Result.NextContinuationToken : null;

                } while (continuationToken != null);

                return objectKeys;
            }
            catch (Exception ex)
            {
                Log.writeError("Error in S3 communication: " + ex.Message, null);
                Log.writeError("Method getAllObjectsNewerDate()", null);
                updateS3Cred();

                //retry
                Thread.Sleep(100);
                return getAllObjectsNewerDate(date);
            }
        }


        public List<ProductionPart> s3ObjectToProductionPart(List<String> objects)
        {
            try
            {
                List<ProductionPart> productionParts = new List<ProductionPart>();

                foreach (String obj in objects)
                {
                    //ProductionPart Details saved as Metadata
                    GetObjectMetadataRequest metadataRequest = new GetObjectMetadataRequest
                    {
                        BucketName = bucketName,
                        Key = obj
                    };

                    Task<GetObjectMetadataResponse> metadataResponse = s3client.GetObjectMetadataAsync(metadataRequest);

                    metadataResponse.Wait();

                    ProductionPart part = new ProductionPart()
                    {
                        Id = Convert.ToInt32(metadataResponse.Result.Metadata["id"]),
                        Name = Convert.ToString(metadataResponse.Result.Metadata["name"]),
                        TimeStamp = DateTime.ParseExact(metadataResponse.Result.Metadata["timestamp"], "dd-MM-yyyy HH:mm:ss,fff", System.Globalization.CultureInfo.InvariantCulture),
                    };

                    productionParts.Add(part);
                }

                return productionParts;
            }
            catch (Exception ex)
            {
                Log.writeError("Error in S3 communcation: " + ex.Message, null);
                Log.writeError("Method getAllObjectsNewerDate()", null);
                updateS3Cred();

                //retry
                Thread.Sleep(100);
                return s3ObjectToProductionPart(objects);
            }
        }
    }
}
