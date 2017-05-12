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
            MockDb.Users.Add(u2);

            var u3 = new ApplicationUser
            {
                Id = "c",
                UserName = "sonni"
            };
            MockDb.Users.Add(u3);

            var u4 = new ApplicationUser
            {
                Id = "d",
                UserName = "donni"
            };
            MockDb.Users.Add(u4);

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

            var p3 = new Project
            {
                ID = 3,
                UserID = "b",
                Name = "proj3",
                Type = "Website",
                Deleted = false
            };
            MockDb.Projects.Add(p3);

            var p4 = new Project
            {
                ID = 4,
                UserID = "c",
                Name = "proj4",
                Type = "Mobile app",
                Deleted = false
            };
            MockDb.Projects.Add(p4);

            var p5 = new Project
            {
                ID = 5,
                UserID = "c",
                Name = "proj5",
                Type = "Website",
                Deleted = true
            };
            MockDb.Projects.Add(p5);


            // Add relations
            var r1 = new UserProjectRelation
            {
                ID = 1,
                UserID = "a",
                ProjectID = 1,
                Deleted = false
            };
            MockDb.UserProjectRelations.Add(r1);

            var r2 = new UserProjectRelation
            {
                ID = 2,
                UserID = "a",
                ProjectID = 2,
                Deleted = false
            };
            MockDb.UserProjectRelations.Add(r2);

            var r3 = new UserProjectRelation
            {
                ID = 3,
                UserID = "b",
                ProjectID = 3,
                Deleted = false
            };
            MockDb.UserProjectRelations.Add(r3);

            var r4 = new UserProjectRelation
            {
                ID = 4,
                UserID = "c",
                ProjectID = 4,
                Deleted = false
            };
            MockDb.UserProjectRelations.Add(r4);

            var r5 = new UserProjectRelation
            {
                ID = 5,
                UserID = "c",
                ProjectID = 5,
                Deleted = false
            };
            MockDb.UserProjectRelations.Add(r5);

            var r6 = new UserProjectRelation
            {
                ID = 6,
                UserID = "a",
                ProjectID = 3,
                Deleted = true
            };
            MockDb.UserProjectRelations.Add(r6);

            var r7 = new UserProjectRelation
            {
                ID = 1,
                UserID = "a",
                ProjectID = 4,
                Deleted = false
            };
            MockDb.UserProjectRelations.Add(r7);

            // Add files
            var f1 = new File
            {
                ID = 1,
                ProjectID = 1,
                Name = "file",
                Type = "HTML",
                Data = null,
                Deleted = false
            };
            MockDb.Files.Add(f1);

            var f2 = new File
            {
                ID = 2,
                ProjectID = 3,
                Name = "file",
                Type = "HTML",
                Data = null,
                Deleted = false
            };
            MockDb.Files.Add(f2);

            var f3 = new File
            {
                ID = 3,
                ProjectID = 3,
                Name = "file",
                Type = "JavaScript",
                Data = "asdf",
                Deleted = false
            };
            MockDb.Files.Add(f3);

            var f4 = new File
            {
                ID = 4,
                ProjectID = 4,
                Name = "file",
                Type = "JavaScript",
                Data = null,
                Deleted = true
            };
            MockDb.Files.Add(f4);

            var f5 = new File
            {
                ID = 5,
                ProjectID = 3,
                Name = "file5",
                Type = "JavaScript",
                Data = "asdf",
                Deleted = false
            };
            MockDb.Files.Add(f5);

            _service = new EditorService(MockDb);
        }
        [TestMethod]
        public void TestGetFile()
        {
            // Arrange
            int id = 3;

            // Act
            File result = _service.GetFile(id);

            // Assert
            Assert.AreEqual("asdf", result.Data);
        }
    }
}
