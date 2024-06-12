using Quiz.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.MVVN.Model
{
    public class Quiz1
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz1(string quizName, List<Question> questions,int id)
        {
            QuizName = quizName;
            Questions = questions;
            Id= id;
        }

    }
}
