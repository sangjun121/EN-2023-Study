using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller.Administrator.AdminMenu
{
    public class AdditionBookByApplyList
    {
        AdministratorModeUi administratorModeUi;
        InputProcess inputProcess;
        BookDAO bookDAO;
        DataLogging dataLogging;

        public AdditionBookByApplyList()
        {
            this.administratorModeUi = AdministratorModeUi.GetInstance();
            this.inputProcess = InputProcess.GetInstance();
            this.bookDAO = new BookDAO();
            this.dataLogging = DataLogging.GetInstance();
        }

        public void AddBookByApplyListMain()
        {
            List<BookDTO> appliedBookList;

            bool isMenuExecute = true; //메뉴 탈출 진리형 변수
            string bookId;
            
            while (isMenuExecute)
            {
                Console.SetCursorPosition(0, 0);
                administratorModeUi.PrintApplyBookInNaverMenu();

                //요청 도서 리스트 불러오기
                appliedBookList = bookDAO.ReadApplyBookList();
                
                if(appliedBookList.Count == 0) //신청 도서가 없을때
                {
                    administratorModeUi.PrintBookIsNotExistMessage();
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    return;
                }

                //신청 도서가 있을때
                foreach (BookDTO book in appliedBookList)
                {
                    administratorModeUi.PrintBookListAppliedBookList(book);
                }

                //아이디 검색
                bookId = inputProcess.InputProcessFunction(48, 11, 3, Constants.IS_NOT_PASSWORD, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE); // 책 번호 검색

                //예외처리 1. 리스트에 있는 번호인지 검사
                BookDTO bookDTO = new BookDTO();
                bookDTO = bookDAO.FindBookInApplyBookListWithId(bookId); //책 아이디랑 일치하는 요청된 리스트의 책 가져오기

                if (bookDTO.BookName == null) // 요청리스트에 책이 없는 경우
                {
                    administratorModeUi.PrintRedColorSentence(Constants.NO_BOOK_IN_LIST, 48, 11);
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    Console.Clear();

                }

                else//책이 있는 경우 - 도서 추가하기
                {
                    bookDAO.AddNewBookInLibrary(bookDTO); // 도서관 도서목록에 추가
                    bookDAO.DeleteBookInAppliedList(bookId); // 도서 요청 리스트에서 삭제   
                    administratorModeUi.PrintGreenColorSentence(Constants.BOOK_REGISTRATE_SUCCESS, 48, 11); //성공 메시지

                    //로그 수집
                    dataLogging.SetLog(Constants.ADMINSTRATOR, bookDTO.BookName, Constants.REQUESTED_BOOK);

                    //뒤로가기
                    administratorModeUi.PrintBlueColorSentence(Constants.GOBACK_OR_AGAIN, 35, 12);
                    isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
                    Console.Clear();
                }

            }
        }

    }
}
