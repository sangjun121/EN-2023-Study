using System;
using System.Collections.Generic;
using ConsoleApp1.Model;
using ConsoleApp1.Utility;

public class BookBorrowList
{
    UserModeUi userModeUi;
    DataLogging dataLogging;
    BookDAO bookDAO;
    
    public BookBorrowList()
    {
        this.userModeUi = UserModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();
        this.bookDAO = new BookDAO();
       
    }

    public void ShowBookBorrowList(UserDTO loggedInUserInformation)
	{
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        List<BookDTO> borrowedBookInformation;

        while (isMenuExecute)
		{
			userModeUi.PrintBorrowingList();
            borrowedBookInformation = bookDAO.ReadBorrowedBookList(loggedInUserInformation);

            dataLogging.SetLog(loggedInUserInformation.UserName, Constants.ALL_BOOK_VIEW, Constants.BOOK_BORROW_LIST);

            for (int i = 0; i < borrowedBookInformation.Count; i++)
			{
				userModeUi.PrintUserBorrowingList(borrowedBookInformation[i]);
			}

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }

    }
}
