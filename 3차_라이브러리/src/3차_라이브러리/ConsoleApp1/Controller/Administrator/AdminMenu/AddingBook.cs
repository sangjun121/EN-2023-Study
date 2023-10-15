using System;
using ConsoleApp1.Model;
using ConsoleApp1.Utility;

public class AddingBook
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    AdministratorModeUi administratorModeUi;
    DataLogging dataLogging;

    BookDAO bookDAO;

    public AddingBook() {
        
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.administratorModeUi = AdministratorModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookDAO = new BookDAO();
    }


    bool isJudgingCorrectString = false;
    bool isNumber = false;
    public void AddNewBook() //새로운 책 추가하기
	{
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        BookDTO newBookDTO = new BookDTO();

        while (isMenuExecute)
        {
            administratorModeUi.PrintAddingBookMenu();  // 책 추가 메뉴 인터페이스 출력

            newBookDTO = InputNewBookInformation(); //새로 입력받은 책을 DTO에 저장
            bookDAO.AddNewBookToData(newBookDTO); //데이터 베이스에 저장

            Console.Clear();
            administratorModeUi.PrintAddingBookSuccessSentence(); //책 추가 완료 인터페이스 출력

            //로그 수집
            dataLogging.SetLog(Constants.ADMINSTRATOR, newBookDTO.BookName, Constants.ADDING_BOOK);

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }

    public BookDTO InputNewBookInformation() //정보 입력 받는 함수
    {
        BookDTO newBookDTO = new BookDTO();

        isNumber = false;
        Console.SetCursorPosition(29, 11);

        do// 책이름 입력
        {
            newBookDTO.BookName = InputByReadKey.ReceiveInput(29, 11, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 11, newBookDTO.BookName, Constants.BOOK_NAME_REGULAR_EXPRESSION, Constants.BOOK_NAME_ERROR_MESSAGE);
        } while (!isJudgingCorrectString);

        Console.SetCursorPosition(29, 12);
        do// 저자 입력
        {
            newBookDTO.BookAuthor = InputByReadKey.ReceiveInput(29, 12, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 12, newBookDTO.BookAuthor, Constants.BOOK_AUTHOR_REGULAR_EXPRESSION, Constants.BOOK_AUTHOR_ERROR_MESSAGE);
        } while (!isJudgingCorrectString);

        Console.SetCursorPosition(29, 13);
        do// 출판사 입력
        {
            newBookDTO.BookPublisher = InputByReadKey.ReceiveInput(29, 13, 15, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 13, newBookDTO.BookPublisher, Constants.BOOK_PUBLISHER_REGULAR_EXPRESSION, Constants.BOOK_PUBLISHER_ERROR_MESSAGE);
        } while (!isJudgingCorrectString);

        Console.SetCursorPosition(29, 14);

        while (!isJudgingCorrectString)// 수량 입력
        {
            if (regularExpression.JudgeWithRegularExpression(29, 14, InputByReadKey.ReceiveInput(29, 14, 3, Constants.IS_NOT_PASSWORD), Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE)) //문자열이 숫자인지 검사
            {
                newBookDTO.BookQuantity = int.Parse(InputByReadKey.ReceiveInput(29, 14, 3, Constants.IS_NOT_PASSWORD));
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 14, (newBookDTO.BookQuantity).ToString(), Constants.BOOK_QUANTITY_REGULAR_EXPRESSION, Constants.BOOK_QUANTITY_ERROR_MESSAGE);
            }
        }

        Console.SetCursorPosition(29, 15);
        do// 책가격 입력
        {
            if (regularExpression.JudgeWithRegularExpression(29,15,InputByReadKey.ReceiveInput(29, 15, 6, Constants.IS_NOT_PASSWORD),Constants.NUMBER_REGULAR_EXPRESSION,Constants.NUMBER_ERROR_MESSAGE)) //문자열이 숫자인지 검사
            {
                newBookDTO.BookPrice = int.Parse(InputByReadKey.ReceiveInput(29, 15, 6, Constants.IS_NOT_PASSWORD));
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 15, (newBookDTO.BookPrice).ToString(), Constants.BOOK_PRICE_REGULAR_EXPRESSION, Constants.BOOK_PRICE_ERROR_MESSAGE);
            }

        } while (!isJudgingCorrectString);

        Console.SetCursorPosition(29, 16);
        do// 책출시일 입력
        {
            newBookDTO.BookPublicationDate = InputByReadKey.ReceiveInput(29, 16, 10, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 16, newBookDTO.BookPublicationDate, Constants.BOOK_PUBLISH_DATE_REGULAR_EXPRESSION, Constants.BOOK_PUBLISH_DATE_ERROR_MESSAGE);
        } while (!isJudgingCorrectString);

        Console.SetCursorPosition(29, 17);
        do// 책 isbn 입력
        {
            newBookDTO.Isbn = InputByReadKey.ReceiveInput(29, 17, 17, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 17, newBookDTO.Isbn, Constants.BOOK_ISBN_REGULAR_EXPRESSION, Constants.BOOK_ISBN_ERROR_MESSAGE);
        } while (!isJudgingCorrectString);

        Console.SetCursorPosition(29, 18);
        do// 책 설명 입력
        {
            newBookDTO.BookDescription = InputByReadKey.ReceiveInput(29, 18, 200, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(29, 18, newBookDTO.BookDescription, Constants.BOOK_INFORMATION_REGULAR_EXPRESSION, Constants.BOOK_INFORMATION_ERROR_MESSAGE);
        } while (!isJudgingCorrectString);

        return newBookDTO;
    }

    
}
