using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookAuthor.Models;

namespace BookAuthor.Controllers
{
    public class AuthorDetailsController : ApiController
    {
        BookAuthorEntities EntDb = new BookAuthorEntities();
        
        [HttpGet]
        public IHttpActionResult GetAuthorList()
        {
            BookAuthorEntities EntDb = new BookAuthorEntities();

            List<Author> Author = EntDb.Authors.ToList();
            List<Book> Book = EntDb.Books.ToList();
            List<AuthBook> AuthBook = EntDb.AuthBooks.ToList();

           var emploeeRecord = from a in Author
                                join b in AuthBook on a.Author_id equals b.author_id into table1
                                from b in table1.ToArray()
                                join c in Book on b.book_id equals c.Book_id into table2
                                from c in table2.ToArray()
                                select new
                                {
                                   // AuthBook = b.author_id,b.book_id,
                                    Author = a.First_Name,a.Last_Name,a.Date_Of_Birth,
                                    Book = c.Book_title,
                                };
                  
            return Ok(emploeeRecord);

            //return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult GetAuthorByID(int ID)
        {
            BookAuthorEntities EntDb = new BookAuthorEntities();

            List<Author> Author = EntDb.Authors.ToList();
            List<Book> Book = EntDb.Books.ToList();
            List<AuthBook> AuthBook = EntDb.AuthBooks.ToList();

            var emploeeRecord = from a in Author
                                join b in AuthBook on a.Author_id equals b.author_id into table1
                                from b in table1.ToArray()
                                join c in Book on b.book_id equals c.Book_id into table2
                                from c in table2.ToArray()
                                where b.author_id.Equals(ID)
                                select new
                                {
                                    // AuthBook = b.author_id,b.book_id,
                                    Author = a.First_Name,
                                    a.Last_Name,
                                    a.Date_Of_Birth,
                                    Book = c.Book_title,
                                };

            return Ok(emploeeRecord);

          
        }

       
    }

   }


