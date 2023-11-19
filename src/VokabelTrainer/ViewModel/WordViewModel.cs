using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;

namespace VokabelTrainer.ViewModel
{
    public class WordViewModel : BaseViewModel<WordItem>, IEquatable<WordViewModel>, IComparable<WordViewModel>
    {
        public int Id => Model.Id;
        public string OwnWord => Model.OwnWord;
        public string ForeignWord => Model.ForeignWord;
        public string Info => Model.Info;

        public WordViewModel(WordItem model) : base(model)
        {
        }

        public bool Equals(WordViewModel other)
        {
            return this.Id.Equals(other?.Id);
        }

        public override int GetHashCode() => this.Id.GetHashCode();

        public int CompareTo(WordViewModel other)
        {
            return this.Id.CompareTo(other?.Id);
        }
    }
}
