using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class ProjectViewModel
    {
        public List<File> files { get; set; }
        public string username { get; set; }
        public string projectName { get; set; }
    }
}