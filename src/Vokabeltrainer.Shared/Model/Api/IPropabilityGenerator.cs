using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model.Api
{
    public interface IPropabilityGenerator
    {
        public void LoadFor(Lesson lesson);
        WordItem GetNextWord();
    }
}