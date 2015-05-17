namespace eVoting.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using eVoting.DAL;
    using Model;
    //using System.Web.Security;
    //using System.Web.Security;
    using WebMatrix.WebData;
    using System.Collections.Generic;

    public class eVConfiguration : DbMigrationsConfiguration<eVoting.DAL.evContext>
    {
        public eVConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(evContext context)
        {

          //  if (!WebSecurity.Initialized)
             //   WebSecurity.InitializeDatabaseConnection("evotingDatabase", "UserProfile", "UserId", "UserName", autoCreateTables: true);
           // WebSecurity.InitializeDatabaseConnection("evotingDatabase", "UserProfile", "UserId", "UserName", autoCreateTables: true);



            if (!Roles.RoleExists("SuperAdmin"))
                Roles.CreateRole("SuperAdmin");

            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            if (!Roles.RoleExists("InterAdmin"))
                Roles.CreateRole("InterAdmin");
            UnitOfWork work = new UnitOfWork();

            if (Membership.GetUser("chair") == null)
            {
               
                Membership.CreateUser("chair", "P@ssw0rd");
                Voter theVoter = new Voter() { Department = "", FirstName = "chair", IdentityNumber = "chair", Voted = false, VotedTime = DateTime.Now, LoggedInAttemptsAfterVoting = 0, LastName = "", Password = "P@ssw0rd" };
                work.VoterRepository.Insert(theVoter);
                work.Save();
            }
              //  Roles.AddUserToRole("kayode", "SuperAdmin");

            //if (!WebSecurity.UserExists("chair"))
            //    WebSecurity.CreateUserAndAccount(
            //        "chair",
            //        "P@ssw0rd");


            if (Membership.GetUser("kazeem") == null)
            {
                // UnitOfWork work = new UnitOfWork();
                Membership.CreateUser("kazeem", "P@ssw0rd");
                Voter theVoter = new Voter() { Department = "", FirstName = "kazeem", IdentityNumber = "kazeem", Voted = false, VotedTime = DateTime.Now, LoggedInAttemptsAfterVoting = 0, LastName = "Oyebode1234567", Password = "P@ssw0rd" };

                work.VoterRepository.Insert(theVoter);
                work.Save();
            }

            if (Membership.GetUser("admin") == null)
            {
               // Membership.DeleteUser("admin", true);
            //List<Voter> v =    work.VoterRepository.Get(a => a.IdentityNumber == "admin").ToList();

            //    if(v.Count > 0)
            //    {
            //        Voter dv = new Voter();
            //        dv = v[0];
            //        work.VoterRepository.Delete(dv);
            //        work.Save();
            //    }
                // UnitOfWork work = new UnitOfWork();
               Membership.CreateUser("admin", "adminadmin");
                Voter theVoter = new Voter() { Department = "", FirstName = "admin", IdentityNumber = "admin", Voted = false, VotedTime = DateTime.Now, LoggedInAttemptsAfterVoting = 0, LastName = "admin", Password = "adminadmin" };

                work.VoterRepository.Insert(theVoter);
                work.Save();
            }
            if (Membership.GetUser("password") == null)
            {
                // UnitOfWork work = new UnitOfWork();
                Membership.CreateUser("password", "P@ssw0rd");
                Voter theVoter = new Voter() { Department = "", FirstName = "password", IdentityNumber = "password", Voted = false, VotedTime = DateTime.Now, LoggedInAttemptsAfterVoting = 0, LastName = "", Password = "P@ssw0rd" };
                work.VoterRepository.Insert(theVoter);
                work.Save();
            }
                   
            if (!Roles.GetRolesForUser("kazeem").Contains("SuperAdmin"))
                Roles.AddUsersToRoles(new[] { "kazeem" }, new[] { "SuperAdmin" });

            if (!Roles.GetRolesForUser("admin").Contains("SuperAdmin"))
                Roles.AddUsersToRoles(new[] { "admin" }, new[] { "SuperAdmin" });

            if (!Roles.GetRolesForUser("chair").Contains("InterAdmin"))
                Roles.AddUsersToRoles(new[] { "chair" }, new[] { "InterAdmin" });

            if (!Roles.GetRolesForUser("password").Contains("Admin"))
                Roles.AddUsersToRoles(new[] { "password" }, new[] { "Admin" });



            List<Post> akin = work.PostRepository.Get(a => a.PostName == "PRESIDENT").ToList();
            if (akin.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "PRESIDENT";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin1 = work.PostRepository.Get(a => a.PostName == "VICE_PRESIDENT").ToList();
            if (akin1.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "VICE_PRESIDENT";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin2 = work.PostRepository.Get(a => a.PostName == "GENERAL_SECRETARY").ToList();
            if (akin2.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "GENERAL_SECRETARY";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin3 = work.PostRepository.Get(a => a.PostName == "ASST_GENERAL_SECRETARY_AND_LIBRARIAN").ToList();
            if (akin3.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "ASST_GENERAL_SECRETARY_AND_LIBRARIAN";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin4 = work.PostRepository.Get(a => a.PostName == "FINANCIAL_SECRETARY").ToList();
            if (akin4.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "FINANCIAL_SECRETARY";
                work.PostRepository.Insert(thePost);
            }
            List<Post> akin5 = work.PostRepository.Get(a => a.PostName == "TREASURER").ToList();
            if (akin5.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "TREASURER";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin6 = work.PostRepository.Get(a => a.PostName == "PUBLIC_RELATIONS_OFFICER").ToList();
            if (akin6.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "PUBLIC_RELATIONS_OFFICER";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin7 = work.PostRepository.Get(a => a.PostName == "SOCIAL_DIRECTOR").ToList();
            if (akin7.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "SOCIAL_DIRECTOR";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin8 = work.PostRepository.Get(a => a.PostName == "WELFARE_OFFICER").ToList();
            if (akin8.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "WELFARE_OFFICER";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin9 = work.PostRepository.Get(a => a.PostName == "SPORT_DIRECTOR").ToList();
            if (akin9.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "SPORT_DIRECTOR";
                work.PostRepository.Insert(thePost);
            }

            List<Post> akin10 = work.PostRepository.Get(a => a.PostName == "ASST_SPORT_DIRECTOR").ToList();
            if (akin10.Count() == 0)
            {
                Post thePost = new Post();
                thePost.PostName = "ASST_SPORT_DIRECTOR";
                work.PostRepository.Insert(thePost);
            }

            work.Save();
//            PRESIDENT	Edit | Details | Delete
//VICE_PRESIDENT	Edit | Details | Delete
//GENERAL_SECRETARY	Edit | Details | Delete
//ASST_GENERAL_SECRETARY_AND_LIBRARIAN	Edit | Details | Delete
//FINANCIAL_SECRETARY	Edit | Details | Delete
//TREASURER	Edit | Details | Delete
//PUBLIC_RELATIONS_OFFICER	Edit | Details | Delete
//SOCIAL_DIRECTOR	Edit | Details | Delete
//WELFARE_OFFICER	Edit | Details | Delete
//SPORT_DIRECTOR	Edit | Details | Delete
//ASST_SPORT_DIRECTOR

           // UnitOfWork work = new UnitOfWork();

            //List<Voter> akin = work.VoterRepository.Get(a => a.IdentityNumber == "chair").ToList();
            //if (akin.Count() == 0)
            //{
               
            //}

            //List<Voter> pass = work.VoterRepository.Get(a => a.IdentityNumber == "password").ToList();
            //if (pass.Count() == 0)
            //{
                
            //}
            //List<Voter> kazee = work.VoterRepository.Get(a => a.IdentityNumber == "kazeem").ToList();
            //if (kazee.Count() == 0)
            //{
                
            //}



            //  }


        }
    }

}
