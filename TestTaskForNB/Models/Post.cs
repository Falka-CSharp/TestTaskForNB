using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestTaskForNB.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string PostText { get; set; } = string.Empty;
        [Required]
        public DateTime CreatingDate { get; set; }
        [Required]
        public string PostRubrics { get; set; } = string.Empty;
    }
}
