using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kurs.Model
{
    public class Plant:Culture
    {
        public int Plant_id { get; set; }
        public Stage Stage_id { get; set; }
        public int Count { get; set; }
        public int House_id { get; set; }
       // public int Culture { get; set; }
        public List<Card> Cards_of_plant { get; set; }
    }
}
