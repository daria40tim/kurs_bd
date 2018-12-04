using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kurs.Model
{
    public class Task
    {
        public int Task_id { get; set; }
        public string Text { get; set; }
        public Worker Worker_id { get; set; }
        public DateTime App_date { get; set; }
        public DateTime End_date { get; set; }
    }
}
