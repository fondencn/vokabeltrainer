using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{
    public class Lesson
    {
        public string Name { get; set; }
        public IList<WordItem> Items { get; } = new List<WordItem>();
    }
}