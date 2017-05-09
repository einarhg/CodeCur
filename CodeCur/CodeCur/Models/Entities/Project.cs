using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.Entities
{
    public class Project
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Deleted { get; set; }
        public DateTime DateCreated { get; set; }
    }
}