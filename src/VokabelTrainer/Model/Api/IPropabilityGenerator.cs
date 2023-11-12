using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VokabelTrainer.Model.Api
{
    public interface IPropabilityGenerator
    {
        double Generate(WordItem word);
    }
}