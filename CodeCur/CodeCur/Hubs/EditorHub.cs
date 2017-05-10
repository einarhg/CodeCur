using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CodeCur.Hubs
{
    public class EditorHub : Hub
    {
        public void JoinFile(int fileID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(fileID));
        }

        public void OnChange(object changeData, int fileID)
        {
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnChange(changeData);
        }

        List<string> Editors = new List<string>();

        public void listEditors(string name)
        {
            appendToEditorList(name);
        }

        public void appendToEditorList(string name)
        {
            Editors.Add(name);
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<EditorHub>();
            context.Clients.All.appendToEditorList(name);
        }
    }
}