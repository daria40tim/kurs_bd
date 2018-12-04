using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kurs.Model
{
    public class Plant:Culture
    {
        public int Plant_id { get; set; }
        public string Stage { get; set; }
        public int Count { get; set; }
        public List<Card> Cards_of_plant { get; set; }
    }
}
