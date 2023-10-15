using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class ConnectionWithServer
    {
        private static MySqlConnection connection;
        private static MySqlConnection GetInstance()
        {
            if(connection == null)
            {
                connection = new MySqlConnection("Server=;Port=;Database=;Uid=;Pwd=");
            }
            return connection;
        }

        public object SelectUsedExecuteScalarMethod(string queryStatement)
        {
            MySqlCommand command = new MySqlCommand(queryStatement, GetInstance());
            GetInstance().Open();
            object readedData = command.ExecuteScalar();
            GetInstance().Close();
            return readedData;
        }

        public MySqlDataReader SelectUsedExecuteReader(string queryStatement) 
        {
            MySqlCommand command = new MySqlCommand(queryStatement, GetInstance());
            GetInstance().Open();
            MySqlDataReader readedData = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return readedData;
        }

        public void CreateUpdateDelete(string queryStatement) //Insert,Update, delete 하는 함수
        {
            MySqlCommand command = new MySqlCommand(queryStatement, GetInstance());
            GetInstance().Open();
            command.ExecuteNonQuery();
            GetInstance().Close();
        }
    }

}
