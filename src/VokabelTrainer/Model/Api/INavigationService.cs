﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VokabelTrainer.Model.Api
{
    public interface INavigationService
    {
        Task Navigate<T>() where T : Page;

        Task Back();
    }
}
