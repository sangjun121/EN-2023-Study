using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;

public class DeletingUserInformation //inf와같이 줄임말 
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    UserModeUi userModeUi;
    DataLogging dataLogging;

    UserDAO userDAO;


    public DeletingUserInformation()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.userModeUi = UserModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.userDAO = new UserDAO();
    }

    public bool DeleteUserInformation(UserDTO loggedInUserInformation)
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        while (isMenuExecute)
        {
            userModeUi.confirmAccountDeletion();
            ConsoleKeyInfo inputKey;


            bool isCheckedEnter = false;
            int selectedMenuNumber = -1;

            Console.SetCursorPosition(43, 3);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("○ 예");
            Console.SetCursorPosition(60, 3);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("○ 아니오");
            selectedMenuNumber = (int)(UserManagementNumber.DELETEING_USER);

            while (isCheckedEnter == false)
            {
                inputKey = Console.ReadKey();
                if (inputKey.Key == ConsoleKey.LeftArrow)
                {
                    Console.SetCursorPosition(43, 4);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("○ 예");
                    Console.SetCursorPosition(60, 4);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("○ 아니오");
                    selectedMenuNumber = (int)(UserManagementNumber.DELETEING_USER);
                }
                else if (inputKey.Key == ConsoleKey.RightArrow)
                {
                    Console.SetCursorPosition(43, 4);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("○ 예");
                    Console.SetCursorPosition(60, 4);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("○ 아니오");
                    Console.ResetColor();
                    selectedMenuNumber = (int)(UserManagementNumber.SAVING_USER); 
                }
                else if (inputKey.Key == ConsoleKey.Enter)
                {
                    isCheckedEnter = true;
                }
            }

            if (selectedMenuNumber == (int)(UserManagementNumber.DELETEING_USER))
            {
                userDAO.DeleteUserInformation((loggedInUserInformation.UserNumber).ToString()); // 유저정보 삭제
                Console.Clear();
                userModeUi.PrintAccountDeletionSentence();
                Console.ReadKey(true);

                //로그 수집
                dataLogging.SetLog(loggedInUserInformation.UserName, loggedInUserInformation.Id, Constants.DELETE_USER_INFORMATION);

                return Constants.IS_CANCELLATION_OF_MEMBERSHIP; // 유저정보 삭제 여부 불리언 변수로 반환
            }
            else if (selectedMenuNumber == (int)(UserManagementNumber.SAVING_USER))
            {
                Console.Clear();
                userModeUi.PrintMaintainingAccountSentence();
            }   
        }

        return Constants.IS_NOT_CANCELLATION_OF_MEMBERSHIP; // 유저 삭제가 안되었을 경우 
    }
}
