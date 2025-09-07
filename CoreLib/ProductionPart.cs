using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class ProductionPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TimeStamp { get; set; }



        public Hashtable getDataAsHashtable()
        {
            Hashtable dataAsHashtable = new Hashtable();
            dataAsHashtable["id"] = Id;
            dataAsHashtable["name"] = Name;
            dataAsHashtable["timestamp"] = TimeStamp.ToString("dd-MM-yyyy HH:mm:ss,fff");

            return dataAsHashtable;
        }
    }
}
