using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{

    [Table("TrainingRuns")]
    public class TrainingRun
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Lesson")]
        public int Id_Lesson { get; set; }

        public Lesson Lesson { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IList<TrainingItem> Items { get; set; } = new List<TrainingItem>();

        [NotMapped]
        public int Count => Items?.Count ?? 0;

        [NotMapped]
        public double PercentCorrect => Count > 0 ? (double)(Items.Where(item => item.IsCorrect).Count() / (double)Count) : 0;
    }
}