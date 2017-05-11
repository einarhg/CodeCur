using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeDbSet;
using System.Data.Entity;
using CodeCur.Models;
using CodeCur.Models.Entities;

namespace CodeCur.Tests
{
    /// <summary>
    /// This is an example of how we'd create a fake database by implementing the 
    /// same interface that the BookeStoreEntities class implements.
    /// </summary>
    public class MockDatabase : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDatabase()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            this.Projects = new InMemoryDbSet<Project>();
            this.Files = new InMemoryDbSet<File>();
            this.UserProjectRelations = new InMemoryDbSet<UserProjectRelation>();
            this.Users = new InMemoryDbSet<ApplicationUser>();
        }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<File> Files { get; set; }
        public IDbSet<UserProjectRelation> UserProjectRelations { get; set; }
        public IDbSet<ApplicationUser> Users { get; set; }

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;
            //changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
            //changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}