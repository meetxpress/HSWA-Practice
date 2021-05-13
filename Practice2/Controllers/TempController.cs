using Practice2.Filters;
using Practice2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Practice2.Controllers {
    [CustomException]
    public class TempController : ApiController {
        DbInternalEntities db = new DbInternalEntities();
        [AuthoFilter]
        public string PostIndex(Account_Holder acc) {
            if(acc != null) {
                db.Account_Holder.Add(acc);
                db.SaveChanges();
                return "Data Inserted Successfully";
            } else {
                return "Something went wrong";
            }
        }        

        public List<Account_Holder> GetAccountDetails() {            
            return db.Account_Holder.ToList();
        }

        public Account_Holder GetAccountDetails(int accNo) {
            return db.Account_Holder.Find(accNo);
        }

        public HttpResponseMessage GetAccountDetails(String accType) {
            List<Account_Holder> acc = db.Account_Holder.ToList();
            List<Account_Holder> toAcc = new List<Account_Holder>();
            foreach(Account_Holder a in acc) {             
                if(a.actype == "Saving") {
                    if(a.actype == accType) {
                        toAcc.Add(a);
                    }
                } else if(a.actype == "Current") {
                    if (a.actype == accType) {
                        toAcc.Add(a);
                    }
                } else if(a.actype == "Recurring") {
                    if (a.actype == accType) {
                        toAcc.Add(a);
                    }
                }                 
            }
            if (toAcc.Count == 0) {
                throw new NotValidAccountTypeException();
            } else {
                HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK);
                res.Content = new ObjectContent(typeof(List<Account_Holder>), toAcc, new JsonMediaTypeFormatter());
                return res;
            }
        }

        public void Put(int id, [FromBody] string value) {
        }

        public void Delete(int id) {
        }
    }
}
