using Practice2.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Practice2.Filters {
    public class CustomException : ExceptionFilterAttribute {
        public override void OnException(HttpActionExecutedContext actionExecutedContext) {
            base.OnException(actionExecutedContext);
            if (actionExecutedContext.Exception is DbUpdateException) {
                HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.BadRequest);
                res.Content = new StringContent("Provide Required data!");
                res.ReasonPhrase = "Not able to modify!";
                actionExecutedContext.Response = res;
            } else if(actionExecutedContext.Exception is NotValidAccountTypeException) {
                HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.NotFound);
                res.Content = new StringContent("Account type not found!");                
                actionExecutedContext.Response = res;
            } else {
                HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                res.Content = new StringContent("Something went Wrong!");
                actionExecutedContext.Response = res;
            }
        }
    }
}