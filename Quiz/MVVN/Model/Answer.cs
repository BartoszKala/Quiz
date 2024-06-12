using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.MVVN.Model
{
   public  class Answer
    {
        public string AnswerText { get; set; }
        public bool? IsCorrect { get; set; }

        public void ChangeToCorrect()
        {
            IsCorrect=true;
        }
        public Answer()
        { }
        public Answer(string answerText, bool? isCorrect)
        {
            this.AnswerText = answerText;
            this.IsCorrect = isCorrect;
        }

        public Answer(Answer a)
        {
            AnswerText = a.AnswerText;
            IsCorrect =false;
        }       

  

    }
}
