using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class ViewStatisticsViewModel : BaseViewModel
    {
        public override string Title => "Training Runs";

        public ObservableCollection<RunViewModel> Runs { get; } = new ObservableCollection<RunViewModel>();


        public void Load()
        {
            Runs.Clear();
            foreach(var run in CommonServices.Instance.Database.Runs
                .Include(item => item.Items)
                .Include(item => item.Lesson)
                .OrderByDescending(item => item.StartDate)
                )
            {
                Runs.Add(new RunViewModel(run));
            }
        }
    }
}
