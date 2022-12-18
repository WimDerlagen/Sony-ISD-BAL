using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Web;
using System.Collections;
using System.Web.Security;
using Sony.ISD.WebToolkit.Components;

namespace Sony.ISD.BAL.Components
{
    public class BALContext : WTContext
    {
        private static readonly string dataKey = "balContextStore";

        #region state

        private int projectId = -2;
        /// <summary>
        /// Gets or sets the project id. The getter retreives the project id from the querystring
        /// </summary>
        public int ProjectID
        {
            get
            {
                if (projectId < 0)
                {
                    projectId = GetIntFromQueryString("ProjectID", -1);
                }

                return projectId;
            }
            set { projectId = value; }
        }

        private int meetingId = -1;
        public int MeetingID
        {
            get
            {
                if (meetingId < 0)
                {
                    meetingId = GetIntFromQueryString("MeetingID", -1);
                }

                return meetingId;
            }
            set { meetingId = value; }
        }
            

        public MembershipUser User
        {
            get
            {
                return Membership.GetUser();
            }
        }

        #endregion

        #region Current
        protected BALContext(HttpContext context) : base(context){}
        protected BALContext(HttpContext context, bool isRewritten) : base(context, isRewritten) { }


        public static new BALContext Current
        {
            get
            {
                HttpContext httpContext = HttpContext.Current;
                BALContext context = null;
                if (httpContext != null)
                {
                    context = httpContext.Items[dataKey] as BALContext;
                }
                else
                {
                    context = Thread.GetData(GetSlot(dataKey)) as BALContext;
                }

                if (context == null)
                {

                    if (httpContext == null)
                        throw new Exception("No wtContext exists in the Current Application. AutoCreate fails since HttpContext.Current is not accessible.");

                    context = new BALContext(httpContext, true);
                    SaveContextToStore(context, dataKey);
                }
                return context;
            }
        }
        #endregion
    }
}
