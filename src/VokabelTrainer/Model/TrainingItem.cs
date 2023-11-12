using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{

    [Table("TrainingItems")]
    public class TrainingItem
    {
        public int Id_TrainingRun { get; set; }
        public int Id_Word { get; set; }
        [NotMapped]
        public WordItem Word { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime TrainingDate { get; set; }
    }
}