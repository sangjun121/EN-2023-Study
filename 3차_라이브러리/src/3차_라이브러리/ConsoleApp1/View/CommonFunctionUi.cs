using System;
public class CommonFunctionUi
{
    //싱글톤
    private static CommonFunctionUi instance;
    private CommonFunctionUi() { }
    public static CommonFunctionUi GetInstance()
    {
        if(instance == null)
        {
            instance = new CommonFunctionUi();
        }
        return instance;    
    }

    public void ViewMenu()
    {
        Console.SetCursorPosition(0, 21);
        Console.WriteLine("                       _________________________________________________________________                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                          메 뉴 선 택                          |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");
    }

    public void PrintBookFinderMenu()
    {
        Console.WriteLine("============================================================");
        Console.WriteLine(" 제목으로 찾기:");
        Console.WriteLine(" 작가명으로 찾기:");
        Console.WriteLine(" 출판사로 찾기:");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("ESC  :  뒤로가기");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("ENTER  :  입력하기");
        Console.ResetColor();
        Console.WriteLine("건너뛰고자 하는 항목은 ENTER을 눌러주세요");
        Console.WriteLine("============================================================");
    }
    public void PrintBookList(BookDTO book)
    {
        Console.WriteLine("책아이디  :  {0}", book.BookId);
        Console.WriteLine("책 제목   :  " + book.BookName);
        Console.WriteLine("작가      :  " + book.BookAuthor);
        Console.WriteLine("출판사    :  " + book.BookPublisher);
        Console.WriteLine("수량      :  {0}", book.BookQuantity);
        Console.WriteLine("가격      :  {0}", book.BookPrice);
        Console.WriteLine("출시일    :  " + book.BookPublicationDate);
        Console.WriteLine("ISBN      :  " + book.Isbn);
        Console.WriteLine("책 정보   :  " + book.BookDescription);
        Console.WriteLine("============================================================");
    }
    public void SelectEndorReturnInTheProgram()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("다시 검색하려면 ENTER, 메뉴선택으로 돌아가려면 ESC를 눌러주세요.");
        Console.ResetColor();
    }
    
}

