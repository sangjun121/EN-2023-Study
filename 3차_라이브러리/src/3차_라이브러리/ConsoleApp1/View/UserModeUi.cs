using System;

public class UserModeUi
{
    //싱글턴 디자인 패턴
    private static UserModeUi instance;
    private UserModeUi() { }
    public static UserModeUi GetInstance()
    {
        if (instance == null)
        {
            instance = new UserModeUi();
        }
        return instance;
    }


    public void PrintBorrowingBookMenu()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("     빌릴 책의 ID를 입력해 주세요 : ");
        Console.WriteLine("     값의 범위 : 0~999");
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ESC  :  뒤로가기");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ENTER  :  입력하기\n\n\n\n");
        Console.ResetColor();
        Console.WriteLine("============================================================");


    }
    public void PrintNotCorrectBook()
    {
        Console.ForegroundColor= ConsoleColor.Red;
        Console.WriteLine("해당 책이 없습니다. 다시 입력해 주세요");
        Console.ResetColor();
    }

    public void PrintBorrowingList()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("                  현재 대여 중인 도서 목록                  ");
        Console.WriteLine("============================================================");
    }

    public void PrintUserBorrowingList(BookDTO book)
    {
        Console.WriteLine(" 책 아이디 : " + book.BookId);
        Console.WriteLine(" 책 이름   : " + book.BookName);
        Console.WriteLine(" 작가      : " + book.BookAuthor);
        Console.WriteLine(" 출판사    : " + book.BookPublisher);
        Console.WriteLine(" 책 수량   : " + book.BookQuantity);
        Console.WriteLine(" 책 가격   : " + book.BookPrice);
        Console.WriteLine(" 대여 일시 : " + book.BorrowTime);
        Console.WriteLine(" 반납 의무 일시 : " + book.ReturnTime);
        Console.WriteLine(" ISBN      : " + book.Isbn);
        Console.WriteLine("============================================================");
    }
    public void PrintUserReturningList(BookDTO book)
    {
        Console.WriteLine(" 책 아이디 : " + book.BookId);
        Console.WriteLine(" 책 이름   : " + book.BookName);
        Console.WriteLine(" 작가      : " + book.BookAuthor);
        Console.WriteLine(" 출판사    : " + book.BookPublisher);
        Console.WriteLine(" 책 수량   : " + book.BookQuantity);
        Console.WriteLine(" 책 가격   : " + book.BookPrice);
        Console.WriteLine(" 대여 일시 : " + book.BorrowTime);
        Console.WriteLine(" 반납 일시 : " + book.ReturnTime);
        Console.WriteLine(" ISBN      : " + book.Isbn);
        Console.WriteLine("============================================================");
    }

    public void PrintNoShouldReturnBook()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("                    반납할 책이 없습니다!                   ");
        Console.WriteLine("============================================================");
    }

    public void PrintReturningBook()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("   반납할 책의 ID를 입력해 주세요 : ");
        Console.WriteLine("   값의 범위 : 0~999");
        Console.WriteLine("\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ESC  :  뒤로가기");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ENTER  :  입력하기\n\n");
        Console.ResetColor();
        Console.WriteLine("============================================================");
    }
    public void PrintReturningMenuList()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine("                    현재 반납한 도서 목록                   ");
        Console.WriteLine("============================================================");
    }

    public void PrintBeforeUserInformation(UserDTO user)
    {
        Console.WriteLine("                        _______________________________________________________________                        ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                        개인 정보 바꾸기                       |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |             ESC : 뒤로가기          ENTER : 선택하기          |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");
        Console.WriteLine("                                                                                                               ");
        Console.WriteLine("                                          ★ 현재 등록되어 있는 정보 ★                                        ");
        Console.WriteLine("                                                                                                               ");
        Console.WriteLine("                                                                                                               ");
        Console.WriteLine("                 USER ID (8~15글자 영어, 숫자포함) : " + user.Id);
        Console.WriteLine("                 USER PW (8~15글자 영어, 숫자포함) : " + user.Password);
        Console.WriteLine("                 USER Name (한글,영어 포함 2글자 이상) : " + user.UserName);
        Console.WriteLine("                 USER Age (    자연수 0~200세    ) : " + user.UserAge);
        Console.WriteLine("                 USER PhoneNumber (  01x-xxxx-xxxx  ) : " + user.UserPhoneNumber);
        Console.WriteLine("                 USER Address (       한글 주소       ) : " + user.UserAddress);
    }
    public void PrintAfterUserInformation(UserDTO user)
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("                                          ★     변경 할 정보 입력    ★                                        ");
        Console.WriteLine("                                                                                                               ");
        Console.WriteLine("                                                                                                               ");
        Console.WriteLine("                 USER ID (8~15글자 영어, 숫자포함) : ");
        Console.WriteLine("                 USER PW (8~15글자 영어, 숫자포함) : ");
        Console.WriteLine("                 USER Name (한글,영어 포함 2글자 이상) : ");
        Console.WriteLine("                 USER Age (    자연수 0~200세    ) : ");
        Console.WriteLine("                 USER PhoneNumber (  01x-xxxx-xxxx  ) : ");
        Console.WriteLine("                 USER Address (       한글 주소       ) : ");
    }

    public void confirmAccountDeletion()
    {
        Console.WriteLine("                        _______________________________________________________________                        ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                     정말 삭제하시겠습니까?                    |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");

    }

    public void PrintAccountDeletionSentence()
    {
        Console.WriteLine("                        _______________________________________________________________                        ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                   회원 탈퇴가 완료 되었습니다.                |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");

    }

    public void PrintMaintainingAccountSentence()
    {
        Console.WriteLine("                        _______________________________________________________________                        ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                   회원 탈퇴가 거절 되었습니다.                |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");

    }

    public void PrintUserInformationUpdateSuccess()
    {
        Console.WriteLine("                        _______________________________________________________________                        ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                 회원정보 수정이 완료 되었습니다.              |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");
    }

    public void PrintSearchBookInNaverMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                               네이버 도서 검색\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                     ESC : 뒤로가기\n\n");
        Console.WriteLine("                        -----------------------------------------------------------------");
        Console.WriteLine("                                       책 이름:                                          ");
        Console.WriteLine("                                  검색 책 수량:                                          ");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }

    public void PrintBookListSearchedByNaver(BookDTO book)
    {
        Console.WriteLine("========================================================================================================================");
        Console.WriteLine("책 번호   :  " + book.BookId);
        Console.WriteLine("책 제목   :  " + book.BookName);
        Console.WriteLine("작가      :  " + book.BookAuthor);
        Console.WriteLine("출판사    :  " + book.BookPublisher);
        Console.WriteLine("가격      :  {0}", book.BookPrice);
        Console.WriteLine("출시일    :  " + book.BookPublicationDate);
        Console.WriteLine("ISBN      :  " + book.Isbn);
        Console.WriteLine("책 정보   :  " + book.BookDescription);
        Console.WriteLine("========================================================================================================================");
    }

    public void ResetMenuScreen()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < 14; i++)
        {
            Console.WriteLine("                                                                                           ");
        }
    }

    public void PrintApplyBookInNaverMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                   도서 신청\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                신청 도서번호 :                                          \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }

    public void PrintBookIsNotExistMessage()
    {
        Console.WriteLine("========================================================================================================================");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("검색된 도서가 없습니다. 다시 검색해 주세요");
        Console.ResetColor();
        Console.WriteLine("========================================================================================================================");
    }
    
    public void PrintBlueColorSentence(string sentence,int cursorPositionX, int cursorPositionY)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        Console.WriteLine(sentence);
        Console.ResetColor();
    }

    public void PrintGreenColorSentence(string sentence, int cursorPositionX, int cursorPositionY)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        Console.WriteLine(sentence);
        Console.ResetColor();
    }

    public void PrintRedColorSentence(string sentence, int cursorPositionX, int cursorPositionY)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        Console.WriteLine(sentence);
        Console.ResetColor();
    }
}