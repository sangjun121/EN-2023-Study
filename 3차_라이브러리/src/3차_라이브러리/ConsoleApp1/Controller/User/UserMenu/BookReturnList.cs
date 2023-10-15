using System;
using System.Collections.Generic;
using ConsoleApp1.Model;
using ConsoleApp1.Utility;

public class BookReturnList
{
    UserModeUi userModeUi;
    DataLogging dataLogging;

    BookDAO bookDAO;

    public BookReturnList()
    {
        this.userModeUi = UserModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookDAO = new BookDAO();
    }

    public void ShowBookReturnList(UserDTO loggedInUserInformation) // 함수 명 변경
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        List<BookDTO> returnedBookInformation;

        while (isMenuExecute)
        {
            userModeUi.PrintReturningMenuList();
            returnedBookInformation = bookDAO.ReadReturnedBookList(loggedInUserInformation);

            //로그 수집
            dataLogging.SetLog(loggedInUserInformation.UserName, Constants.ALL_BOOK_VIEW, Constants.BOOK_RETURN_LIST);

            for (int i = 0; i < returnedBookInformation.Count; i++)
            {
                userModeUi.PrintUserReturningList(returnedBookInformation[i]);
            }
            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }
}
