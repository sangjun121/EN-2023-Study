using ConsoleApp1.Controller.User.UserMenu;
using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;

public class UserMenu //유저 메뉴 진입 기능 클래스
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    MainMenuUi mainMenuUi;
    UserModeUi userModeUi;
    CommonFunctionUi commonFunctionUi;
    MenuSelectController menuSelectController;
    DataLogging dataLogging;


    BookFinder bookFinder;
    BorrowingBook borrowingBook;
    BookBorrowList bookBorrowList;
    ReturningBook returningBook;
    BookReturnList bookReturnList;
    EditingUserInformation editingUserInformation;
    DeletingUserInformation deletingUserInformation;
    FindBookInNaverAndApply findBookInNaverAndApply;

    public UserMenu()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.mainMenuUi = MainMenuUi.GetInstance();
        this.userModeUi = UserModeUi.GetInstance();
        this.commonFunctionUi = CommonFunctionUi.GetInstance();
        this.menuSelectController = MenuSelectController.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookFinder = new BookFinder();
        this.borrowingBook = new BorrowingBook( );
        this.bookBorrowList = new BookBorrowList( );
        this.returningBook = new ReturningBook();
        this.bookReturnList = new BookReturnList();
        this.editingUserInformation = new EditingUserInformation();
        this.deletingUserInformation = new DeletingUserInformation();
        this.findBookInNaverAndApply = new FindBookInNaverAndApply();
    }

    public void ControllUserMenu(UserDTO loggedInUserInformation) //유저 메뉴 선택 함수
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        int menuNumber;
        string[] userMenuList = { "○ 책 정보 검색", "○ 책 대여하기", "○ 대여 목록 확인", "○ 책 반납하기", "○ 반납 목록 확인", "○ 회원정보 수정", "○ 회원탈퇴", "○ 네이버 검색 및 도서 신청" };

        //로그인 성공
        dataLogging.SetLog(loggedInUserInformation.UserName, loggedInUserInformation.Id, Constants.LOGIN);

        while (isMenuExecute)
        {
            Console.Clear();
            mainMenuUi.ViewMainMenu();
            commonFunctionUi.ViewMenu();
            menuNumber = menuSelectController.SelectMenuWithUpAndDown(userMenuList, 8, 49, 26);
            Console.Clear();

            switch (menuNumber)
            {
                case (int)(UserMenuNumber.BOOK_FINDER):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.FIND_BOOK, Constants.SELECT_MENU);
                    bookFinder.FindBook(loggedInUserInformation);
                    break;

                case (int)(UserMenuNumber.BORROWING_BOOK):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.BORROWING_BOOK, Constants.SELECT_MENU);
                    borrowingBook.BorrowBook(loggedInUserInformation);
                    break;

                case (int)(UserMenuNumber.BOOK_BORROW_LIST):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.BOOK_BORROW_LIST, Constants.SELECT_MENU);
                    bookBorrowList.ShowBookBorrowList(loggedInUserInformation);
                    break;

                case (int)(UserMenuNumber.RETURNING_BOOK):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.RETURNING_BOOK, Constants.SELECT_MENU);
                    returningBook.ReturnBook(loggedInUserInformation);
                    break;

                case (int)(UserMenuNumber.BOOK_RETURN_LIST):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.BOOK_RETURN_LIST, Constants.SELECT_MENU);
                    bookReturnList.ShowBookReturnList(loggedInUserInformation);
                    break;

                case (int)(UserMenuNumber.EDIT_USER_INF):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.EDIT_USER_INF, Constants.SELECT_MENU);
                    editingUserInformation.EditUserInformation(loggedInUserInformation);
                    break;

                case (int)(UserMenuNumber.DELETE_USER_INFORMATION):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.DELETE_USER_INFORMATION, Constants.SELECT_MENU);
                    isMenuExecute = !deletingUserInformation.DeleteUserInformation(loggedInUserInformation); //회원탈퇴 성공시 true가 반환됨
                    break;

                case (int)(UserMenuNumber.SEARCH_APPLY_NAVER_BOOK_API):
                    dataLogging.SetLog(loggedInUserInformation.UserName, Constants.SEARCH_APPLY_NAVER_BOOK_API, Constants.SELECT_MENU);
                    findBookInNaverAndApply.FindBookInNaverAndApplyMain(loggedInUserInformation);
                    break;
            }

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }
}
