//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookAuthor.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthBook
    {
        public int AuthBookID { get; set; }
        public Nullable<int> author_id { get; set; }
        public Nullable<int> book_id { get; set; }
        public string Active { get; set; }
        public string Softdelete { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
