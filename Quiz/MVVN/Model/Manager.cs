using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Model;
using Quiz.MVVN.Model;

namespace Quiz.MVVN.Model
{
    class Manager
    {
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();

        public Manager()
        {
            var quiz2 = Repozytory.PobierzDane();

            List<Answer> a = new List<Answer>
                         {
                new Answer { AnswerText = "", IsCorrect = true },
                new Answer { AnswerText = "", IsCorrect = false },
                new Answer { AnswerText = "", IsCorrect = false },
                new Answer { AnswerText = "", IsCorrect = false }
                          };

            var example = new Question(0, "",a,0);

  
           Questions.Add(example);



            foreach (var o in quiz2)
            {
                Questions.Add(o);
            }
        }


        public Question FindQuestion(int id)
        {
            foreach (var t in Questions)
            {
                if (t.QuestionNumber == id)
                    return t;
            }
            return null;
        }

        public bool CheckCorrectness(List<Answer> list)
        {
            foreach (var answer in list)
            {
                if ((bool)!answer.IsCorrect)
                {
                    return false;
                }
            }
            return true;
        }




    }
}

