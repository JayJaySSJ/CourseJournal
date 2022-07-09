using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client.Models
{
    public class HomeworkResult
    {
      
            public int Id { get; set; }
            public string NameHomework { get; set; }

            public DateTime ReturnDate { get; set; }

            public int Result { get; set; }

            public int StudentId { get; set; }

            public int CourseId { get; set; }
        
    }
}
