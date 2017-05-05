using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCur.Models.Entities;
using CodeCur.Models;

namespace CodeCur.Services
{
    public class NavService
    {
        static ApplicationDbContext _db = new ApplicationDbContext();
        public static void AddProjectToDb(Project project)
        {
            
            // Add the new object to the Orders collection.
            _db.Projects.Add(project);

            // Fail check?
            _db.SaveChanges();
        }

        public static IEnumerable<Project> GetUserProjects(string ID)
        {
            /*var userProjectIds = (from PP in _db.UserProjectRelations where PP.UserID == ID select PP.ID).ToList();

            List<Project> projects = new List<Project>();

            foreach (var idnum in userProjectIds)
            {
                projects.Add(
                    (from prj in _db.Projects where prj.ID == idnum select prj).FirstOrDefault());
            }*/

            IEnumerable<Project> projects = (from project in _db.Projects
                                             where project.UserID == ID
                                             select project).ToList();
            return projects;
        }

        /*
        public static IEnumerable<Project> GetOtherProjects(string ID)
        {
            IEnumerable<Project> projects = 

            return projects;
        }

        public static IEnumerable<Project> GetAllProjects(string ID)
        {
            IEnumerable<Project> projects =

            return projects;
        }
        */
        public static void AddFileToDb(File file)
        {
            _db.Files.Add(file);
            //Fail check?
            _db.SaveChanges();
        }

        public static IEnumerable<File> GetProjectFiles(int ID)
        {
            IEnumerable<File> files = (from file in _db.Files
                                       where file.ProjectID == ID
                                       select file).ToList();
            return files;
        }

        public static string GetProjectName(int ID)
        {
            Project item = (from project in _db.Projects
                            where project.ID == ID
                            select project).FirstOrDefault();
            return item.Name;
        }
    }
}