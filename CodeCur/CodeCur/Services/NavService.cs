using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCur.Models.Entities;
using CodeCur.Models;
using Microsoft.AspNet.Identity;
using System.Data.Linq;

namespace CodeCur.Services
{
    public class NavService
    {
        public static void AddProjectToDb(Project project)
        {   
            ApplicationDbContext _db = new ApplicationDbContext();
            // Add the new object to the Orders collection.
            _db.Projects.Add(project);

            // Fail check?
            _db.SaveChanges();

            AddUserProjectRelationByID(project.UserID, project.ID);
        }

        public static void AddUserProjectRelationByID(string userID, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            UserProjectRelation relation = new UserProjectRelation();

            relation.ProjectID = projectID;
            relation.UserID = userID;

            _db.UserProjectRelations.Add(relation);
            _db.SaveChanges();
        }

        public static void AddUserProjectRelationByName(string username, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            UserProjectRelation relation = new UserProjectRelation();

            relation.ProjectID = projectID;
            relation.UserID = (from user in _db.Users
                               where user.UserName == username
                               select user.Id).FirstOrDefault();

            _db.UserProjectRelations.Add(relation);
            _db.SaveChanges();
        }

        public static List<Project> GetUserProjects(string ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var userProjectIds = (from PP in _db.UserProjectRelations
                                  where PP.UserID == ID
                                  select PP.ProjectID).ToList();

            List<Project> projects = new List<Project>();

            // Added delete == false
            foreach (var idnum in userProjectIds)
            {
                projects.Add(
                (from prj in _db.Projects
                where prj.ID == idnum && prj.Deleted == false
                select prj).FirstOrDefault());
            }

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
            ApplicationDbContext _db = new ApplicationDbContext();
            if (ValidFileName(file.Name, file.Type, file.ProjectID))
            {
                _db.Files.Add(file);
                //Fail check?
                _db.SaveChanges();
            }
        }

        public static IEnumerable<File> GetProjectFiles(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            IEnumerable<File> files = (from file in _db.Files
                                       where file.ProjectID == ID
                                       select file).ToList();
            return files;
        }

        public static string GetProjectName(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            Project item = (from project in _db.Projects
                            where project.ID == ID
                            select project).FirstOrDefault();
            return item.Name;
        }

        public static string GetUserName(string ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            string name = (from user in _db.Users
                               where user.Id == ID
                               select user.UserName).FirstOrDefault();
            return name;
        }

        public static bool ValidFileName(string name, string type, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            if ((from item in _db.Files
                where item.Name == name && item.Type == type && item.ProjectID == projectID
                select item).Any())
                {
                    return false;
                }
            return true;
        }

        public static void DeleteProject(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var ToDelete = (from project in _db.Projects
                           where project.ID == ID
                           select project).FirstOrDefault();

            ToDelete.Deleted = true;
            _db.SaveChanges();
        }

        public static void DeleteAllFiles(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var ToDelete = (from file in _db.Files
                           where file.ProjectID == ID
                           select file).ToList();
            foreach (var item in ToDelete)
            {
                item.Deleted = true;
            }
            _db.SaveChanges();
        }

        public static void DeleteFile(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var ToDelete = (from file in _db.Files
                           where file.ID == ID
                           select file).FirstOrDefault();

            ToDelete.Deleted = true;
            _db.SaveChanges();
        }
    }
}