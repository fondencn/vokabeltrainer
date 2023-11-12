using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model
{
    public class WordItem
    {
        public int Id { get; set; }
        public string  OwnWord { get; set; }
        public string  ForeignWord { get; set; }
        public string Info { get; set; }
    }
}