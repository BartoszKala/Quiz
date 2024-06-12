using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.MVVN.Model;

namespace Quiz.Model
{
    public class Question
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public int Time { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(int questionNumber, string questionText, List<Answer> answers, int time)
        {
            QuestionNumber = questionNumber;
            QuestionText = questionText;
            Time = time;
            Answers = answers;
        }


        public Question(SQLiteDataReader reader)
        {
            var answers = new List<Answer>
            {
                new Answer(reader["answerA"].ToString(), false),
                new Answer(reader["answerB"].ToString(), false),
                new Answer(reader["answerC"].ToString(), false),
                new Answer(reader["answerD"].ToString(), false)
            };

            switch (reader["correct"].ToString())
            {
                case "A":
                    answers[0].ChangeToCorrect();
                    break;
                case "B":
                    answers[1].ChangeToCorrect();
                    break;
                case "C":
                    answers[2].ChangeToCorrect();
                    break;
                case "D":
                    answers[3].ChangeToCorrect();
                    break;
                default:
                    break;
            }

            QuestionNumber = int.Parse(reader["question_id"].ToString());
            QuestionText = reader["question"].ToString();
            Time = int.Parse(reader["time"].ToString());
            Answers = answers;

        }
    }
}
