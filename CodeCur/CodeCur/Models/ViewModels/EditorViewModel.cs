using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class EditorViewModel
    {
        public File file { get; set; }
        public string projectName { get; set; }
        public string username { get; set; }
    }
}