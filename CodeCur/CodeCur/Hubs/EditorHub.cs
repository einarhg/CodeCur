using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace CodeCur.Hubs
{
    public class EditorHub : Hub
    {
        List<string> OnlineEditors = new List<string>();

        public void JoinFile(int fileID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(fileID));
        }

        public void OnChange(object changeData, int fileID)
        {
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnChange(changeData);
        }

        //IHubContext context = GlobalHost.ConnectionManager.GetHubContext<EditorHub>();
        //context.Clients.All.addToEditorList(name);

    }
}