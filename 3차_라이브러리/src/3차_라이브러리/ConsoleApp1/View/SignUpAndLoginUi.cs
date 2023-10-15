using System;

public class SignUpAndLoginUi
{
    //싱글턴 디자인 패턴
    private static SignUpAndLoginUi instance;
    private SignUpAndLoginUi() { }
    public static SignUpAndLoginUi GetInstance()
    {
        if (instance == null)
        {
            instance = new SignUpAndLoginUi();
        }
        return instance;
    }

    public void PrintSignUpMenu()
    {
        Console.SetCursorPosition(50, 22);
        Console.WriteLine("                                              ");
        Console.SetCursorPosition(50, 23);
        Console.WriteLine("회 원 가 입");
        Console.SetCursorPosition(34, 25);
        Console.WriteLine("ESC : 뒤로가기              ENTER : 입력하기  ");
    }
    public void PrintSignUpInputMenu()
    {
        Console.WriteLine("\n");
        Console.WriteLine("                       User ID (8~15글자 영어 ,숫자 포함) : ");
        Console.WriteLine("                       User PW (8~15글자 영어 ,숫자 포함) : ");
        Console.WriteLine("                       User PW (      PASSWORD 확인     ) : ");
        Console.WriteLine("                       User Name (한글,영어 포함 1글자 이상) : ");
        Console.WriteLine("                       User Age ( 0,자연수 0세 ~ 200세 ) : ");
        Console.WriteLine("                       User PhoneNumber (  01x-xxxx-xxxx  ) : ");
        Console.WriteLine("                       User Address (  도로명 주소 - 00시 00구  ) : ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("                       ex) 경기도 수원시 영통구 영통로 124");
        Console.WriteLine("                       ex) 서울특별시 강남구 남부순환로 지하 2744");
        Console.WriteLine("                       ex) 서울특별시 마포구 큰우물로 28");
        Console.ResetColor();
    }
    public void PrintAccountDeletionSentence(string name)
    {
        Console.WriteLine("                        _______________________________________________________________                        ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |              " + name + "님" + "   회원가입이 완료 되었습니다!           |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       |                                                               |                       ");
        Console.WriteLine("                       -----------------------------------------------------------------                       ");
    }
    public void PrintpasswordConfirmation(int cursorPositionX, int cursorPositionY)
    {
        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("비밀번호가 서로 다릅니다. 다시 입력해주세요");
        Console.ResetColor();
    }
    public void PrintUserLoginMenu()
    {
        Console.SetCursorPosition(50, 22);
        //Console.WriteLine("                           ");
        Console.SetCursorPosition(50, 22);
        Console.WriteLine("사용자 로그인");
        Console.SetCursorPosition(40, 23);
        //Console.WriteLine("                           ");
        Console.SetCursorPosition(40, 23);
        Console.WriteLine("아이디(ID) : ");
        Console.SetCursorPosition(40, 24);
        //Console.WriteLine("                             ");
        Console.SetCursorPosition(40, 24);
        Console.WriteLine("패스워드(PASSWORD) : ");

    }

    public void PrintAdministratorLoginMenu()
    {
        Console.SetCursorPosition(50, 22);
        Console.WriteLine("                          ");
        Console.SetCursorPosition(50, 22);
        Console.ForegroundColor= ConsoleColor.Green;
        Console.WriteLine("관리자 로그인");
        Console.ResetColor();
        Console.SetCursorPosition(40, 23);
        Console.WriteLine("                               ");
        Console.SetCursorPosition(40, 23);
        Console.WriteLine("아이디(ID) : ");
        Console.SetCursorPosition(40, 24);
        Console.WriteLine("                                  ");
        Console.SetCursorPosition(40, 24);
        Console.WriteLine("패스워드(PASSWORD) : ");

    }
}
