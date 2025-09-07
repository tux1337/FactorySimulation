using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class ProductionPartKPI:ProductionPart
    {
        public ProductionPartKPI(ProductionPart part) 
        { 
            base.Id = part.Id;
            base.Name = part.Name;
            base.TimeStamp = part.TimeStamp;
        }


        public TimeSpan? TimeSpanToPreviousPart = null;
    }
}
