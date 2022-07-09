using System;
using System.Collections.Generic;

namespace CourseJournal.TrainerApp.Client.Models
{
    public class TestResults
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public int CourseId { get; set; }
        public List<StudentsResults> StudentsResults { get; set; }
    }
}
