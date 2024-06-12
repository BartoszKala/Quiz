using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz.Commands;
using Quiz.Model;

namespace Quiz.MVVN.Model
{
    static class Repozytory
    {
        private const string WSZYSTKO = "SELECT * FROM questions";

        public static List<Question> PobierzDane()
        {
            List<Question> questions = new List<Question>();

            using (var connection = DBConnection.Instance.Connection)
            {
                SQLiteCommand command = new SQLiteCommand(WSZYSTKO, connection);
                connection.Open();
                var reader = command.ExecuteReader();


                while (reader.Read())
                    questions.Add(new Question(reader));
                connection.Close();
            }


            Random rnd = new Random();
            questions = questions.OrderBy(q => rnd.Next()).ToList();


            return questions;
        }
    }
}
