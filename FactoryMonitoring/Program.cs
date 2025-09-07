using System.Diagnostics;
using System.Text.Json;
using CoreLib;

internal class Program
{
    private static void Main(string[] args)
    {
        S3Wrapper s3 = S3Wrapper.create();

        while (true)
        {

            Thread.Sleep(2000);

            Console.Clear();
            Log.write("Production Monitoring", null);
            Log.linebreak();

            //S3 Access / Secret Key
            s3.printS3Details();
            Log.linebreak();
            Log.linebreak();

            //KPI last minute
            DateTime oneMinuteAgo = DateTime.Now.AddMinutes(-1);
            List<ProductionPart> parts = s3.s3ObjectToProductionPart(s3.getAllObjectsNewerDate(oneMinuteAgo));

            //calculate Timespan
            List<ProductionPartKPI> partKPI = KPI.calcTimeSpan(parts);

            if(parts.Count == 0)
            {
                Log.writeError("Production stopped.", null);
                Log.printSadFace();
            }
            else if (parts.Count == 1)
            {
                Log.writeError("Production starts...", null);
                Log.printSadFace();
            }
            else
            {
                int maxDauer = KPI.calculateMaxFertigungstakt(partKPI);
                int minDauer = KPI.calculateMinFertigungstakt(partKPI);
                int avgDauer = KPI.calculateAvgFertigungstakt(partKPI);

                //When was the last part produced?
                int lastPartSecondsElapsed = KPI.lastPartSecondsElapsed(partKPI);

                Log.write("KPI takt time", true);
                Log.write("min: " + minDauer.ToString() + "ms   avg:" + avgDauer.ToString() + "ms   max: " + maxDauer.ToString() + "ms", true);

                if(lastPartSecondsElapsed < 3)
                    Log.write("Last part "+ lastPartSecondsElapsed.ToString() +" second(s) ago.",true);
                else
                    Log.writeError("Last part "+ lastPartSecondsElapsed.ToString() +" seconds ago.", null);
                
                if (avgDauer <= 3000 && lastPartSecondsElapsed <= 3)
                {
                    Log.printHappyFace();
                }
                else 
                {
                    Log.printSadFace();
                }
            }
            
            Log.linebreak();
            
            //Detail last 5 parts
            Log.write("Last 5 parts: ", true);
            int i = 1;
            foreach (ProductionPartKPI part in partKPI.OrderByDescending(x => x.TimeStamp))
            {
                Log.write("ID: " + part.Id.ToString() +"; Manufacturing time: " + part.TimeSpanToPreviousPart.Value.TotalMilliseconds.ToString() + "ms" , true);

                //abort after fith part
                if (i >= 5)
                    break;
                i++;
            }
        }
    }
}