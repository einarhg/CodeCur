using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Project> projects { get; set; }
        public string username { get; set; }
    }
}