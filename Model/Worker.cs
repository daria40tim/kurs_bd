using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kurs.Model
{
    public class Worker
    {
        public int Worker_id { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public Greenhouse House_id { get; set; }
    }
}
