using ConsoleApp1.Model;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;
using ConsoleApp1.Utility;

public class BookFinder
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    CommonFunctionUi commonFunctionUi;
    DataLogging dataLogging;

    BookDAO bookDAO;

    public BookFinder()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.commonFunctionUi = CommonFunctionUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookDAO = new BookDAO();
    }

    List<BookDTO> allBookInformation;
    List<BookDTO> selectedBookInformation;
    UserDTO loggedInUserInformation;
    bool isJudgingCorrectString; //입력값 검사 후 탈출을 위한 변수
    bool isPrintPossiblity;
    string title;
    string author;
    string publisher;

    public void FindBook(UserDTO loggedInUserInformation)
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        this.loggedInUserInformation = loggedInUserInformation;

        while (isMenuExecute)
        {
            PrintBookFinderMenu(); // 메뉴 검색 창 및 책 리스트 출력

            InputBookSearchData(); //책 검색 입력하기

            Console.Clear();
            commonFunctionUi.PrintBookFinderMenu(); // 메뉴 검색 창 출력

            CompareAndPrintBookList(); // 검색 정보와 책 정보 비교 후 출력

            commonFunctionUi.SelectEndorReturnInTheProgram(); //다시하기 또는 나가기 출력

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }

    public void PrintBookFinderMenu()
    {
        //책 검색창 출력
        commonFunctionUi.PrintBookFinderMenu();

        //책 리스트 출력
        allBookInformation = bookDAO.ReadAllBookData();
        for (int i = 0; i < allBookInformation.Count; i++)
        {
            commonFunctionUi.PrintBookList(allBookInformation[i]);
        }
    }
    public void InputBookSearchData()
    {
        //입력값 받기 (책 제목, 저자, 출판사)
        isJudgingCorrectString = false;
        Console.SetCursorPosition(17, 1);
        while (!isJudgingCorrectString)//제목
        {
            title = InputByReadKey.ReceiveInput(17, 1, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(17, 1, title, Constants.BOOK_NAME_REGULAR_EXPRESSION, Constants.BOOK_NAME_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(19, 2);
        while (!isJudgingCorrectString) // 작가
        {
            author = InputByReadKey.ReceiveInput(19, 2, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(19, 2, author, Constants.BOOK_AUTHOR_REGULAR_EXPRESSION, Constants.BOOK_AUTHOR_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(17, 3);
        while (!isJudgingCorrectString) // 출판사
        {
            publisher = InputByReadKey.ReceiveInput(17, 3, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(17, 3, publisher, Constants.BOOK_PUBLISHER_REGULAR_EXPRESSION, Constants.BOOK_PUBLISHER_ERROR_MESSAGE);
        }

        dataLogging.SetLog(loggedInUserInformation.UserName, title, Constants.FIND_BOOK);
    }

    public List<BookDTO> CompareAndPrintBookList()
    {
        //입력받은 책과 데이터의 책들 비교
        isPrintPossiblity = false;
        selectedBookInformation = new List<BookDTO>();

        for (int i = 0; i < allBookInformation.Count; i++)
        {
            if ((allBookInformation[i].BookName).Contains(title) && (allBookInformation[i].BookAuthor).Contains(author) && (allBookInformation[i].BookPublisher).Contains(publisher))
            {
                selectedBookInformation.Add(allBookInformation[i]); //검색된 책만 저장하는 리스트
                isPrintPossiblity = true;
            }

            if (isPrintPossiblity) // 일치하면 출력
            {
                commonFunctionUi.PrintBookList(allBookInformation[i]);
                isPrintPossiblity = false;
            }
        }
        return selectedBookInformation;
    }

}
