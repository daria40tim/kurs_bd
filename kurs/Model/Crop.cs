using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs.Model
{
    public class Crop
    {
        public int Crop_id { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }
        public Culture Cult { get; set; }
    }
}
