using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Security.Policy;

public class EditingBook
{
    string title;
    string author;
    string publisher;
    string quantity;
    string price;
    string publishDate;

    int PrintPossiblity = 0;
    string editedBookId;
    bool isJudgingCorrectString;

    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    AdministratorModeUi administratorModeUi;
    CommonFunctionUi commonFunctionUi;
    DataLogging dataLogging;

    BookFinder bookFinder;
    BookDAO bookDAO;

    public EditingBook(BookFinder bookFinder)
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.administratorModeUi = AdministratorModeUi.GetInstance();
        this.commonFunctionUi = CommonFunctionUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookFinder = bookFinder;
        this.bookDAO = new BookDAO();
    }

    public void EditBook()
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        bool isBookExistInList;
        BookDTO currentBookInformation = null; //현재 책정보 담는 DTO
        List<BookDTO> selectedBookInformation;

        while (isMenuExecute)
        {
            //책 검색하기
            bookFinder.PrintBookFinderMenu(); // 메뉴 검색 창 및 책 리스트 출력
            bookFinder.InputBookSearchData(); //책 검색 입력하기
            Console.Clear();

            administratorModeUi.PrintEditingBookAskingMenu(); // 수정할 책 입력받는 메뉴 출력
            selectedBookInformation = bookFinder.CompareAndPrintBookList(); // 검색 정보와 책 정보 비교 후 출력, 출력된 책 리스트 저장

            //수정할 책 아이디 입력 하기
            Console.SetCursorPosition(64, 2);
            isJudgingCorrectString = false;
            while (!isJudgingCorrectString)
            {
                editedBookId = InputByReadKey.ReceiveInput(64, 2, 3, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(64, 2, editedBookId, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE);
            }

            //수정 가능한 도서인지 판단(1. 검색된 리스트에 있어야 함  2. 대여도서 목록에 없어야 함) 
            //1. 입력한 아이디가 검색된 책 리스트에 있는지 확인 
            isBookExistInList = false;
            for (int i = 0; i < selectedBookInformation.Count; i++)
            {
                if (int.Parse(editedBookId) == selectedBookInformation[i].BookId) //있다면 수정 가능 조건 1개 충족
                {
                    currentBookInformation = selectedBookInformation[i]; //나중에 수정할 경우 현재 책 정보를 출력하기 위해 저장
                    isBookExistInList = true;
                    break;
                }
            }

            if (isBookExistInList)
            {
                int bookCountInBorrowedList = bookDAO.FindBookInBorrowedList(editedBookId); // 2. 해당 책이 이미 대여중일 경우
                if (bookCountInBorrowedList != 0) //대여 중일 경우 수정 불가
                {
                    Console.Clear();
                    administratorModeUi.PrintEditingBookFailAlreadyBorrowedSentence();
                }
                else // 대여 목록에 없을 경우 수정가능!
                {
                    Console.Clear();
                    administratorModeUi.PrintEditingBookMenu();
                    administratorModeUi.PrintCurrentSavedBookInformation(currentBookInformation); //기존 책정보 출력
                    administratorModeUi.PrintEditingBookInformation(); //책 정보 입력 창 출력
                    InputEditBookData(); // 책 정보 입력하기

                    //책 수정하기 1. 새로 입력받은 정보 DTO에 담기
                    BookDTO editedBookDTO = new BookDTO();
                    editedBookDTO.BookName = title;
                    editedBookDTO.BookAuthor =author;
                    editedBookDTO.BookPublisher = publisher;
                    editedBookDTO.BookQuantity = int.Parse(quantity);
                    editedBookDTO.BookPrice = int.Parse(price);
                    editedBookDTO.BookPublicationDate = publishDate;

                    //책 수정하기 2. 책 정보 업데이트 하는 함수에 대입
                    bookDAO.EditBook(editedBookDTO, editedBookId);//책 수정

                    Console.Clear();
                    administratorModeUi.PrintEditingBookSuccessSentence();  //수정 성공 메시지 출력
                                                                            
                    //로그 수집
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.BOOK_ID + editedBookId, Constants.EDITING_BOOK);
                }
            }
            else // 검색된 리스트에 존재 하지않을때
            {
                Console.Clear();
                administratorModeUi.PrintFailNotExistInListSentence();
            }

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();

        }
    }
   
    public void InputEditBookData()
    {
        isJudgingCorrectString = false;
        Console.SetCursorPosition(63, 24);
        while (!isJudgingCorrectString) //책 이름
        {
            title = InputByReadKey.ReceiveInput(63, 24, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(63, 24, title, Constants.BOOK_NAME_REGULAR_EXPRESSION, Constants.BOOK_NAME_ERROR_MESSAGE);
        } 

        isJudgingCorrectString = false;
        Console.SetCursorPosition(63, 25);
        while (!isJudgingCorrectString) //책 작가
        {
            author = InputByReadKey.ReceiveInput(63, 25, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(63, 25, author, Constants.BOOK_AUTHOR_REGULAR_EXPRESSION, Constants.BOOK_AUTHOR_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(63, 26);
        while (!isJudgingCorrectString) //출판사
        {
            publisher = InputByReadKey.ReceiveInput(63, 26, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(63, 26, publisher, Constants.BOOK_PUBLISHER_REGULAR_EXPRESSION, Constants.BOOK_PUBLISHER_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(63, 27);
        while (!isJudgingCorrectString) //수량
        {
            quantity = InputByReadKey.ReceiveInput(63, 27, 3, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(63, 27, quantity, Constants.BOOK_QUANTITY_REGULAR_EXPRESSION, Constants.BOOK_QUANTITY_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(63, 28);
        while (!isJudgingCorrectString) //가격
        {
            price = InputByReadKey.ReceiveInput(63, 28, 6, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(63, 28, price, Constants.BOOK_PRICE_REGULAR_EXPRESSION, Constants.BOOK_PRICE_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(63, 29);
        while (!isJudgingCorrectString) //출판 일시
        {
            publishDate = InputByReadKey.ReceiveInput(63, 29, 10, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(63, 29, publishDate, Constants.BOOK_PUBLISH_DATE_REGULAR_EXPRESSION, Constants.BOOK_PUBLISH_DATE_ERROR_MESSAGE);
        }
    }

}

