using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
}