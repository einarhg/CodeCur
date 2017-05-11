using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.Entities
{
    /// <summary>
    /// Project entity class, keeps track of information about projects.
    /// </summary>
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