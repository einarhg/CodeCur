﻿using CodeCur.Models.Entities;
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
        public string ProjectName { get; set; }
    }

    public class CreateProjectViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} cannot be longer then 30 characters" )]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string ProjectType { get; set; }
    }

    public class CreateFileViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The {0} cannot be longer then 30 characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string FileType { get; set; }

        [Required]
        public int ProjectID { get; set; }
    }

    public class ShareProjectViewModel
    {
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        public int ProjectID { get; set; }
    }

    public class FileAndProjectViewModel
    {
        public CreateFileViewModel FileModel { get; set; }
        public ProjectDetailsViewModel ProjectModel { get; set; }
    }
}