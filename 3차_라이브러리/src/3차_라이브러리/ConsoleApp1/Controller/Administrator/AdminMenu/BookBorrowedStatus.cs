using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;

public class BookBorrowedStatus
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    AdministratorModeUi administratorModeUi;
    DataLogging dataLogging;

    UserDAO userDAO;
    BookDAO bookDAO;

    public BookBorrowedStatus()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.administratorModeUi = AdministratorModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.userDAO = new UserDAO();
        this.bookDAO = new BookDAO();

    }

    public void CheckBookBorrowedList() //빌린 책 출력하는 함수
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        string userName="";
        List<BookDTO> borrowedBookInformationOfSpecificUser;
        List<UserDTO> userNumber;
        while (isMenuExecute)
        {
            administratorModeUi.PrintBookBorrowedMenu(); //빌린 책 출력하는 함수 인터페이스 출력 

            userNumber = userDAO.ReadAllUserNumber(); //모든 User의 number 가져오기

            for (int i = 0; i < userNumber.Count; i++) //모든 유저 수만큼 탐색
            {
                userName = userDAO.FindUserNameByUserNumber(userNumber[i].UserNumber); // userNumber 값을 이용해서 userName 구하기
                administratorModeUi.PrintUserName(userName); //유저 이름 출력 인터페이스

                borrowedBookInformationOfSpecificUser = bookDAO.SpecificUserBorrowedBook(userNumber[i].UserNumber); //유저 번호 넘겨 주고 일치하는 빌린 리스트 출력

                for (int j = 0; j < borrowedBookInformationOfSpecificUser.Count; j++)
                {
                    administratorModeUi.PrintUserBorrowedBookList(borrowedBookInformationOfSpecificUser[j]); //빌린책 리스트 출력
                }
            }

            //로그 수집
            dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.ALL_BOOK_VIEW, Constants.BOOK_BORROW_LIST);

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }
}
    

