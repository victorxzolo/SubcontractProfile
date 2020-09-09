using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SubcontractProfile.Web.Model;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;


namespace SubcontractProfile.Web.Extension
{

    public class AuthorizeUserAttribute : AuthorizeAttribute
    {

        //  //   [ImportAttribute]
        // // public ILogger _Logger { get; set; }


        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    if (null == httpContext)
        //        throw new ArgumentNullException("httpContext");

        //    // check authenticated user
        //    if (null != httpContext.User && !httpContext.User.Identity.IsAuthenticated)
        //    {
        //        return false;
        //    }

        //    return true;
        //}


        // // public override void OnAuthorization(AuthorizationContext filterContext)
        ////  {
        //     //    //_Logger.Info("Begin OnAuthorization");
        //     //    if (null == filterContext)
        //     //        throw new ArgumentNullException("filterContext");

        //  ///      var currentUser = (SubcontractProfileUserModel)filterContext.HttpContext.Session[WebConstants.FBBConfigSessionKeys.User];
        //     //    if (currentUser == null)
        //     //    {
        //     //      //  _Logger.Info("null currentUser");
        //     //        return;
        //     //    }

        //     //    bool skipAuthorization = (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
        //     //        || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
        //     //        || currentUser.ForceLogOut);

        //     //    if (skipAuthorization)
        //     //    {
        //     //        return;
        //     //    }

        //     //    if (!this.AuthorizeCore(filterContext.HttpContext))
        //     //    {
        //     //        this.HandleUnauthorizedRequest(filterContext);
        //     //        return;
        //     //    }

        //     //  // syn current user sso token
        //     //  //  _Logger.Info(filterContext.HttpContext.Request.Headers["X-Requested-With"]);

        //     //    if (currentUser.AuthenticateType == AuthenticateType.SSO
        //     //        && filterContext.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
        //     //    {
        //     //       // _Logger.Info("EmployeeServiceWebServiceV2Service");
        //     //        var ssoError = false;


        //     //    }

        //  //   if (currentUser.AuthenticateType == AuthenticateType.SSO
        //   //     && filterContext.HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
        //  //   {
        //        // _Logger.Info("EmployeeServiceWebServiceV2Service");
        //      //   var ssoError = false;

        //    //     using (var ssoService = new EmployeeServices.EmployeeServiceWebServiceV2Service())
        //      //   {
        //          //   _Logger.Info(string.Format("Syncing user session to sso, Token:{0}", currentUser.SSOFields.Token));

        //    //         try
        //    //         {
        //     //            var syncUserSessionResponse = ssoService.syncUserSession(currentUser.SSOFields.Token);

        //      //           if (syncUserSessionResponse.Message.ErrorCode == SSOReturnStatus.Success)
        //      //           {
        //                     //_Logger.Info("SSO syncUserSession SUCCESS " +
        //                     //    string.Format("{0}:{1}",
        //                     //        syncUserSessionResponse.Message.ErrorCode,
        //                     //        syncUserSessionResponse.Message.ErrorMesg));
        //     //            }
        //     //            else
        //     //            {
        //                     //sync session ไม่สำเร็จ, force logout
        //                     //_Logger.Info("SSO syncUserSession FAIL " +
        //                     //     string.Format("{0}:{1}",
        //                     //         syncUserSessionResponse.Message.ErrorCode,
        //                     //         syncUserSessionResponse.Message.ErrorMesg));

        //                     //_Logger.Info("Sync SSO session ไม่สำเร็จ, force logout");
        //    //                 ssoError = true;
        //    //             }
        //    //         }
        //    //         catch (TimeoutException tex)
        //    //         {
        //                 //_Logger.Info("SSO syncUserSession TIMEOUT " + tex.Message);
        //    //             ssoError = true;
        //    //         }
        //             catch (Exception ex)
        //             {
        //                // _Logger.Info("SSO syncUserSession ERROR " + ex.Message);
        //                 ssoError = true;
        //             }

        //             if (ssoError)
        //             {
        //                 ((SubcontractProfileUserModel)filterContext.HttpContext.Session[WebConstants.FBBConfigSessionKeys.User])
        //                         .ForceLogOut = true;

        //                 //filterContext.Result = new RedirectToRouteResult(
        //                 //    new RouteValueDictionary(
        //                 //        new
        //                 //        {
        //                 //            controller = "Account",
        //                 //            action = "Logout",
        //                 //        })
        //                 //    );
        //                 return;
        //             }
        //         }
        //     }
        // }

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    filterContext.Result = new HttpUnauthorizedResult();
        //}

        public static class SSOReturnStatus
        {
            public const string AlreadyLoggedOut = "BAV007";
            public const string DataOrStatusHasChanged = "BAV008";
            public const string FunctionIsRedundant = "BAV015";
            public const string IncorectTransaction = "BAV017";
            public const string IncorrctedUserStatus = "BAV014";
            public const string IncorrectPrivilege = "BAV012";
            public const string InvalidSession = "BAV016";
            public const string InvalidUserNameOrPassword = "BAV001";
            public const string LogoutSuccess = "BAV005";
            public const string MultipleSessions = "BAV011";
            public const string NeverLoggedIn = "BAV013";
            public const string NotFound = "BAV004";
            public const string OnProcessing = "BAV002";
            public const string SessionExpiredOrAlreadyLoggedOut = "BAV010";
            public const string Success = "BAV000";
            public const string TemporaryNotAvailable = "BAV009";
            public const string TemporarySuspended = "BAV003";
            public const string UnAuthorized = "BAV006";
        }
    }
}
