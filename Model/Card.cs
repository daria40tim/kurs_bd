using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kurs.Model
{
    public class Card
    {
        public int Card_id { get; set; }
        public string Title { get; set; }
        public string Stage { get; set; }
        public int Cult_id { get; set; }
        public float Optimal { get; set; }
        public float Tolerance { get; set; }
        public float Limit_deviation { get; set; }
    }
}
