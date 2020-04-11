using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class PostCompany
    {
        public Company company { get; set; }
        public Post post { get; set; }
        public PostCompany(Company c, Post p)
        {
            company = c;
            post = p;
        }
    }
}
