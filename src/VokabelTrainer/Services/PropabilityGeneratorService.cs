using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VokabelTrainer.Model;
using VokabelTrainer.Model.Api;

namespace VokabelTrainer.Services
{
    public class PropabilityGeneratorService : IPropabilityGenerator
    {
        private Lesson _currentLesson = null;
        private WordItem[] _words = null;
        private Random _rand = null;



        public void LoadFor(Lesson lesson)
        {
            _currentLesson = lesson;
            if(lesson != null)
            {
                _words = lesson.Words.ToArray();
                _rand = new Random((int)(DateTime.Now.Ticks % Int32.MaxValue));
            } 
            else
            {
                _words = null;
                _rand = null;
            }
        }

        public WordItem GetNextWord()
        {
            if (_words != null)
            {
                int randomIndex = _rand.Next(0, _words.Length - 1);
                return _words[randomIndex];
            } 
            else
            {
                return null;
            }
        }
    }
}
