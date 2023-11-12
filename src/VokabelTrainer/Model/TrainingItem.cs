using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{
    public class TrainingItem
    {
        public WordItem Word { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime TrainingDate { get; set; }
    }
}