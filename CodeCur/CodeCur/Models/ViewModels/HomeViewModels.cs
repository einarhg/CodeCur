using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CodeCur.Models.ViewModels
{
    /// <summary>
    /// Keeps track of information for the users' home page.
    /// </summary>
    public class ProjectViewModel
    {
        public List<Project> Projects { get; set; }
        public List<string> Owners { get; set; }
    }
    /// <summary>
    /// Keeps track of project details.
    /// </summary>
    public class ProjectDetailsViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
    /// <summary>
    /// Validation for creating a project.
    /// </summary>
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
    /// <summary>
    /// Validation for creating a file.
    /// </summary>
    public class CreateFileViewModel
    {
        [Required]
        [StringLength(25, ErrorMessage = "The {0} cannot be longer then 25 characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string FileType { get; set; }

        [Required]
        public int ProjectID { get; set; }
    }
    /// <summary>
    /// Validation for sharing a project.
    /// </summary>
    public class ShareProjectViewModel
    {
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        public int ProjectID { get; set; }
    }
    /// <summary>
    /// View model that keeps track of both project details and validation for creating a file.
    /// </summary>
    public class FileAndProjectViewModel
    {
        public CreateFileViewModel FileModel { get; set; }
        public ProjectDetailsViewModel ProjectModel { get; set; }
    }
    /// <summary>
    /// Model with information about which project to delete.
    /// </summary>
    public class DeleteProjectViewModel
    {
        public int ID { get; set; }
    }
    /// <summary>
    /// Model with information baout which project a user wants to be removed from.
    /// </summary>
    public class RemoveFromProjectViewModel
    {
        public int ID { get; set; }
    }
    /// <summary>
    /// Keeps information about which file to delete and in which project it is stored.
    /// </summary>
    public class DeleteFileViewModel
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }
    }
}