using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System.Net;

namespace CodeCur.Hubs
{
    public static class UserHandler
    {
        
    }

    public class EditorHub : Hub
    {
        public void JoinFile(int fileID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(fileID));
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnJoin(Context.User.Identity.Name);
        }

        public void LeaveFile(int fileID)
        {
            Groups.Remove(Context.ConnectionId, Convert.ToString(fileID));
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnLeave(Context.User.Identity.Name);
        }

        public void OnChange(object changeData, int fileID)
        {
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnChange(changeData);
        }

        public void PopulateList(int fileID, string username)
        {
            Clients.Group(Convert.ToString(fileID)).PopulateList(username);
        }
    }
}