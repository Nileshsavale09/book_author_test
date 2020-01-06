using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookAuthor.Models.View_Model
{
    public class BookVM
    {

        public int Book_id { get; set; }
        public string Book_title { get; set; }
        public string Active { get; set; }
        public string Softdelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthBook> AuthBooks { get; set; }

        public virtual ICollection<Author> Authorlist { get; set; }
    }
}