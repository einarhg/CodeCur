<<<<<<< HEAD:CodeCur/CodeCur/Models/ViewModels/ProjectViewModel.cs
﻿using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<File> Files { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
=======
﻿using CodeCur.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCur.Models.ViewModels
{
    public class ProjectViewModel
    {
        public List<File> Files { get; set; }
        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
>>>>>>> 4518c156d594061da6a4c47357f4d747c28f2838:CodeCur/CodeCur/Models/ViewModels/ProjectViewModels.cs
}