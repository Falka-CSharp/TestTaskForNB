using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestTaskForNB.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PostText { get; set; } = string.Empty;
        [Required]
        public DateTime PostCreatingDate { get; set; }
        [Required]
        public virtual List<Rubric>? PostRubrics { get; set; }
    }
}
