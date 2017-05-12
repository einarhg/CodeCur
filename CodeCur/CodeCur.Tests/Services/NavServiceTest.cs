using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeCur.Services;
using CodeCur.Models;
using CodeCur.Models.Entities;
using System.Collections.Generic;
using System.Linq;

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
                ID = 7,
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
                Name = "file2",
                Type = "HTML",
                Data = null,
                Deleted = false
            };
            MockDb.Files.Add(f2);

            var f3 = new File
            {
                ID = 3,
                ProjectID = 3,
                Name = "file3",
                Type = "JavaScript",
                Data = "asdf",
                Deleted = false
            };
            MockDb.Files.Add(f3);

            var f4 = new File
            {
                ID = 4,
                ProjectID = 4,
                Name = "file4",
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
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void TestAddProjectToDb()
        {
            // Arrange
            Project project = new Project
            {
                UserID = "a",
                Name = "testproject",
                Type = "Website",
                Deleted = false
            };

            // Act
            _service.AddProjectToDb(project);
            List<Project> result = _service.GetUserProjects(project.UserID);

            // Assert
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void TestAddUserProjectRelationByID()
        {
            // Arrange
            const string userID = "b";
            const int projectID = 1;

            // Act
            _service.AddUserProjectRelationByID(userID, projectID);
            List<Project> result = _service.GetUserProjects(userID);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestAddUserProjectRelationByName()
        {
            // Arrange
            const string username = "jonni";
            const string userID = "b";
            const int projectID = 1;

            // Act
            _service.AddUserProjectRelationByName(username, projectID);
            List<Project> result = _service.GetUserProjects(userID);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestAddFileToDb()
        {
            // Arrange
            File file = new File
            {
                ProjectID = 1,
                Name = "testfile",
                Type = "JavaScript",
                Data = "asdf",
                Deleted = false
            };

            // Act
            _service.AddFileToDb(file);
            List<File> result = _service.GetProjectFiles(file.ProjectID).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestGetProjectFiles()
        {
            // Arrange
            const int projectID = 1;

            // Act
            List<File> result = _service.GetProjectFiles(projectID).ToList();

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void TestGetProjectName()
        {
            // Arrange
            const int projectID = 1;

            // Act
            var result = _service.GetProjectName(projectID);

            // Assert
            Assert.AreEqual("proj", result);
        }

        [TestMethod]
        public void TestGetUserName()
        {
            // Arrange
            const string UserID = "a";

            // Act
            var result = _service.GetUserName(UserID);

            // Assert
            Assert.AreEqual("nonni", result);
        }

        [TestMethod]
        public void TestValidFileNameInvalid()
        {
            // Arrange
            File file = new File
            {
                ProjectID = 1,
                Name = "file",
                Type = "HTML",
                Data = null,
                Deleted = false
            };

            // Act
            var result = _service.ValidFileName(file.Name, file.Type, file.ProjectID);

            // Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void TestValidFileNameValid()
        {
            // Arrange
            File file = new File
            {
                ProjectID = 1,
                Name = "Validfile",
                Type = "HTML",
                Data = null,
                Deleted = false
            };

            // Act
            var result = _service.ValidFileName(file.Name, file.Type, file.ProjectID);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestDeleteProject()
        {
            // Arrange
            const int projectID = 1;
            const string userID = "a";

            // Act
            _service.DeleteProject(projectID);
            List<Project> result = _service.GetUserProjects(userID);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestDeleteAllFiles()
        {
            // Arrange
            const int projectID = 3;

            // Act
            _service.DeleteAllFiles(projectID);
            List<File> result = _service.GetProjectFiles(projectID).ToList();

            // Assert
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestDeleteFile()
        {
            // Arrange
            const int fileID = 5;
            const int projectID = 3;

            // Act
            _service.DeleteFile(fileID);
            List<File> result = _service.GetProjectFiles(projectID).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestRemoveUserFromProject()
        {
            // Arrange
            const int projectID = 4;
            const string userID = "a";

            // Act
            _service.RemoveUserFromProject(projectID, userID);
            List<Project> result = _service.GetUserProjects(userID);

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void TestAuthorizeProjectAccessAccept()
        {
            // Arrange
            const int projectID = 4;
            const string userID = "a";

            // Act
            bool result = _service.AuthorizeProjectAccess(userID, projectID);

            // Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void TestAuthorizeProjectAccessDeny()
        {
            // Arrange
            const int projectID = 4;
            const string userID = "b";

            // Act
            bool result = _service.AuthorizeProjectAccess(userID, projectID);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
