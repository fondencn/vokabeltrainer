using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{
    public class TrainingRun
    {
        public int Id { get; set; }
        public Lesson Lesson { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public IList<TrainingItem> Items { get; set; }

        public int Count => Items?.Count ?? 0;

        public double PercentCorrect => Count > 0 ? (Items.Where(item => item.IsCorrect).Count() / Count) : 0;
    }
}