using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using ConsoleApp1.Model;
using ConsoleApp1.Utility;

public class BorrowingBook
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    UserModeUi userModeUi;
    CommonFunctionUi commonFunctionUi;
    DataLogging dataLogging;

    BookDAO bookDAO;

    public BorrowingBook() 
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.userModeUi = UserModeUi.GetInstance();
        this.commonFunctionUi = CommonFunctionUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookDAO = new BookDAO();
    }

    public void BorrowBook(UserDTO loggedInUserInformation) //함수 분리
	{
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        bool isIdIncludedInList;
        bool isBookQuantityExist;
        string title = "";
        string author = "";
        string publisher = "";
        bool isPrintPossiblity;

        while (isMenuExecute)
        {
            string bookId = "";
            bool isJudgingCorrectString = false;
            List<BookDTO> allBookInformation;
            List<BookDTO> selectedBookInformation = new List<BookDTO>();

            Console.Clear();
            // 책 검색 메뉴창 출력
            commonFunctionUi.PrintBookFinderMenu();

            //모든 책 리스트 출력
            allBookInformation = bookDAO.ReadAllBookData(); //while

            for (int i = 0; i < allBookInformation.Count; i++)
            {
                commonFunctionUi.PrintBookList(allBookInformation[i]);
            }

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

            Console.Clear();
            userModeUi.PrintBorrowingBookMenu();

            //입력받은 책과 데이터의 책들 비교 후 출력
            isPrintPossiblity = false;
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

            isIdIncludedInList = false;
            isBookQuantityExist = false;

            while (!isIdIncludedInList || !isBookQuantityExist) // id입력 받고 해당 아이디인 책이 검색 후 출력된 리스트에 있는지 확인 || 책 수량이 있는지 확인
            {
                Console.SetCursorPosition(36, 3);
                Console.Write("                                         ");
                Console.SetCursorPosition(36, 3);

                isJudgingCorrectString = false;
                while (!isJudgingCorrectString) //책 아이디 입력
                {
                    bookId = InputByReadKey.ReceiveInput(36, 3, 3, Constants.IS_NOT_PASSWORD);
                    isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(36, 3, bookId, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE);
                }

                //입력받은 책 id와 검색된 책 리스트 비교
                for (int i = 0; i < selectedBookInformation.Count; i++)
                {
                    if (selectedBookInformation[i].BookId == int.Parse(bookId))
                    {
                        isIdIncludedInList = true; //일치하는 책이 있을 경우
                        if (selectedBookInformation[i].BookQuantity > 0) //책 수량 검사로 대여 가능한지 판별
                        {
                            Console.SetCursorPosition(0, 3);
                            Console.WriteLine("      책 빌리기 성공!                          ");
                            Console.WriteLine("                                               ");

                            //대여 시간, 반납 일시 입력
                            selectedBookInformation[i].BorrowTime = DateTime.Now;
                            selectedBookInformation[i].ReturnTime = (selectedBookInformation[i].BorrowTime).AddDays(7);

                            //책 수량 하나 줄이기
                            selectedBookInformation[i].BookQuantity -= 1;
                            bookDAO.IncreaseAndDecreaseBookQuantity(selectedBookInformation[i]); //책 수량을 하나 줄여 책 데이터 베이스(book_data 테이블)에 업데이트를 위해 해당 책 DTO를 DAO에 넘겨주기
                            
                            //최종적으로 빌린 책 정보들 저장하는 데이터 베이스 테이블(user_borrowed_book_list 테이블)에 저장
                            bookDAO.SaveBorrowedBookToData(selectedBookInformation[i], loggedInUserInformation); // 사용자 정보와 빌린 책정보를 넘겨, 빌린책에 대한 정보를 데이터 베이스에 저장
                            isBookQuantityExist = true;

                            //로그 수집
                            dataLogging.SetLog(loggedInUserInformation.UserName,Constants.BOOK_ID + bookId, Constants.BORROWING_BOOK);
                        }

                        else if (selectedBookInformation[i].BookQuantity == 0)
                        {
                            Console.SetCursorPosition(0, 3);
                            Console.WriteLine("전 수량이 대여 중 입니다.         ");
                            Console.WriteLine("                                  ");
                        }
                        break;
                    }
                }

                if (isIdIncludedInList == false) //일치하는 책이 없을 경우
                {
                    Console.SetCursorPosition(36, 3);
                    userModeUi.PrintNotCorrectBook();
                    Console.ReadKey(true);//창 넘기기
                }

            }

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
        

    }
}
