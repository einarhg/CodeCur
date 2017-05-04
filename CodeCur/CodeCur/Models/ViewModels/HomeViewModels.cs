using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeCur.Models.ViewModels
{
    public class ListProjectsViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
    }

    public class CreateProjectViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string ProjectType { get; set; }
    }
}