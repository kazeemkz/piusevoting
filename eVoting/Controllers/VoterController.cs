﻿using eVoting.DAL;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using PagedList;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Excel;
using System.Data;

namespace eVoting.Controllers
{
    [Authorize]
    public class VoterController : Controller
    {
        UnitOfWork work = new UnitOfWork();
        //
        // GET: /Voter/

        public ActionResult Index(string FirstName, string dept, string ID, int? page, string Voted)
        {
            List<SelectListItem> theItem = new List<SelectListItem>();
            theItem.Add(new SelectListItem() { Text = "...Choose", Value = "" });

            theItem.Add(new SelectListItem() { Text = "False", Value = "False" });
            theItem.Add(new SelectListItem() { Text = "True", Value = "True" });
            //foreach (var post in thePost)
            //{
            //theItem.Add(new SelectListItem() { Text = post.PostName, Value = Convert.ToString(post.PostID) });
            //   }

            ViewBag.Item = theItem;

            var voters = from v in work.VoterRepository.Get() select v;

            if (!(string.IsNullOrEmpty(FirstName)))
            {
                voters = voters.Where(a => a.FirstName.ToLower().Contains(FirstName.ToLower()));
            }

            if (!(string.IsNullOrEmpty(dept)))
            {
                voters = voters.Where(a => a.Department.ToLower().Contains(dept.ToLower()));
            }

            if (!(string.IsNullOrEmpty(ID)))
            {
                voters = voters.Where(a => a.Matric == ID);
            }

            if (!(string.IsNullOrEmpty(Voted)))
            {
                bool theVote = Convert.ToBoolean(Voted);
                voters = voters.Where(a => a.Voted == theVote);
            }

            voters = voters.Where(a => a.LastName != "Oyebode1234567");
            //  voters = voters.Where(a => a.LastName != "Oyebode1234567");
            voters = voters.Where(a => a.IdentityNumber != "akinola");
            voters = voters.Where(a => a.IdentityNumber != "password");


            int pageSize = 100;
            int pageNumber = (page ?? 1);
            ViewBag.Count = voters.Count();



            return View(voters.ToPagedList(pageNumber, pageSize));
            //List<Voter> theVoterList = new List<Voter>();
            //theVoterList = work.VoterRepository.Get().ToList();
            //return View(theVoterList);
        }

        [Authorize(Roles = "SuperAdmin,InterAdmin")]
        public ActionResult Index2(string FirstName, string dept, string ID, int? page, string Voted, string Department)
        {
            List<SelectListItem> theItem = new List<SelectListItem>();
            theItem.Add(new SelectListItem() { Text = "...Choose", Value = "" });
            theItem.Add(new SelectListItem() { Text = "False", Value = "False" });
            theItem.Add(new SelectListItem() { Text = "True", Value = "True" });
            ViewBag.Item = theItem;

            var voters = from v in work.VoterRepository.Get() select v;

            if (!(string.IsNullOrEmpty(FirstName)))
            {
                voters = voters.Where(a => a.FirstName.ToLower().Contains(FirstName.ToLower()));
            }

            if (!(string.IsNullOrEmpty(dept)))
            {
                voters = voters.Where(a => a.Department.ToLower().Contains(dept.ToLower()));
            }

            if (!(string.IsNullOrEmpty(ID)))
            {
                voters = voters.Where(a => a.IdentityNumber == ID);
            }

            if (!(string.IsNullOrEmpty(Department)))
            {
                voters = voters.Where(a => a.Department == Department);
            }

            if (!(string.IsNullOrEmpty(Voted)))
            {
                bool theVote = Convert.ToBoolean(Voted);
                voters = voters.Where(a => a.Voted == theVote);
            }

            voters = voters.Where(a => a.LastName != "Oyebode1234567");
            voters = voters.Where(a => a.IdentityNumber != "chair");
            voters = voters.Where(a => a.IdentityNumber != "password");


            int pageSize = 600;
            int pageNumber = (page ?? 1);
            ViewBag.Count = voters.Count();

            return View(voters.ToPagedList(pageNumber, pageSize));
            //List<Voter> theVoterList = new List<Voter>();
            //theVoterList = work.VoterRepository.Get().ToList();
            //return View(theVoterList);
        }

