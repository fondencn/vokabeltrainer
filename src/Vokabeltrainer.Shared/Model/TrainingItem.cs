using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{

    [Table("TrainingItems")]
    public class TrainingItem
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TrainingRuns")]
        public int Id_TrainingRun { get; set; }

        [ForeignKey("Words")]
        public int Id_Word { get; set; }
        [NotMapped]
        public WordItem Word { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime TrainingDate { get; set; }
    }
}