using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{
    [Table("Lessons")]
    public class Lesson
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Name { get; set; }

        public IList<WordItem> Words { get; } = new List<WordItem>();
    }
}