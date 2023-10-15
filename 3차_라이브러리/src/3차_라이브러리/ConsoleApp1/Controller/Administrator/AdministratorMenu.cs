using ConsoleApp1.Controller.Administrator.AdminMenu;
using ConsoleApp1.Utility;
using System;

public class AdministratorMenu
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    MainMenuUi mainMenuUi;
    AdministratorModeUi administratorModeUi;
    CommonFunctionUi commonFunctionUi;
    MenuSelectController menuSelectController;
    DataLogging dataLogging;

    BookFinder bookFinder;
    AddingBook addingBook;
    DeletingBook deletingBook;
    EditingBook editingBook;
    MemberManger memberManger;
    BookBorrowedStatus bookBorrowedStatus;
    AdditionBookByApplyList additionBookByApplyList;
    LogManagerMenu logManagerMenu;
    SearchingBookByNaver searchingBookByNaver;

    public AdministratorMenu( )
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.mainMenuUi = MainMenuUi.GetInstance();
        this.administratorModeUi = AdministratorModeUi.GetInstance();
        this.commonFunctionUi = CommonFunctionUi.GetInstance();
        this.menuSelectController = MenuSelectController.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.bookFinder = new BookFinder();
        this.addingBook = new AddingBook();
        this.deletingBook = new DeletingBook(bookFinder);
        this.editingBook = new EditingBook(bookFinder);
        this.memberManger = new MemberManger();
        this.bookBorrowedStatus = new BookBorrowedStatus();
        this.additionBookByApplyList = new AdditionBookByApplyList();
        this.logManagerMenu = new LogManagerMenu();
        this.searchingBookByNaver = new SearchingBookByNaver();
    }

    
    public void ControllAdministratorMenu(UserDTO administratorInformation)
    {
        int menuNumber;
        string[] administratorMenuList = { "○ 책 정보 검색", "○ 책 추가하기", "○ 책 삭제하기", "○ 책 정보 수정하기", "○ 회원관리", "○ 도서 대여 현황", "○ NAVER검색", "○ 로그관리", "○ 요청도서" };
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수

        //로그 수집
        dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.ADMINSTRATOR_AUTHORITY, Constants.LOGIN);

        while (isMenuExecute)
        {
            mainMenuUi.ViewMainMenu();
            commonFunctionUi.ViewMenu();

            menuNumber = menuSelectController.SelectMenuWithUpAndDown(administratorMenuList, 9, 49, 26);
            Console.Clear();

            switch (menuNumber)
            {
                case (int)(AdministratorMenuNumber.BOOK_FINDER):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.FIND_BOOK, Constants.SELECT_MENU);
                    bookFinder.FindBook(administratorInformation);
                    break;

                case (int)(AdministratorMenuNumber.ADDING_BOOK):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.ADDING_BOOK, Constants.SELECT_MENU);
                    addingBook.AddNewBook();
                    break;

                case (int)(AdministratorMenuNumber.DELETING_BOOK):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.DELETING_BOOK, Constants.SELECT_MENU);
                    deletingBook.DeleteABook();
                    break;

                case (int)(AdministratorMenuNumber.EDITING_BOOK):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.EDITING_BOOK, Constants.SELECT_MENU);
                    editingBook.EditBook();
                    break;

                case (int)(AdministratorMenuNumber.MEMBER_MANAGER):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.MEMBER_MANAGER, Constants.SELECT_MENU);
                    memberManger.ManageMember();
                    break;

                case (int)(AdministratorMenuNumber.BOOK_BORROWING_STATUS):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.BOOK_BORROWING_STATUS, Constants.SELECT_MENU);
                    bookBorrowedStatus.CheckBookBorrowedList();
                    break;

                case (int)(AdministratorMenuNumber.SEARCHING_NAVER):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.SEARCH_NAVER_ADD_BOOK, Constants.SELECT_MENU);
                    searchingBookByNaver.FindBookInNaverMain();
                    break;

                case (int)(AdministratorMenuNumber.CONTROLL_LOG): 
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.CONTROLL_LOG, Constants.SELECT_MENU);
                    logManagerMenu.LogManagerSelectMenu();
                    break;

                case (int)(AdministratorMenuNumber.REQUESTED_BOOK):
                    dataLogging.SetLog(Constants.ADMINSTRATOR, Constants.REQUESTED_BOOK, Constants.SELECT_MENU);
                    additionBookByApplyList.AddBookByApplyListMain();
                    break;
            }
            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }

    }
}
