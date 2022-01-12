using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Models
{
    public class Books
    {
        public int Id { get; set; } 
        public string title { get; set; }
        public string Description { get; set; }

        //public Books()
        //{
        //    Id = new Guid().ToString();
        //}
    }
}
