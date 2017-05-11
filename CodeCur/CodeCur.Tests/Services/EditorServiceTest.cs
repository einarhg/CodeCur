using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeCur.Services;
using CodeCur.Models;
using CodeCur.Models.Entities;

namespace CodeCur.Tests.Services
{
    [TestClass]
    public class EditorServiceTest
    {
        private EditorService _service;

        [TestInitialize]
        public void initialize()
        {
            var MockDb = new MockDatabase();

            // Add users
            var u1 = new ApplicationUser
            {
                Id = "a",
                UserName = "nonni"
            };
            MockDb.Users.Add(u1);

            var u2 = new ApplicationUser
            {
                Id = "b",
                UserName = "jonni"
            };
            MockDb.Users.Add(u1);

            // Add projects
            var p1 = new Project
            {
                ID = 1,
                UserID = "a",
                Name = "proj",
                Type = "Website",
                Deleted = false
            };
            MockDb.Projects.Add(p1);

            var p2 = new Project
            {
                ID = 2,
                UserID = "a",
                Name = "proj2",
                Type = "Website",
                Deleted = false
            };
            MockDb.Projects.Add(p2);

            // Add relations
            var r1 = new UserProjectRelation
            {
                ID = 1,
                UserID = "a",
                ProjectID = 1
            };
            MockDb.UserProjectRelations.Add(r1);

            // Add files
            var f1 = new File
            {
                ID = 1,
                ProjectID = 1,
                Name = "file",
                Type = "HTML",
                //DateCreated = actual datetimer format,
                Data = null,
                Deleted = false
            };
            MockDb.Files.Add(f1);


            _service = new EditorService(MockDb);
        }
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
