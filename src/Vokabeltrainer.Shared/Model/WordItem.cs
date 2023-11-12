using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{

    [Table("Words")]
    public class WordItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string OwnWord { get; set; }
        public string ForeignWord { get; set; }
        public string Info { get; set; }
    }
}