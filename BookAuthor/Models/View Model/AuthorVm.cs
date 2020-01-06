using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookAuthor.Models.View_Model
{
    public class AuthorVm
    {

        public int Author_id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Nullable<System.DateTime> Date_Of_Birth { get; set; }
        public string Active { get; set; }
        public string Softdelete { get; set; }

        public virtual Book Book { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthBook> AuthBooks { get; set; }

    }
}