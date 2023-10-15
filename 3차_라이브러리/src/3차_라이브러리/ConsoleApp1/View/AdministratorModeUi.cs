using ConsoleApp1.Model;
using System;
using System.Runtime.CompilerServices;

public class AdministratorModeUi{ 

    //싱글턴 디자인 패턴
    private static AdministratorModeUi instance;
    private AdministratorModeUi() { }
    public static AdministratorModeUi GetInstance()
    {
        if (instance == null)
        {
            instance = new AdministratorModeUi();
        }
        return instance;
    }

    public void PrintAddingBookMenu()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                    도서추가\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 확인                     ESC : 뒤로가기\n\n");
        Console.WriteLine("                 ------------------------------------------------------------------------------\n");
        Console.WriteLine("                 책 제목  :  ");
        Console.WriteLine("                 작가     :  ");
        Console.WriteLine("                 출판사   :  ");
        Console.WriteLine("                 수량     :  ");
        Console.WriteLine("                 가격     :  ");
        Console.WriteLine("                 출시일   :  ");
        Console.WriteLine("                 ISBN     :  ");
        Console.WriteLine("                 정보     :  \n");
        Console.WriteLine("                 -------------------------------------------------------------------------------\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("                            책 제목 - 영어, 한글, 숫자,?!+= 1개 이상");
        Console.WriteLine("                          : 작가    - 영어, 한글 1글자 이상");
        Console.WriteLine("                          : 출판사  - 영어, 한글, 숫자 중 1개 이상");
        Console.WriteLine("                          : 수량    - 1~999 사이의 자연수");
        Console.WriteLine("                          : 가격    - 1~9999999 사이의 자연수");
        Console.WriteLine("                          : 출시일  - 19xx or 20xx-xx-xx");
        Console.WriteLine("                          : ISBN    - 국제표준 xxx-xx-xxxxxx-x-x ");
        Console.WriteLine("                          : 정보    - 최소1개의 문자(공백포함)\n");
        Console.ResetColor();
        Console.WriteLine("                 -------------------------------------------------------------------------------\n");
    }

    public void PrintAddingBookSuccessSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                    추가 완료\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                ENTER : 다시추가                     ESC : 뒤로가기\n\n");
        Console.WriteLine("                 -------------------------------------------------------------------------------\n\n\n");
        Console.WriteLine("                                                   ★추가 성공★\n\n\n");
    }

    public void PrintDeletingBookMenu()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                  삭제할 책 ID : \n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                ENTER : 확인                       ESC : 뒤로가기\n\n");
    }
    public void PrintFailNotExistInListSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                             책이 검색 목록에 없습니다!\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 다시입력                  ESC : 뒤로가기\n\n");
    }
    public void PrintDeletingBookFailAlreadyBorrowedSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                        책이 대여 중이라 삭제 할 수 없습니다!\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 다시입력                  ESC : 뒤로가기\n\n");
    }
    public void PrintDeletingBookSuccessSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                    책 삭제 완료!\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 다시삭제                  ESC : 뒤로가기\n\n");
    }

    public void PrintEditingBookAskingMenu()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                  수정할 책 ID : \n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                ENTER : 확인                       ESC : 뒤로가기\n\n");
    }
    public void PrintEditingBookFailAlreadyBorrowedSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                        책이 대여 중이라 수정 할 수 없습니다!\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 다시입력                  ESC : 뒤로가기\n\n");
    }
    public void PrintEditingBookMenu()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                     책  수정  \n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                ENTER : 확인                       ESC : 뒤로가기\n\n");
    }
    public void PrintEditingBookSuccessSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                   책  수정 완료! \n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 다시하기                    ESC : 뒤로가기\n\n");
    }
    public void PrintCurrentSavedBookInformation(BookDTO bookInformation)
    {
        Console.WriteLine("                                                       ◈현재 등록되어 있는 정보◈         \n\n");
        Console.WriteLine("                        책제목(영어,한글,숫자,?!+= 1개 이상): " + bookInformation.BookName);
        Console.WriteLine("                        작가 (  영어,한글 1글자 이상  )     : " + bookInformation.BookAuthor);
        Console.WriteLine("                        출판사 (영어,한글,숫자 1개 이상)    : " + bookInformation.BookPublisher);
        Console.WriteLine("                        수량 (    1~999 사이의 자연수    )  : " + bookInformation.BookQuantity);
        Console.WriteLine("                        가격 (  1~9999999 사이의 자연수  )  : " + bookInformation.BookPrice);
        Console.WriteLine("                        출시일 (  19xx or 20xx-xx-xx   )    : " + bookInformation.BookPublicationDate);
        Console.WriteLine("\n\n");
    }
    public void PrintEditingBookInformation()
    {
        Console.WriteLine("                                                       ◈새로 정보등록 하기◈         \n\n");
        Console.WriteLine("                        책제목(영어,한글,숫자,?!+= 1개 이상): ");
        Console.WriteLine("                        작가 (  영어,한글 1글자 이상  )     : ");
        Console.WriteLine("                        출판사 (영어,한글,숫자 1개 이상)    : ");
        Console.WriteLine("                        수량 (    0~999 사이의 정수    )  : ");
        Console.WriteLine("                        가격 (  1~9999999 사이의 자연수  )  : ");
        Console.WriteLine("                        출시일 (  19xx or 20xx-xx-xx   )    : ");
    }

    public void PrintMemberManagerMenu()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                             삭제할 유저 Number 입력 :\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 확인                     ESC : 뒤로가기\n\n");
    }

    public void PrintNotExistUser()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                             입력한 사용자가 없습니다 :\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 확인                     ESC : 뒤로가기\n\n");
    }

    public void PrintUserBorrowedSomeBook()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                    대여중인 도서가 있어 회원삭제가 불가능합니다. :\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 확인                     ESC : 뒤로가기\n\n");
    }

    public void PrintMemberList(UserDTO userInformation)
    {
        Console.WriteLine("===========================================================================================================\n");
        Console.WriteLine("유저 Number :{0}", userInformation.UserNumber);
        Console.WriteLine("유저 ID     :" + userInformation.Id);
        Console.WriteLine("유저 이름   :" + userInformation.UserName);
        Console.WriteLine("유저 나이   :{0}", userInformation.UserAge);
        Console.WriteLine("유저 번호   :" + userInformation.UserPhoneNumber);
        Console.WriteLine("유저 주소   :" + userInformation.UserAddress);
        Console.WriteLine("\n\n");
    }
    public void PrintDeletingUserSuccessSentence()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                 유저 삭제 성공 \n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 다시하기                 ESC : 뒤로가기\n\n");
    }

    public void PrintBookBorrowedMenu()
    {
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                 전체회원 대여상황 \n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                  ENTER : 확인                     ESC : 뒤로가기\n\n");
    }

    public void PrintUserName(string userName)
    {
        Console.WriteLine("========================================================================================================================");
        Console.WriteLine("User Name  :" + userName);
        Console.WriteLine("========================================================================================================================");
    }
    public void PrintUserBorrowedBookList(BookDTO bookInformation)
    {
        Console.WriteLine("책아이디  :  {0}", bookInformation.BookId);
        Console.WriteLine("책 제목   :  " + bookInformation.BookName);
        Console.WriteLine("작가      :  " + bookInformation.BookAuthor);
        Console.WriteLine("출판사    :  " + bookInformation.BookPublisher);
        Console.WriteLine("수량      :  {0}", bookInformation.BookQuantity);
        Console.WriteLine("가격      :  {0}", bookInformation.BookPrice);
        Console.WriteLine("출시일    :  " + bookInformation.BookPublicationDate);
        Console.WriteLine("ISBN      :  " + bookInformation.Isbn);
        Console.WriteLine("빌린 시간 :  " + bookInformation.BorrowTime);
        Console.WriteLine("반납 의무 시간 :  " + bookInformation.ReturnTime);
        Console.WriteLine("============================================================");
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
        Console.WriteLine("신청 도서가 없습니다.");
        Console.ResetColor();
        Console.WriteLine("========================================================================================================================");
    }
    public void PrintBookListAppliedBookList(BookDTO book)
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
    public void PrintBlueColorSentence(string sentence, int cursorPositionX, int cursorPositionY)
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
    public void ResetMenuScreen()
    {
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < 14; i++)
        {
            Console.WriteLine("                                                                                           ");
        }
    }

    public void PrintEditLogMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                   로그 삭제\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                삭제 로그 번호 :                                          \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }
    public void PrintLogList(LogDTO log)
    {
        Console.WriteLine("========================================================================================================================");
        Console.WriteLine("로그 ID     :  " + (log.LogNumber).ToString());
        Console.WriteLine("로그 시간   :  " + log.LogTime);
        Console.WriteLine("로그 사용자 :  " + log.LogUser);
        Console.WriteLine("로그 정보   :  " + log.LogInformation);
        Console.WriteLine("로그 활동   :  " + log.LogAction);
        Console.WriteLine("========================================================================================================================");
    }

    public void PrintResetLogMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                   로그 리셋\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                         초기화 하려면 ENTER을 눌러주세요                 \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }

    public void PrintSaveLogMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                         로그 TEXT 파일로 바탕화면에 저장\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                         저장 하려면 ENTER을 눌러주세요                 \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }

    public void PrintSaveLogSuccess()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                         로그 TEXT 파일로 바탕화면에 저장\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                      바탕화면에 성공적으로 저장 되었습니다!                 \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }
    public void PrintDeleteLogTextMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                       바탕화면에 저장된 로그 파일 삭제\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                         삭제 하려면 ENTER을 눌러주세요                 \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }
    public void PrintDeleteLogTextSuccess()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                         바탕화면에 저장된 로그 파일 삭제\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                           성공적으로 삭제 되었습니다!                 \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
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
    public void PrintAddBookInNaverMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                                   도서 추가\n");
        Console.WriteLine("                        ■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■\n");
        Console.WriteLine("                                 ENTER : 입력                      ESC : 뒤로가기\n\n");
        Console.WriteLine("                        ----------------------------------------------------------------\n");
        Console.WriteLine("                                추가 도서번호 :                                          \n");
        Console.WriteLine("                        -----------------------------------------------------------------\n");
    }
}
