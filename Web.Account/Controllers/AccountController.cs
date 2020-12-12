using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Account.Extensions;
using Web.Account.Models;

namespace Web.Account.Controllers
{
    public class AccountController : Controller
    {

        AccountDatabaseEntities dbObj = new AccountDatabaseEntities();
        // GET: Account

        public ActionResult Account(tbl_Account obj)
        {
            return View(obj);
        }

        [HttpPost]
        public ActionResult AddAccount(tbl_Account model)
        {
            if (ModelState.IsValid)
            {
                tbl_Account obj = new tbl_Account();
                obj.RefNo = model.RefNo;
                obj.AccountNumber = model.AccountNumber;
                obj.AccountHolder = model.AccountHolder;
                obj.CurrentBalace = model.CurrentBalace;
                obj.BankName = model.BankName;
                obj.OpeningDate = model.OpeningDate;
                obj.IsActive = model.IsActive;

                if (model.RefNo == 0)
                {

                    dbObj.tbl_Account.Add(obj);
                    this.AddNotification("Account is Added Successfully", NotificationType.SUCCESS);
                    dbObj.SaveChanges();
                }
                else {

                    dbObj.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                    dbObj.SaveChanges();
                }
                
            }
            
            ModelState.Clear();   
            return View("Account");

        }

        public ActionResult AccountList()
        {
            var res = dbObj.tbl_Account.ToList();
            return View(res);
        }

        public ActionResult Delete (int refno) {

            var res = dbObj.tbl_Account.Where(x => x.RefNo == refno).First();
            dbObj.tbl_Account.Remove(res);
            this.AddNotification("Account is Removed Successfully", NotificationType.WARNING);
            dbObj.SaveChanges();
            var list = dbObj.tbl_Account.ToList();
            return View("AccountList", list);
        }

      
        }




    }
