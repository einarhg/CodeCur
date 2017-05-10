using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeCur.Services;
using CodeCurr.Tests;
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

            // Add projects
            var p1 = new Project
            {
                ID = 1,
                UserID = "a",
                Name = "proj",
                Type = "Website",
                Deleted = false
            };

            // Add relations
            var r1 = new UserProjectRelation
            {
                ID = 1,
                UserID = "a",
                ProjectID = 1
            };

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


            _service = new NavService(MockDb);
        }

        [TestMethod]
        public void TestGetUserProjects()
        {
            // Arrange: 
            const string userID = "a";

            // Act:
            List<Project> result = _service.GetUserProjects(userID);

            // Assert:
            Assert.AreEqual(1, result.Count);
        }
    }
}
