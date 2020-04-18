using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class StudentNameMatching
    {
        public Student student { get; set; }
        public Matching matching { get; set; }
        public StudentNameMatching(Student s, Matching m)
        {
            student = s;
            matching = m;
        }
    }
}
