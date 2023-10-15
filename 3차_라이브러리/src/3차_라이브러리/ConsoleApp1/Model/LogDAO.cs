using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class LogDAO
    {
        public ConnectionWithServer connectionWithServer;

        public LogDAO()
        {
            this.connectionWithServer = new ConnectionWithServer();
        }

        public void AddLogInData(string logTime, string logUSer, string logInformation, string logAction)
        {
            string queryStatement = string.Format("INSERT INTO log_data (logTime, logUser, logInformation, logAction) VALUES ('{0}', '{1}','{2}','{3}');", logTime, logUSer, logInformation, logAction);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public List<LogDTO> ReadAllLogData()
        {
            LogDTO logDTO;
            List<LogDTO> allLogList = new List<LogDTO>();  //모든 로그 정보를 담을 리스트 선언

            string queryStatement = "SELECT * FROM log_data ;";
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                logDTO = new LogDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                logDTO.LogNumber = readedData.GetInt32(0);
                logDTO.LogTime = readedData.GetString(1);
                logDTO.LogUser = readedData.GetString(2);
                logDTO.LogInformation = readedData.GetString(3);
                logDTO.LogAction = readedData.GetString(4);

                allLogList.Add(logDTO);//로그 리스트에 추가
            }
            readedData.Close();
            return allLogList; //모든 로그가 담겨 있는 리스트 전달
        }
        
        public List<LogDTO> FindLogByLogId(string logNumber)
        {
            LogDTO logDTO;
            List<LogDTO> equalIdLogList = new List<LogDTO>();  //해당 로그 정보를 담을 리스트 선언

            string queryStatement = string.Format ("SELECT * FROM log_data WHERE logNumber = '{0}';", logNumber);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                logDTO = new LogDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                logDTO.LogNumber = readedData.GetInt32(0);
                logDTO.LogTime = readedData.GetString(1);
                logDTO.LogUser = readedData.GetString(2);
                logDTO.LogInformation = readedData.GetString(3);
                logDTO.LogAction = readedData.GetString(4);

                equalIdLogList.Add(logDTO);//로그 리스트에 추가
            }
            readedData.Close();
            return equalIdLogList; //아이디가 일치하는 로그가 담겨 있는 리스트 전달
        }

        public void DeleteLogData(string logNumber)
        {
            string queryStatement = string.Format("DELETE FROM log_data WHERE logNumber = '{0}';", logNumber);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void ResetLogData()
        {
            string queryStatement = string.Format("DELETE FROM log_data;");
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

    }
}
