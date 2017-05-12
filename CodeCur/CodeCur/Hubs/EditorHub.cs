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
    public class EditorHub : Hub
    {
        /// <summary>
        /// Adding users to a group group when opening a file and notifying other users.
        /// </summary>
        /// <param name="fileID"></param>
        public void JoinFile(int fileID)
        {
            Groups.Add(Context.ConnectionId, Convert.ToString(fileID));
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnJoin(Context.User.Identity.Name);
        }

        /// <summary>
        /// Removing users from group when exiting file and notifying other users.
        /// </summary>
        /// <param name="fileID"></param>
        public void LeaveFile(int fileID)
        {
            Groups.Remove(Context.ConnectionId, Convert.ToString(fileID));
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnLeave(Context.User.Identity.Name);
        }

        /// <summary>
        /// Updating the content of the file for all users in the file.
        /// </summary>
        /// <param name="changeData"></param>
        /// <param name="fileID"></param>
        public void OnChange(object changeData, int fileID)
        {
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).OnChange(changeData);
        }

        /// <summary>
        /// Letting other users in the fil know where they are typing.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="fileID"></param>
        public void MoveCursor(int row, int column, int fileID)
        {
            Clients.Group(Convert.ToString(fileID), Context.ConnectionId).MoveCursor(row, column, Context.User.Identity.Name);
        }
    }
}