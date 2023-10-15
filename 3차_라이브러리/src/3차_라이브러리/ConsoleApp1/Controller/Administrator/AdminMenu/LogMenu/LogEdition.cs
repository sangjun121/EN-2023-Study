using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller.Administrator.AdminMenu.LogMenu
{
    public class LogEdition
    {
        LogDAO logDAO;
        AdministratorModeUi administratorModeUi;
        InputProcess inputProcess;
        DataLogging dataLogging;

        public LogEdition()
        {
            logDAO = new LogDAO();
            this.administratorModeUi = AdministratorModeUi.GetInstance();
            this.inputProcess = InputProcess.GetInstance();
            this.dataLogging = DataLogging.GetInstance();
        }

        public void DeleteLog()
        {
            bool isMenuExecute = true; //메뉴 탈출 진리형 변수
            List<LogDTO> allLogData = new List<LogDTO>();
            List<LogDTO> equalIdLogData = new List<LogDTO>();
            string logId;

            while (isMenuExecute)
            {
                Console.Clear();
                administratorModeUi.PrintEditLogMenu(); // 로그 삭제 메뉴 출력
                allLogData = logDAO.ReadAllLogData();

                foreach (LogDTO logDTO in allLogData) //모든 로그 출력 
                {
                    administratorModeUi.PrintLogList(logDTO);
                }

                Console.SetCursorPosition(0, 0);
                //삭제할 아이디 입력받기
                logId = inputProcess.InputProcessFunction(48, 11, 3, Constants.IS_NOT_PASSWORD, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE);

                //로그 삭제 예외처리 1. 리스트에 있는지 확인
                equalIdLogData = logDAO.FindLogByLogId(logId);
                if (equalIdLogData.Count == 0) //리스트에 없으면
                {
                    administratorModeUi.PrintRedColorSentence(Constants.NO_LOG_IN_LIST, 48, 11);
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    continue;
                }
                //삭제가능!
                logDAO.DeleteLogData(logId);//로그 삭제
                administratorModeUi.PrintGreenColorSentence(Constants.LOG_DELETE_SUCCESS, 48, 11); //삭제 성공 메시지 출력                                                
                //로그 수집
                dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.LOG_ID_SUB + logId, Constants.EDIT_LOG);


                //뒤로가기
                administratorModeUi.PrintBlueColorSentence(Constants.GOBACK_OR_AGAIN, 35, 12);
                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
            }
            
        }
    }
}
