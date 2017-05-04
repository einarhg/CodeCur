using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class EditorViewModel
    {
        public File File { get; set; }
        public string ProjectName { get; set; }
        public string UserName { get; set; }
    }
}