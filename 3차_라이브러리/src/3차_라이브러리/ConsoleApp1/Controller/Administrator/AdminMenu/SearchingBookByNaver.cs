using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller.Administrator.AdminMenu
{
    public class SearchingBookByNaver
    {

        public AdministratorModeUi administratorModeUi;
        public InputProcess inputProcess;
        public NaverSearchFunctionAPI naverSearchFunctionAPI;
        public BookDAO bookDAO;
        public DataLogging dataLogging;

        public List<BookDTO> searchedBookList;
       

        public SearchingBookByNaver()
        {
            this.administratorModeUi = AdministratorModeUi.GetInstance();
            this.inputProcess = InputProcess.GetInstance();
            this.naverSearchFunctionAPI = new NaverSearchFunctionAPI();
            this.bookDAO = new BookDAO();
            this.dataLogging = DataLogging.GetInstance();
        }

        public void FindBookInNaverMain()
        {
            bool isMenuExecute = true; //메뉴 탈출 진리형 변수
            Console.CursorVisible = true;

            while (isMenuExecute)
            {
                Console.SetWindowSize(115, 40);

                SearchBookInNaver();
                AddBookInLibrary();

                //뒤로가기
                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
            }
        }

        public void SearchBookInNaver()
        {
            bool isMenuExecute = true; //메뉴 탈출 진리형 변수

            Console.Clear();
            while (isMenuExecute)
            {
                Console.SetCursorPosition(0, 0);
                administratorModeUi.ResetMenuScreen();
                administratorModeUi.PrintSearchBookInNaverMenu();
                
                string bookName = inputProcess.InputProcessFunction(48, 10, 15, Constants.IS_NOT_PASSWORD, Constants.BOOK_NAME_REGULAR_EXPRESSION, Constants.BOOK_NAME_ERROR_MESSAGE); // 책이름 검색
                string bookCount = inputProcess.InputProcessFunction(48, 11, 3, Constants.IS_NOT_PASSWORD, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE); // 검색 책 수량 검색

                //도서 검색 로그 수집
                dataLogging.SetLog(Constants.ADMINSTRATOR, bookName, Constants.FIND_BOOK);

                searchedBookList = naverSearchFunctionAPI.SearchBook(bookName, bookCount);  // 네이버 api연결 (url에 보내줄 책이름, 수량 전달)

                //도서 목록 출력
                Console.SetCursorPosition(0, 16);

                //도서 목록 여부 확인
                if (searchedBookList.Count == 0)//예외처리: 도서 검색 결과가 없을때
                {
                    administratorModeUi.PrintBookIsNotExistMessage();
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    Console.Clear();
                }
                else
                {
                    foreach (BookDTO index in searchedBookList)
                    {
                        administratorModeUi.PrintBookListSearchedByNaver(index);
                    }


                    administratorModeUi.PrintGreenColorSentence(Constants.BOOK_ADD, 40, 13);
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    isMenuExecute = false; //검색 종료
                }
            }
        }

        public void AddBookInLibrary()
        {
            bool isMenuExecute = true; //메뉴 탈출 진리형 변수
            int bookCount;

            while (isMenuExecute)
            {
                List<BookDTO> administratorSelectedBook = new List<BookDTO>();

                Console.SetCursorPosition(0, 0);
                administratorModeUi.ResetMenuScreen();
                administratorModeUi.PrintAddBookInNaverMenu();

                string BookId = inputProcess.InputProcessFunction(48, 11, 3, Constants.IS_NOT_PASSWORD, Constants.NUMBER_REGULAR_EXPRESSION, Constants.NUMBER_ERROR_MESSAGE); // 책아이디 검색

                foreach (BookDTO book in searchedBookList)
                {
                    if (book.BookId == int.Parse(BookId)) // 책 아이디가 있을 경우
                    {
                        administratorSelectedBook.Add(book);
                    }
                }

                //실패시 예외처리 
                // 1. 검색 결과 리스트에 없을때
                if (administratorSelectedBook.Count == 0)
                {
                    administratorModeUi.PrintRedColorSentence(Constants.NO_BOOK_IN_LIST, 48, 11);
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    continue;
                }

                // 2. 도서관에 이미 있는 경우 - isbn으로 탐색
                bookCount = bookDAO.FindBookInBookDataList(administratorSelectedBook[0].Isbn);
                if (bookCount != 0) //이미 도서관 책일 경우
                {
                    administratorModeUi.PrintRedColorSentence(Constants.BOOK_ALREADY_IN_BOOK_DATA, 48, 11);
                    GoBackMenu.GetInstance().ensureUiVisibility();
                    continue;
                }

                //성공시
                administratorModeUi.PrintGreenColorSentence(Constants.BOOK_REGISTRATE_SUCCESS, 48, 11);
                //DAO 통해 데이터 베이스에 전달하기
                bookDAO.AddNewBookInLibrary(administratorSelectedBook[0]);

                //도서 추가로그 수집
                dataLogging.SetLog(Constants.ADMINSTRATOR, administratorSelectedBook[0].BookName, Constants.ADDING_BOOK);

                //뒤로가기
                administratorModeUi.PrintBlueColorSentence(Constants.GOBACK_OR_AGAIN, 35, 12);
                isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
            }
        }

    }
}

