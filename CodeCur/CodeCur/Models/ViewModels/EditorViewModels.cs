using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    /// <summary>
    /// Keeps information about a specific file.
    /// </summary>
    public class EditorViewModel
    {
        public File File { get; set; }
    }
    /// <summary>
    /// Keeps track of data that is passed in an out of database.
    /// </summary>
    public class SaveViewModel
    {
        public string Data { get; set; }
    }
}