using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeCur.Models.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<Project> Projects { get; set; }
    }

    public class ProjectDetailsViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public int ProjectID { get; set; }
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

    public class CreateFileViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string FileType { get; set; }

        [Required]
        public int ProjectID { get; set; }
    }

    public class FileAndProjectViewModel
    {
        public CreateFileViewModel FileModel { get; set; }
        public ProjectDetailsViewModel ProjectModel { get; set; }
    }
}