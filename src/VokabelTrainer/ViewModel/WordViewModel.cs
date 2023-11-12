using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;

namespace VokabelTrainer.ViewModel
{
    public class WordViewModel : BaseViewModel<WordItem>
    {
        public string OwnWord => Model.OwnWord;
        public string ForeignWord => Model.ForeignWord;
        public string Info => Model.Info;

        public WordViewModel(WordItem model) : base(model)
        {
        }
    }
}
