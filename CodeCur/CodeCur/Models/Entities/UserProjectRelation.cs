using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.Entities
{
    public class UserProjectRelation
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public int ProjectID { get; set; }
    }
}