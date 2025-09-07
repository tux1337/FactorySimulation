using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public static class KPI
    {

        public static List<ProductionPartKPI> calcTimeSpan(List<ProductionPart> partList)
        {
            List<ProductionPartKPI> partKPI = new List<ProductionPartKPI>();

            partList = partList.OrderBy(x => x.TimeStamp).ToList();

            for (int i = 0; i < partList.Count; i++)
            {
                int vorigesTeil = i - 1;
                if (vorigesTeil >= 0)
                {
                    TimeSpan timespan = partList[i].TimeStamp.Subtract(partList[vorigesTeil].TimeStamp);

                    ProductionPartKPI part = new ProductionPartKPI(partList[i]);
                    part.TimeSpanToPreviousPart = timespan;
                    partKPI.Add(part);
                }
            }

            return partKPI;
        }

        public static int calculateAvgFertigungstakt(List<ProductionPartKPI> partList)
        {
            double sumDauer = 0;
            int entrys = 0;

            foreach (ProductionPartKPI part in partList)
            {
                if(part.TimeSpanToPreviousPart != null)
                {
                    sumDauer += part.TimeSpanToPreviousPart.Value.TotalMilliseconds;
                    entrys++;
                }
            }

            double avgDauer = sumDauer / entrys;

            if(entrys <= 1)
            {
                avgDauer = sumDauer;
            }

            return (int)Math.Round(avgDauer);
        }
        public static int calculateMinFertigungstakt(List<ProductionPartKPI> partList)
        {
            double minDauer = 0;
            bool first = true;


            foreach (ProductionPartKPI part in partList)
            {
                if (part.TimeSpanToPreviousPart != null)
                {
                    double diff = part.TimeSpanToPreviousPart.Value.TotalMilliseconds;
                    if (diff < minDauer || first)
                        minDauer = diff;

                    first = false;
                }
            }
            return (int)Math.Round(minDauer);
        }

        public static int calculateMaxFertigungstakt(List<ProductionPartKPI> partList)
        {
            double maxDauer = 0;

            foreach (ProductionPartKPI part in partList)
            {
                if (part.TimeSpanToPreviousPart != null)
                {
                    double diff = part.TimeSpanToPreviousPart.Value.TotalMilliseconds;
                    if (diff > maxDauer)
                        maxDauer = diff;
                }
            }

            return (int)Math.Round(maxDauer);
        }


        public static int lastPartSecondsElapsed(List<ProductionPartKPI> partList)
        {
            return ((int)Math.Round(partList[partList.Count-1].TimeStamp.Subtract(DateTime.Now).TotalSeconds) * (-1));
        }


    }
}
