using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeCur.Services;
using CodeCur.Tests;
using CodeCur.Models;
using CodeCur.Models.Entities;
using System.Collections.Generic;

namespace CodeCur.Tests.Services
{
    [TestClass]
    public class NavServiceTest
    {
        private NavService _service;

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


            _service = new NavService(MockDb);
        }

        [TestMethod]
        public void TestGetUserProjects()
        {
            // Arrange
            const string userID = "a";

            // Act
            List<Project> result = _service.GetUserProjects(userID);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        //[TestMethod]
        //public void TestGetUserProjects()
        //{
        //    // Arrange
        //    const string userID = "a";

        //    // Act
        //    List<Project> result = _service.GetUserProjects(userID);

        //    // Assert
        //    Assert.AreEqual(1, result.Count);
        //}
    }
}
