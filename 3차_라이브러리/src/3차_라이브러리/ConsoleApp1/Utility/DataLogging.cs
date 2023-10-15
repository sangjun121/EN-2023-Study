using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class DataLogging
    {
        //싱글턴 디자인 패턴
        private static DataLogging instance;
        private DataLogging() { }
        public static DataLogging GetInstance()
        {
            if (instance == null)
            {
                instance = new DataLogging();
            }
            return instance;
        }

        public void SetLog(string userName, string logInformation, string logAction)
        {
            LogDAO logDAO = new LogDAO();

            DateTime dateTime = DateTime.Now;
            logDAO.AddLogInData(dateTime.ToString(), userName, logInformation, logAction);

        }

    }
}
