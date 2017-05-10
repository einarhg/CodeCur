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
    /// <summary>
    /// Provides service functions to file/project navigation.
    /// </summary>
    public class NavService
    {
        /// <summary>
        /// Adds project to database.
        /// </summary>
        /// <param name="project"></param>
        public static void AddProjectToDb(Project project)
        {   
            ApplicationDbContext _db = new ApplicationDbContext();
            // Add the new object to the Orders collection.
            _db.Projects.Add(project);

            // Fail check?
            _db.SaveChanges();

            AddUserProjectRelationByID(project.UserID, project.ID);
        }

        /// <summary>
        /// Adds a user -project pair to the UserProjectRelations table. Using userID.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="projectID"></param>
        public static void AddUserProjectRelationByID(string userID, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            UserProjectRelation relation = new UserProjectRelation();

            relation.ProjectID = projectID;
            relation.UserID = userID;

            _db.UserProjectRelations.Add(relation);
            _db.SaveChanges();
        }

        /// <summary>
        /// Adds a user -project pair to the UserProjectRelations table. Using username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="projectID"></param>
        public static void AddUserProjectRelationByName(string username, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            UserProjectRelation relation = new UserProjectRelation();

            if (RelationExists(username, projectID))
            {
                return;
            }

            relation.ProjectID = projectID;
            relation.UserID = (from user in _db.Users
                               where user.UserName == username
                               select user.Id).FirstOrDefault();

            _db.UserProjectRelations.Add(relation);
            _db.SaveChanges();  
        }

        /// <summary>
        /// Gets list of projects user has access to.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>List of projects user has access to.</returns>
        public static List<Project> GetUserProjects(string userID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            List<Project> projects = (from conn in _db.UserProjectRelations
                                      join proj in _db.Projects on conn.ProjectID equals proj.ID
                                      where conn.UserID == userID && proj.Deleted == false && conn.Deleted == false
                                      select proj).ToList();

            //var userProjectIds = (from PP in _db.UserProjectRelations
            //                      where PP.UserID == ID && PP.Deleted == false
            //                      select PP.ProjectID).ToList();

            //List<Project> projects = new List<Project>();

            //foreach (var idnum in userProjectIds)
            //{
            //    var item = (from prj in _db.Projects
            //                where prj.ID == idnum && prj.Deleted == false
            //                select prj).FirstOrDefault();

            //    if (item != null)
            //    {
            //        projects.Add(item);
            //    }
            //}

            return projects;
        }

        /// <summary>
        /// Adds file to database.
        /// </summary>
        /// <param name="file"></param>
        public static void AddFileToDb(File file)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

                _db.Files.Add(file);
                //Fail check?
                _db.SaveChanges();
        }

        /// <summary>
        /// Gets files in project.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Files in project.</returns>
        public static IEnumerable<File> GetProjectFiles(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            IEnumerable<File> files = (from file in _db.Files
                                       where file.ProjectID == ID && file.Deleted == false
                                       select file).ToList();
            return files;
        }

        /// <summary>
        /// Gets project name.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Name of project.</returns>
        public static string GetProjectName(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            Project item = (from project in _db.Projects
                            where project.ID == ID
                            select project).FirstOrDefault();
            return item.Name;
        }

        /// <summary>
        /// Gets username by id.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>Username.</returns>
        public static string GetUserName(string ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            string name = (from user in _db.Users
                               where user.Id == ID
                               select user.UserName).FirstOrDefault();
            return name;
        }

        /// <summary>
        /// Validates that the file is not a duplicate.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="projectID"></param>
        /// <returns>Boolean.</returns>
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

        /// <summary>
        /// Marks project as deleted.
        /// </summary>
        /// <param name="ID"></param>
        public static void DeleteProject(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var ToDelete = (from project in _db.Projects
                           where project.ID == ID
                           select project).FirstOrDefault();

            ToDelete.Deleted = true;
            _db.SaveChanges();
        }

        /// <summary>
        /// Deletes all files in a project.
        /// </summary>
        /// <param name="ID"></param>
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

        /// <summary>
        /// Marks file as deleted.
        /// </summary>
        /// <param name="ID"></param>
        public static void DeleteFile(int ID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var ToDelete = (from file in _db.Files
                           where file.ID == ID
                           select file).FirstOrDefault();

            ToDelete.Deleted = true;
            _db.SaveChanges();
        }

        /// <summary>
        /// Marks UserProjectRelation as deleted.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="username"></param>
        public static void RemoveUserFromProject(int ID, string username)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var ToRemove = (from user in _db.UserProjectRelations
                            where user.ProjectID == ID && username == user.UserID
                            select user).FirstOrDefault();

            ToRemove.Deleted = true;
            _db.SaveChanges();
        }

        /// <summary>
        /// Validates user's access to project.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="projectID"></param>
        /// <returns>Boolean.</returns>
        public static bool AuthorizeProjectAccess(string userID, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            if ((from conn in _db.UserProjectRelations
                 join proj in _db.Projects on conn.ProjectID equals proj.ID
                where conn.UserID == userID && conn.ProjectID == projectID && proj.Deleted == false
                select conn).Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether user exists in database.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Boolean.</returns>
        public static bool DoesUserExist(string username)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            if ((from user in _db.Users
                where user.UserName == username
                select user).Any())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks whether user has access to project by username.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="projectID"></param>
        /// <returns>Boolean.</returns>
        public static bool AlreadyHasAccesss(string username, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            string userID = (from user in _db.Users
                             where user.UserName == username
                             select user.Id).SingleOrDefault();

            if ((from conn in _db.UserProjectRelations
                 where conn.UserID == userID && conn.ProjectID == projectID && conn.Deleted == false
                 select conn).Any())
            {
                return true;
            }
            return false;
        }

        public static bool NoDuplicateFileName(string filename)
        {
            ApplicationDbContext _db = new ApplicationDbContext();

            if ((from file in _db.Files
                 where file.Name == filename
                 select file).Any())
            {
                return true;
            }
            return false;
        }

        public static bool RelationExists(string username, int projectID)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            var userID = (from user in _db.Users
                               where user.UserName == username
                               select user.Id).FirstOrDefault();

            var rel = (from relation in _db.UserProjectRelations
                        where userID == relation.UserID && projectID == relation.ProjectID && relation.Deleted == true
                        select relation).FirstOrDefault();

            if (rel != null)
            {
                rel.Deleted = false;
                _db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}