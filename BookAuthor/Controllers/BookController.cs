using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookAuthor.Models;

namespace BookAuthor.Controllers
{
    public class BookController : Controller
    {
        BookAuthorEntities EntDb = new BookAuthorEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var Book = EntDb.Books.Where(p => p.Active == "Y" && p.Softdelete == "N");

            return View(Book);
        }

        public ActionResult Create()
          {
            var AuthorData = EntDb.Authors.Where(p => p.Active == "Y" && p.Softdelete == "N");
            ViewData["Authors"] = AuthorData;

            //var AuthorNames = EntDb.Authors.Where(p => p.Active == "Y" && p.Softdelete == "N");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book BookEnt, int[] SelectedAuthors)
        {
            if (SelectedAuthors == null)
            {
                Book BkTblOBJ = new Book();

                BkTblOBJ.Book_title = BookEnt.Book_title;
                BkTblOBJ.Active = "Y";
                BkTblOBJ.Softdelete = "N";
                EntDb.Books.Add(BkTblOBJ);
                EntDb.SaveChanges();
            }

            else
            {
                Book BkTblOBJ = new Book();

                BkTblOBJ.Book_title = BookEnt.Book_title;
                BkTblOBJ.Active = "Y";
                BkTblOBJ.Softdelete = "N";
                EntDb.Books.Add(BkTblOBJ);
                EntDb.SaveChanges();

                var saveBook = EntDb.Books.Select(p => p).OrderByDescending(p => p.Book_id).FirstOrDefault();

                AuthBook AuthBk = new AuthBook();

                foreach (int ID in SelectedAuthors)
                {
                    AuthBk.author_id = ID;
                    AuthBk.book_id = saveBook.Book_id;
                    EntDb.AuthBooks.Add(AuthBk);
                    EntDb.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(Int64 Id)
        {
            Book book = EntDb.Books.Find(Id);

         //   var AuthorBkIDs = EntDb.AuthBooks.Select(p => p.book_id).ToList();

            //if (AuthorBkIDs.Contains(Convert.ToInt32(Id)))
            //{

                var data = from b in EntDb.Authors
                           join h in EntDb.AuthBooks
                           on b.Author_id equals h.author_id
                           join c in EntDb.Books
                           on h.book_id equals c.Book_id
                           where h.book_id == book.Book_id
                           select b;
                ViewBag.result = data;
               
                var AuthorIds = EntDb.Authors.Where(p => p.Active == "Y" && p.Softdelete == "N");
                ViewBag.Authors = AuthorIds;
          //  }

            //else
            //{
            //    var data = from b in EntDb.Authors
            //               join h in EntDb.AuthBooks
            //               on b.Author_id equals h.author_id
            //               join c in EntDb.Books
            //               on h.book_id equals c.Book_id
            //               where h.book_id == book.Book_id
            //               select b;
            //    ViewBag.result = data;

            //    var AuthorIds = EntDb.Authors.Where(p => p.Active == "Y" && p.Softdelete == "N");
            //    ViewBag.Authors = AuthorIds;
            //}

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book bookEdt, int[] SelectedAuthors)
        {
            if (SelectedAuthors == null)
            {
                Book EdtBk = EntDb.Books.Find(bookEdt.Book_id);

                EdtBk.Book_title = bookEdt.Book_title;
                EdtBk.Active = "Y";
                EdtBk.Softdelete = "N";
                EntDb.Books.Attach(EdtBk);
                EntDb.Entry(EdtBk).State = System.Data.Entity.EntityState.Modified;
                EntDb.SaveChanges();
            }

            else
            {
                Book EdtBk = EntDb.Books.Find(bookEdt.Book_id);

                EdtBk.Book_title = bookEdt.Book_title;
                EdtBk.Active = "Y";
                EdtBk.Softdelete = "N";
                EntDb.Books.Attach(EdtBk);
                EntDb.Entry(EdtBk).State = System.Data.Entity.EntityState.Modified;
                EntDb.SaveChanges();

                var Booklst = EntDb.AuthBooks.Where(p => p.book_id == bookEdt.Book_id).ToList();

                var AuthBk = from a in EntDb.AuthBooks
                             where a.book_id == bookEdt.Book_id
                             select a.AuthBookID;

                foreach (int Item in AuthBk.ToList())
                {
                    AuthBook AuthBkrc = EntDb.AuthBooks.Find(Item);
                    EntDb.AuthBooks.Remove(AuthBkrc);
                    EntDb.SaveChanges();
                }

                foreach (var ID in SelectedAuthors)
                {
                    AuthBook AuthorBK = new AuthBook();
                    AuthorBK.author_id = ID;
                    AuthorBK.book_id = bookEdt.Book_id;
                    EntDb.Entry(AuthorBK).State = System.Data.Entity.EntityState.Added;
                    EntDb.SaveChanges();
                }

            }
               return RedirectToAction("Index");

            //Nested For Each Loop Code
            //var Booklst = EntDb.AuthBooks.Where(p => p.book_id == bookEdt.Book_id).ToList();
            //foreach (var item in Booklst)
            ////   foreach (int ID in SelectedAuthors)
            //{
            //    AuthBook AuthBk = EntDb.AuthBooks.Find(item.AuthBookID);
            //    foreach (int ID in SelectedAuthors)
            //    {
            //        AuthBk.author_id = ID;
            //        break;
            //    }
            //    AuthBk.book_id = bookEdt.Book_id;
            //    EntDb.AuthBooks.Attach(AuthBk);
            //    EntDb.Entry(AuthBk).State = System.Data.Entity.EntityState.Modified;
            //    EntDb.SaveChanges();
            //}
        }

       // [HttpPost]
       //Book Delete
        public ActionResult DeleteBook(Int64 id)
        {
            Book BookDel = EntDb.Books.Find(id);

            BookDel.Active = "N";
            BookDel.Softdelete = "Y";

            EntDb.Books.Attach(BookDel);
            EntDb.Entry(BookDel).State = System.Data.Entity.EntityState.Modified;
            EntDb.SaveChanges();

            return RedirectToAction("Index");
        }
    }


}