        public ActionResult Populate()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult Populate(IEnumerable<HttpPostedFileBase> file)
        // public void Populate()
        {


            HttpPostedFileBase theFile = Request.Files[0];
            var fileName = Path.GetFileName(Request.Files[0].FileName);

            IExcelDataReader excelReader = null;
            //if ((fileExtension.EndsWith(".xlsx")))
            //{
            //    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            //    excelReader = ExcelReaderFactory.CreateOpenXmlReader(theFile.InputStream);
            //}

            //if ((fileExtension.EndsWith(".xls")))
            //{

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            excelReader = ExcelReaderFactory.CreateBinaryReader(theFile.InputStream);
            //  }
            // ExcelDataReader reader = new ExcelDataReader(ExcelFileUpload.PostedFile.InputStream);

            /// FileStream stream = File.Open(Request.Files[0], FileMode.Open, FileAccess.Read);


            //...

            //...
            //3. DataSet - The result of each spreadsheet will be created in the result.Tables
            DataSet result = excelReader.AsDataSet();
            //  ...
            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = true;
            DataSet result2 = excelReader.AsDataSet();

            //5. Data Reader methods
            int counter = 0;

            int usernameCouner = 2000;
            var chars = "xckheayrydzjcmgncb4au9w8xu5ur93hmb3mqa4j3n3nwm3ktvj6c2vj9kckdnv3n4bsv6a8ev9xjcvk3n5m7rka9a5xz7hz8zrmn3kz3n4nzmavn3kwn7k8kvc3n2a9s3muabtfbusk347sbua3hdkcks28jk";
            while (excelReader.Read())
            {
                if (counter != 0)
                {
                    var random = new Random();
                    string randomPassword = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                    Voter theVoter = new Voter();
                    //theVoter.IdentityNumber = theMatric;
                    try
                    {
                        theVoter.Department = excelReader.GetString(1).TrimEnd().TrimStart(); ;// theBrokenData[0].TrimEnd().TrimStart();


                        usernameCouner = usernameCouner + 1;
                        theVoter.FirstName = excelReader.GetString(3).TrimEnd().TrimStart();
                        theVoter.Matric = excelReader.GetString(4).TrimEnd().TrimStart();
                        theVoter.IdentityNumber = Convert.ToString(usernameCouner);//usernameCouner + 1; //excelReader.GetValue(5).ToString().TrimEnd().TrimStart();

                        theVoter.Password = randomPassword;
                        theVoter.VotedTime = DateTime.Now;

                        // theVoter.LastName = theBrokenData[5].TrimEnd().TrimStart(); ;
                        theVoter.Voted = false;


                        List<Voter> theVoters = work.VoterRepository.Get(a => a.IdentityNumber == theVoter.IdentityNumber).ToList();
                        if (theVoter.IdentityNumber != null && theVoters.Count() > 0)
                        {
                            string k = "f";
                        }
                        //  theID = WebSecurity.GetUserId(theVoter.IdentityNumber);
                        if (theVoters.Count == 0 && theVoter.FirstName != null && theVoter.IdentityNumber != null && theVoter.Department != null)
                        {
                            work.VoterRepository.Insert(theVoter);

                            Membership.CreateUser(theVoter.IdentityNumber, randomPassword);
                            work.Save();
                            //  WebSecurity.CreateUserAndAccount(theVoter.IdentityNumber, randomPassword);
                        }
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                counter = counter + 1;
            }









            //// int theID = 0;

            //var random = new Random();
            //// string result = new string(Enumerable.Repeat(chars,6).Select(s=>s[random.Next(s.Length)]).ToArray());


            //FileStream fs = new FileStream("C:\\Users\\kazeem\\Desktop\\School Projects\\doctorsreal.txt", FileMode.Open, FileAccess.Read);
            //StreamReader sr = new StreamReader(fs);
            //// Math.
            ////  string theLast = null;
            //// string theMatric = null;
            //while (!(sr.EndOfStream))
            //{
            //    string randomPassword = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());
            //    string theMatric = sr.ReadLine().Trim();
            //    string[] theBrokenData = theMatric.Split('\t');

            //    // theBrokenData[6];

            //    Voter theVoter = new Voter();
            //    //theVoter.IdentityNumber = theMatric;
            //    theVoter.IdentityNumber = theBrokenData[3].TrimEnd().TrimStart();
            //    theVoter.Department = theBrokenData[0].TrimEnd().TrimStart();
            //    theVoter.Password = randomPassword;
            //    theVoter.VotedTime = DateTime.Now;
            //    theVoter.FirstName = theBrokenData[2].TrimEnd().TrimStart(); ;
            //    // theVoter.LastName = theBrokenData[5].TrimEnd().TrimStart(); ;
            //    theVoter.Voted = false;

            //    List<Voter> theVoters = work.VoterRepository.Get(a => a.IdentityNumber == theVoter.IdentityNumber).ToList();
            //    //  theID = WebSecurity.GetUserId(theVoter.IdentityNumber);
            //    if (theVoters.Count == 0)
            //    {
            //        work.VoterRepository.Insert(theVoter);

            //        Membership.CreateUser(theVoter.IdentityNumber, randomPassword);
            //        work.Save();
            //        //  WebSecurity.CreateUserAndAccount(theVoter.IdentityNumber, randomPassword);
            //    }
            //    //  theLast  =theMatric;
            //}
            //sr.Close();
            //fs.Close();
            //  theLast = theMatric;
            // List<Post> theposts = work.PostRepository.Get().ToList();
            // return View("Result", theposts);
            return View();
        }

        //
        // GET: /Voter/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Voter/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Voter/Create

        [HttpPost]
        public ActionResult Create(Voter model)
        {
            model.VotedTime = DateTime.Now;
            model.LoggedInAttemptsAfterVoting = 0;
            // model.Voted = false;
            //  model.
            model.Voted = false;
            try
            {
                if (!(ModelState.IsValid))
                {
                    View(model);
                }
                work.VoterRepository.Insert(model);
                Membership.CreateUser(model.IdentityNumber, model.Password);
                work.Save();

                //  WebSecurity.CreateUserAndAccount(model.IdentityNumber, model.Password);
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        //
        // GET: /Voter/Edit/5

        public ActionResult Reset()
        {
            List<Voter> theVoters = new List<Voter>();

            theVoters = work.VoterRepository.Get(a => a.Voted == true).ToList();
            // Voter theVoter = work.VoterRepository.GetByID(id);

            if (theVoters.Count > 0)
            {
                foreach (var v in theVoters)
                {

                    v.Voted = false;
                    work.VoterRepository.Update(v);
                }

                work.Save();
            }

            return RedirectToAction("Index");
            // return View(theVoter);
        }

        public ActionResult Edit(int id)
        {

            Voter theVoter = work.VoterRepository.GetByID(id);
            return View(theVoter);

        }

        //
        // POST: /Voter/Edit/5

        [HttpPost]
        public ActionResult Edit(Voter theVoter, string newPassword)
        {
            try
            {
                if (TryUpdateModel(theVoter))
                {
                    if (string.IsNullOrEmpty(newPassword))
                    {
                        work.VoterRepository.Update(theVoter);
                        work.Save();
                    }
                    else
                    {
                        if (newPassword.Length < 6)
                        {
                            ModelState.AddModelError("", "Passwrod Must have a minimum of 6 Characters");

                            return View();
                        }
                        else
                        {
                            WebSecurity.ChangePassword(theVoter.IdentityNumber, theVoter.Password, newPassword);
                            theVoter.Password = newPassword;
                            work.VoterRepository.Update(theVoter);
                            work.Save();
                            return RedirectToAction("Index");
                        }
                    }


                }
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Voter/Delete/5

        public ActionResult Delete(int id)
        {
            Voter theVoter = work.VoterRepository.GetByID(id);


            return View(theVoter);
        }
        [Authorize(Roles = "SuperAdmin")]
        public ActionResult DeleteAll()
        {
            try
            {

                List<Voter> allVoters = work.VoterRepository.Get().ToList();
                // Membership.GetUser
                // Membership.DeleteUser(theRealVoter.IdentityNumber, true);

                foreach (Voter voter in allVoters)
                {
                    //voters = voters.Where(a => a.LastName != "Oyebode1234567");
                    //voters = voters.Where(a => a.IdentityNumber != "chair");
                    //voters = voters.Where(a => a.IdentityNumber != "password");
                    Voter theRealVoter = work.VoterRepository.GetByID(voter.VoterID);
                    if (theRealVoter.LastName != "Oyebode1234567" && theRealVoter.IdentityNumber != "chair" && theRealVoter.IdentityNumber != "password")
                    {
                        // ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(theRealVoter.IdentityNumber);

                        Membership.DeleteUser(theRealVoter.IdentityNumber, true);
                        //  DeleteAccount(theRealVoter.IdentityNumber);
                        // ((SimpleMembershipProvider)Membership.Provider).DeleteUser(theRealVoter.IdentityNumber, true);

                        work.VoterRepository.Delete(theRealVoter);
                        //  
                        work.Save();
                    }
                }


                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        //
        // POST: /Voter/Delete/5

        [HttpPost]
        public ActionResult Delete(Voter theVoter)
        {
            try
            {

                Voter theRealVoter = work.VoterRepository.GetByID(theVoter.VoterID);
                // Membership.GetUser
                // Membership.DeleteUser(theRealVoter.IdentityNumber, true);

                try
                {
                    ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(theRealVoter.IdentityNumber);

                    ((SimpleMembershipProvider)Membership.Provider).DeleteUser(theRealVoter.IdentityNumber, true);
                }
                catch (Exception e)
                {
                    work.VoterRepository.Delete(theRealVoter);

                    work.Save();
                }

                work.VoterRepository.Delete(theRealVoter);
                //  
                work.Save();
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

