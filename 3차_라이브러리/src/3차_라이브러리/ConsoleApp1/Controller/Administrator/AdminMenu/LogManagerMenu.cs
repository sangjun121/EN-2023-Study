using ConsoleApp1.Controller.Administrator.AdminMenu.LogMenu;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller.Administrator.AdminMenu
{
    public class LogManagerMenu
    {
        DataLogging dataLogging;
        MainMenuUi mainMenuUi;
        CommonFunctionUi commonFunctionUi;
        MenuSelectController menuSelectController;

        LogEdition logEdition;
        LogReset logReset;
        LoggingTextFile loggingTextFile;
        DeletionLogTextFile deletionLogTextFile;

        public LogManagerMenu()
        {
            this.dataLogging = DataLogging.GetInstance();
            this.mainMenuUi = MainMenuUi.GetInstance();
            this.commonFunctionUi = CommonFunctionUi.GetInstance();
            this.menuSelectController = MenuSelectController.GetInstance();

            this.logEdition = new LogEdition();
            this.logReset = new LogReset();
            this.loggingTextFile = new LoggingTextFile();
            this.deletionLogTextFile = new DeletionLogTextFile();
        }

        public void LogManagerSelectMenu()
        {
            int menuNumber;
            string[] logMenuList = { "○ 로그 수정", "○ 로그 TEXT 파일저장", "○ 로그 삭제", "○ 로그 리셋" };
            bool isMenuExecute = true; //메뉴 탈출 진리형 변수

            while (isMenuExecute)
            {
                Console.Clear();
                mainMenuUi.ViewMainMenu();
                commonFunctionUi.ViewMenu();

                menuNumber = menuSelectController.SelectMenuWithUpAndDown(logMenuList, 4, 49, 26);
                Console.Clear();

                switch (menuNumber)
                {
                    case (int)(LogManagerMenuNumber.LOG_EDIT):
                        dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.EDIT_LOG, Constants.SELECT_MENU);
                        logEdition.DeleteLog();
                        break;
                    case (int)(LogManagerMenuNumber.LOG_SAVE_TEXT_FILE):
                        dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.SAVE_LOG, Constants.SELECT_MENU);
                        loggingTextFile.SaveLogDateToTEXT();
                        break;
                    case (int)(LogManagerMenuNumber.LOG_DELETE_TEXT_FILE):
                        dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.DELETE_LOG, Constants.SELECT_MENU);
                        deletionLogTextFile.DeleteTextFile();
                        break;
                    case (int)(LogManagerMenuNumber.LOG_RESET):
                        dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.RESET_LOG, Constants.SELECT_MENU);
                        logReset.ResetLog();
                        break;
                }

                //뒤로가기
                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
            }

        }
    }
}
