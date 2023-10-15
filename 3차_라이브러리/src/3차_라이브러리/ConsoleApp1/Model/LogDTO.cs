using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class LogDTO
    {
        public LogDTO() { }

        private int logNumber;
        private string logTime;
        private string logUser;
        private string logInformation;
        private string logAction;

        public int LogNumber
        {
            get { return logNumber; }
            set { logNumber = value; }
        }
        public string LogTime
        {
            get { return logTime; }
            set { logTime = value; }
        }
        public string LogUser
        {
            get { return logUser; }
            set { logUser = value; }
        }
        public string LogInformation
        {
            get { return logInformation; }
            set { logInformation = value; }
        }
        public string LogAction
        {
            get { return logAction; }
            set { logAction = value; }
        }
    }
}
