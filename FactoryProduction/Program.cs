using System.Diagnostics;
using System.Text;
using System.Text.Json;
using CoreLib;


internal class Program
{
    private static void Main(string[] args)
    {
        S3Wrapper s3 = S3Wrapper.create();

        Log.write("Simulation of a production line.", null);
        Log.write("Line starts production....", null);

        Log.linebreak();

        int TeileID = 1;

        while (true)
        {
            Stopwatch stopWatchProduktionGesamt = Stopwatch.StartNew();

            Log.write(" production in process.", TeileID);
            //warte auf Produktion
            Thread.Sleep(2500);

            ProductionPart part = new ProductionPart() { Id = TeileID, Name = "Alpenmilch", TimeStamp = DateTime.Now };

            Log.write("sucessfully produced.", TeileID);

            Log.write("start documentation.", TeileID);
            
            Stopwatch stopwatchDoku = Stopwatch.StartNew();

            //lokale Dokumentation
            string fileName = TeileID.ToString() + ".txt";
            string content = JsonSerializer.Serialize(part);
            File.WriteAllText(fileName, content);

            //upload to s3
            bool s3status = s3.uploadFile(fileName, part);    

            //lokale Kopie löschen bei Erfolg
            if(s3status && File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            //Stoppe Produktion bei Fehler in Dokumenation
            if(!s3status)
            {
                Log.write("Repeat the upload", TeileID);
                bool s3statusRetry = s3.uploadFile(fileName, part);

                if (s3statusRetry && File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                if (!s3statusRetry)
                {
                    Log.writeError("Error in documentation, production line stopped.", TeileID);
                    break;
                }
            }

            stopwatchDoku.Stop();
            Log.write("sucessufully documented. (" + stopwatchDoku.ElapsedMilliseconds + "ms)", TeileID);

            stopWatchProduktionGesamt.Stop();
            Log.write("Production finished. (" + stopWatchProduktionGesamt.ElapsedMilliseconds + "ms)", TeileID);

            Log.linebreak();

            TeileID++;
        }

    }
}