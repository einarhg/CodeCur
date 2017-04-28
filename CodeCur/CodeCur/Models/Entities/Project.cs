using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.Entities
{
    public class Project
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public List<File> files { get; set; }
    }
}