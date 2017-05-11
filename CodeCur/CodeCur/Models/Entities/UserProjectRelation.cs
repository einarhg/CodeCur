using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.Entities
{
    /// <summary>
    /// Relations between users and projects, keeps track of the projects a user has access to.
    /// </summary>
    public class UserProjectRelation
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ProjectID { get; set; }
        public bool Deleted { get; set; }
    }
}