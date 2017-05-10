using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeCur.Models.Entities;
using CodeCur.Models;

namespace CodeCur.Services
{
    /// <summary>
    /// Provides service functions to editor with database access.
    /// </summary>
    public class EditorService
    {
        private readonly IAppDataContext _db;

        public EditorService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        /// <summary>
        /// Retrieves file from database.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>File.</returns>
        public File GetFile(int ID)
        {
            File file = (from item in _db.Files
                         where item.ID == ID
                         select item).SingleOrDefault();
            return file;
        }

        /// <summary>
        /// Saves file content to database.
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileID"></param>
        public void SaveFile(string content, int fileID)
        {
            if (fileID != 0)
            {
                File file = (from item in _db.Files
                             where item.ID == fileID
                             select item).SingleOrDefault();
                file.Data = content;

                // Success check?
                _db.SaveChanges();
            }
            
        }

        /// <summary>
        /// Validates user's access to file.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="fileID"></param>
        /// <returns>Boolean.</returns>
        public bool AuthorizeFileAccess(string userID, int fileID)
        {

            if ((from conn in _db.UserProjectRelations
                 join file in _db.Files on conn.ProjectID equals file.ProjectID
                 where conn.UserID == userID && file.ID == fileID && file.Deleted == false
                 select conn).Any())
            {
                return true;
            }
            return false;
        }
    }
}