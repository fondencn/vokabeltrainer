using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class ViewStatisticsViewModel : BaseViewModel
    {
        private RelayCommand _loadCommand;
        private ObservableCollection<RunViewModel> _runs;
        private bool _IsBusy;


        public ViewStatisticsViewModel()
        {
            AddPropertyChangedHandler(nameof(StatisticResult), () => OnPropertyChanged(nameof(HasResults)));
            AddPropertyChangedHandler(nameof(IsBusy), () => _loadCommand.RaiseCanExecuteChanged());
        }

        private StatisticResultViewModel _StatisticResult = null;
        private StatisticFilterViewModel _Filter = new StatisticFilterViewModel();


        public override string Title => "Training Runs";


        public ObservableCollection<RunViewModel> Runs { get => _runs; private set => SetProperty(ref _runs, value); }


        public StatisticFilterViewModel Filter
        {
            get { return _Filter; }
            set { SetProperty(ref _Filter, value); }
        }


        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }


        public StatisticResultViewModel StatisticResult
        {
            get { return _StatisticResult; }
            set { SetProperty(ref _StatisticResult, value); }
        }

        public bool HasResults => StatisticResult != null;


        public ICommand LoadCommand
        {
            get
            {
                if (_loadCommand == null)
                {
                    _loadCommand = new RelayCommand(CanLoad, Load);
                }
                return _loadCommand;
            }
        }

        public bool CanLoad() => !IsBusy;

        public async void Load()
        {
            if (!this.IsBusy)
            {
                this.IsBusy = true;
                this.Runs = null;
                this.StatisticResult = null;
                List<RunViewModel> runs = null;

                await Task.Run(() =>
                {
                    runs = CommonServices.Instance.Database.Runs
                        .Where(item =>
                            item.StartDate >= Filter.StartDate && item.EndDate <= Filter.EndDate && (Filter.Lesson == null ? true : item.Id_Lesson == Filter.Lesson.Id)
                            )
                        .Include(item => item.Items)
                        .Include(item => item.Lesson)
                        .OrderByDescending(item => item.StartDate)
                        .Select(item => new RunViewModel(item))
                        .ToList();
                });


                this.StatisticResult = await UpdateStatistics(runs);
                if (runs.Any())
                {
                    this.Runs = new ObservableCollection<RunViewModel>(runs);
                }
                this.IsBusy = false;
            }
        }

        private static async Task<StatisticResultViewModel> UpdateStatistics(List<RunViewModel> runs)
        {
            StatisticResultViewModel result = null;
            await Task.Run(() =>
            {
                result = new StatisticResultViewModel();
                if (runs.Any())
                {
                    result.Duration = TimeSpan.FromSeconds(runs.Sum(item => item.Model.EndDate < item.Model.StartDate ? 1 : (item.Model.EndDate - item.Model.StartDate).TotalSeconds));
                    result.WordCount = runs.Sum(item => (item.Count));
                    result.PercentCorrect = runs.Average(item => item.Model.PercentCorrect);
                }
            });

            return result;
        }
    }

    public class StatisticFilterViewModel : BaseViewModel
    {
        private RelayCommand _resetCommand;
        private DateTime _startDate;
        private DateTime _endDate;
        private LessonViewModel _lesson;


        public StatisticFilterViewModel()
        {
            Reset();
        }


        public LessonViewModel[] AllLessons => CommonServices.Instance.Database.Lessons.Select(item => new  LessonViewModel(item)).ToArray();


        public ICommand ResetCommand
        {
            get
            {
                if (_resetCommand == null)
                {
                    _resetCommand = new RelayCommand(Reset);
                }
                return _resetCommand;
            }
        }

        public void Reset()
        {
            this.StartDate = DateTime.Today.AddDays(-1); //gestern
            this.EndDate = DateTime.Today.AddDays(1); //morgen 0.00 Uhr
            this.Lesson = null;
        }

        public DateTime StartDate { get => _startDate; set => SetProperty(ref _startDate, value); }
        public DateTime EndDate { get => _endDate; set => SetProperty(ref _endDate, value); }
        public LessonViewModel Lesson { get => _lesson; set => SetProperty(ref _lesson, value); }
    }

    public class StatisticResultViewModel : BaseViewModel 
    {
        public int WordCount { get; set; }
        public double PercentCorrect { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
