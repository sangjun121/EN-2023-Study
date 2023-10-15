using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller.Administrator.AdminMenu.LogMenu
{
    public class LogReset
    {

        LogDAO logDAO;
        AdministratorModeUi administratorModeUi;
        InputProcess inputProcess;
        DataLogging dataLogging;

        public LogReset()
        {
            logDAO = new LogDAO();
            this.administratorModeUi = AdministratorModeUi.GetInstance();
            this.inputProcess = InputProcess.GetInstance();
            this.dataLogging = DataLogging.GetInstance();
        }

        public void ResetLog()
        {
            bool isMenuExecute = true; //메뉴 탈출 진리형 변수
            List<LogDTO> allLogData = new List<LogDTO>();

            while (isMenuExecute)
            {
                Console.Clear();
                administratorModeUi.PrintResetLogMenu(); // 로그 리셋 메뉴 출력
                allLogData = logDAO.ReadAllLogData();

                foreach (LogDTO logDTO in allLogData) //모든 로그 출력 
                {
                    administratorModeUi.PrintLogList(logDTO);
                }
                Console.SetCursorPosition(0, 0);

                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction(); //ESC눌러서 뒤로가기 기능 구현
                if (isMenuExecute == Constants.ESC_END_FUNCTION) continue;

                administratorModeUi.PrintRedColorSentence(Constants.BLANK, 0, 11); // 재 확인 문구 출력
                administratorModeUi.PrintRedColorSentence(Constants.REASK_RESET_INTENTION, 43, 11); // 재 확인 문구 출력

                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction(); //ESC눌러서 뒤로가기 기능 구현
                if (isMenuExecute == Constants.ESC_END_FUNCTION) continue;

                //진짜 리셋하기
                logDAO.ResetLogData();
                //로그 수집
                dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.RESET, Constants.RESET_LOG);

                //초기화 성공메시지
                administratorModeUi.PrintGreenColorSentence(Constants.BLANK, 0, 11); // 초기화 성공 문구 출력
                administratorModeUi.PrintGreenColorSentence(Constants.LOG_RESET_SUCCESS, 43, 11); // 초기화 성공 문구 출력
                
                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction(); //나가기
            }

        }
    }
}
