//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Books
    {
        public int idBook { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int year_of_publication { get; set; }
        public Nullable<int> idPublisher { get; set; }
        public Nullable<int> idAuthor { get; set; }
        public Nullable<int> idCategory { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<int> Pages { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    
        public virtual Authors Authors { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual Publishers Publishers { get; set; }
    }
}
