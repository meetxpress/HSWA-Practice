using Practice2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Practice2.Filters {
    public class AuthoFilter2 : AuthorizationFilterAttribute {
        public override void OnAuthorization(HttpActionContext actionContext) {
            base.OnAuthorization(actionContext);
            String Accno = actionContext.Request.RequestUri.ToString().Split('=')[1];

            if (actionContext.Request.Headers.Authorization == null) {
                HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponse.Content = new StringContent("Authorization data is missing");
                httpResponse.ReasonPhrase = "No Data for Authorization";
                actionContext.Response = httpResponse;
            } else {
                String encodedData = actionContext.Request.Headers.Authorization.Parameter;
                String decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(encodedData));
                String[] u2data = decodedData.Split(':');
                String u2name = u2data[0];
                String u2pass = u2data[1];

                DbInternalEntities db = new DbInternalEntities();                
                Bank_Admin u2 = db.Bank_Admin.Where(uu => uu.adminName == u2name && uu.adminPass == u2pass).FirstOrDefault();
                if(u2 != null) {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(u2.adminName), null);
                } else {
                    if(u2name == Accno) {
                        Account_Holder u2_2 = db.Account_Holder.Where(uu2 => uu2.acname == u2name && uu2.acpin + "" == u2pass).FirstOrDefault();
                        if(u2_2 != null) {
                            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(u2_2.acname), null);
                        }
                    } else {
                        HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        httpResponse.Content = new StringContent("Authorization Data is invalid!!");
                        httpResponse.ReasonPhrase = "No Authorization!!";
                        actionContext.Response = httpResponse;
                    }
                } 
            }
        }
    }
}