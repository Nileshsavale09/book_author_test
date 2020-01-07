using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAuthor.Models;

namespace BookAuthor.Controllers
{
    public class AuthorController : Controller
    {
        BookAuthorEntities EntDb = new BookAuthorEntities();
        // Author Index
        // GET: Author
        [HttpGet]
        public ActionResult Index()
        {
            var Auth = EntDb.Authors.Where(P => P.Active == "Y" && P.Softdelete == "N");

            return View(Auth);
        }

        public ActionResult Create()
        {
            return View();
        }
        
        // Author Post
        [HttpPost]
        public ActionResult Create(Author authorEnt)
        {
            Author authorsTblObj = new Author();

            authorsTblObj.First_Name = authorEnt.First_Name;
            authorsTblObj.Last_Name = authorEnt.Last_Name;
            authorsTblObj.Date_Of_Birth = authorEnt.Date_Of_Birth;
            authorsTblObj.Active = "Y";
            authorsTblObj.Softdelete = "N";

            EntDb.Authors.Add(authorsTblObj);
            EntDb.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Int64 Id)
        {
            Author author = EntDb.Authors.Find(Id);

            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Author AuthorEdt)
        {
            Author EdtAuth = EntDb.Authors.Find(AuthorEdt.Author_id);

            EdtAuth.First_Name = AuthorEdt.First_Name;
            EdtAuth.Last_Name = AuthorEdt.Last_Name;
            EdtAuth.Date_Of_Birth = AuthorEdt.Date_Of_Birth;
            EdtAuth.Active = "Y";
            EdtAuth.Softdelete = "N";

            EntDb.Authors.Attach(EdtAuth);
            EntDb.Entry(EdtAuth).State = System.Data.Entity.EntityState.Modified;
            EntDb.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Details(Int64 Id)
        {
            Author DtlsAuth = EntDb.Authors.Find(Id);
            return View(DtlsAuth);
        }

        public ActionResult Delete(Int64 id)
        {
            Author DelAuth = EntDb.Authors.Find(id);

            DelAuth.Active = "N";
            DelAuth.Softdelete = "Y";

            //  EntDb.Authors.Add(DelAuth);

            EntDb.Authors.Attach(DelAuth);
            EntDb.Entry(DelAuth).State = System.Data.Entity.EntityState.Modified;
            EntDb.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}