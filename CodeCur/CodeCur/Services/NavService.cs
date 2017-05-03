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
        public static void AddProjectToDb(Project project)
        {
            ApplicationDbContext _db = new ApplicationDbContext();
            // Add the new object to the Orders collection.
            _db.Projects.Add(project);

            // Fail check?
            _db.SaveChanges();
        }
    }
}