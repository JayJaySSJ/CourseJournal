﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.Domain.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }  
              
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

    }
}
