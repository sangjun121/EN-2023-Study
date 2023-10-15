using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Net;
using ConsoleApp1.Model;
using ConsoleApp1.Utility;

public class ReturningBook //책 반납하기
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    UserModeUi userModeUi;
    DataLogging dataLogging;

    BookDAO bookDAO;

    public ReturningBook() 
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.userModeUi = UserModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookDAO = new BookDAO();
    }

    public void ReturnBook(UserDTO loggedInUserInformation)
	{
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        bool isJudgingCorrectString;//입력값 올바른지 판단 함수
        string bookId="";

        List<BookDTO> uesrBorrowBookInformation;
        List<BookDTO> borrowedBookInformationOfInputId;

        while (isMenuExecute)
        {
            userModeUi.PrintReturningBook(); // 1.반납도서 입력할 창 출력

            // 2.대여도서 목록 불러오기
            uesrBorrowBookInformation = bookDAO.ReadBorrowedBookList(loggedInUserInformation);

            // 3.대여도서 목록 출력
            if(uesrBorrowBookInformation.Count == 0) //빌린 책이 없을 때
            {
                userModeUi.PrintNoShouldReturnBook();
                Console.ReadKey(true);
                break;
            }

            else //빌린 책이 있을 때
            {
                for (int i = 0; i < uesrBorrowBookInformation.Count; i++) //데이터에서 유저가 빌린 책 리스트 가져와 전부 출력
                {
                    userModeUi.PrintUserBorrowingList(uesrBorrowBookInformation[i]);
                }
            }

            // 4.반납할 책 아이디 입력
            isJudgingCorrectString = false;
            Console.SetCursorPosition(36, 3);
            while (!isJudgingCorrectString)  // 반납할 책 아이디 입력
            {
                bookId = InputByReadKey.ReceiveInput(36, 3, 3, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(36, 3, bookId, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE);
            }

            // 5.반납할 책이 도서 정보에 있는지 탐색
            borrowedBookInformationOfInputId = bookDAO.SearchForInputIdInBorrowedBookList(loggedInUserInformation, bookId);
            if(borrowedBookInformationOfInputId.Count == 0 ) //입력한 책 id가 빌린 리스트에 없을때
            {
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("잘못된 ID를 입력하였습니다. 다시 입력해 주세요.");
                Console.WriteLine("                            ");
            }
            else //빌린 리스트에 입력한 책 id가 있을 경우
            {
                //book_data에 해당 책 권수 한권 늘려주기
                borrowedBookInformationOfInputId[0].BookQuantity += 1;
                bookDAO.IncreaseAndDecreaseBookQuantity(borrowedBookInformationOfInputId[0]);

                //user_borrowed_book_list 에서 제거하기
                bookDAO.DeleteBookInBorrowedList(loggedInUserInformation, bookId);

                //반납시간 기입
                borrowedBookInformationOfInputId[0].ReturnTime = DateTime.Now;
                //user_returned_book_list에 추가
                bookDAO.SaveReturnedBookToData(borrowedBookInformationOfInputId[0], loggedInUserInformation);

                //로그 수집
                dataLogging.SetLog(loggedInUserInformation.UserName, Constants.BOOK_ID + bookId, Constants.RETURNING_BOOK);

                Console.SetCursorPosition(0, 3);
                Console.WriteLine("      책 반납 성공!                          ");
                Console.WriteLine("                                               ");

            }

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }
}
