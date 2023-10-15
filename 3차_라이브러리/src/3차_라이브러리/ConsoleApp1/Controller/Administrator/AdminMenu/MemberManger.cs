using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Reflection;

public class MemberManger
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    AdministratorModeUi administratorModeUi;
    DataLogging dataLogging;

    UserDAO userDAO;
    BookDAO bookDAO;

    public MemberManger()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.administratorModeUi = AdministratorModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.userDAO = new UserDAO();
        this.bookDAO = new BookDAO();
    }

    public void ManageMember()
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        bool isJudgingCorrectString;
        string userNumber="";
        int userCount;
        List<UserDTO> userInformationList = new List<UserDTO>();

        while (isMenuExecute)
        {
            Console.Clear();
            administratorModeUi.PrintMemberManagerMenu();

            //삭제할 유저 아이디 입력
            Console.SetCursorPosition(70, 2);
            isJudgingCorrectString = false;
            while (!isJudgingCorrectString)
            {
                userNumber = InputByReadKey.ReceiveInput(70, 2, 3, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(70, 2, userNumber, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE);
            }

            //예외처리 1.삭제할 유저 번호가 있는지 판단
            userInformationList = userDAO.FindUserById(userNumber);
            if (userInformationList.Count != 0) //입력받은 번호와 일치하는 유저가 있을때
            {
                //예외처리 2.있다면 빌린 책 있는지 확인
                userCount = bookDAO.FindUserNumberInBorrowedBookList(userNumber);
                if (userCount == 0) //빌린책이 없을때 회원삭제 가능!
                {
                    //유저 삭제
                    userDAO.DeleteUserInformation(userNumber);
                    //유저 빌린책 리스트에서 삭제
                    bookDAO.DeleteUserInBorrowedList(userNumber);
                    //유저 반납책 리스트에서 삭제
                    bookDAO.DeleteUserInReturnedList(userNumber);

                    Console.Clear();
                    administratorModeUi.PrintDeletingUserSuccessSentence();

                    //로그 수집
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.USER_NUMBER + userNumber, Constants.MEMBER_MANAGER);
                }

                else//빌린 책이 있을 때
                {
                    Console.Clear();
                    administratorModeUi.PrintUserBorrowedSomeBook();
                }

            }
            else //입력받은 번호와 일치하는 유저가 없을때
            {
                Console.Clear();
                administratorModeUi.PrintNotExistUser();
            }

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    } 

    
}
