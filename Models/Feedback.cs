using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaseIt.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public string UserEmail { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
