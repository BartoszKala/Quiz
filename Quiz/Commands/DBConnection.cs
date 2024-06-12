using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Quiz.Commands
{
    class DBConnection
    {
        private SQLiteConnectionStringBuilder stringBuilder = new SQLiteConnectionStringBuilder();

        private static DBConnection instance = null;
        public static DBConnection Instance
        {
            get
            {
                if (instance == null)
                    instance = new DBConnection();
                return instance;
            }
        }

        private SQLiteConnection connection;
        public SQLiteConnection Connection => connection;

        private DBConnection()
        {
            stringBuilder.DataSource = "C:\\Users\\lenovo\\Desktop\\studia\\c#\\Quiz\\Quiz\\questions.db";

            connection = new SQLiteConnection(stringBuilder.ToString());
        }
    }
}


