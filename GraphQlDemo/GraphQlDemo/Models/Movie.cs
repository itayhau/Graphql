using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlDemo.Models
{
    public class Movie
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Release_date { get; set; }
        public double Price { get; set; }
        public long Country_id { get; set; }

    }
}
