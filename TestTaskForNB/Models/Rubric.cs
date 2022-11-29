using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskForNB.Models
{
    public class Rubric
    {
        public int Id { get; set; }
        [Required]
        public string RubricName { get; set; } = string.Empty;
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
