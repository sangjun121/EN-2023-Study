using System;

 public class MainMenuUi
{
    //싱글턴 디자인 패턴
    private static MainMenuUi instance;
    private MainMenuUi() { }
    public static MainMenuUi GetInstance()
    {
        if (instance == null)
        {
            instance = new MainMenuUi();
        }
        return instance;
    }

    public void ViewMainMenu()
    {

        Console.WriteLine("\n\n\n\n");
        Console.WriteLine("           ####       ####    #########      ########             ###       ########      ####     ####");
        Console.WriteLine("            ##         ##      ##      ##     ##     ##           ###        ##     ##     ##       ##");
        Console.WriteLine("            ##         ##      ##     ###     ##      ##         ## ##       ##      ##     ##     ##");
        Console.WriteLine("            ##         ##      ##  ##         ##     ##         ##   ##      ##     ##       ##  ##");
        Console.WriteLine("            ##         ##      ##  ##         ##  ###          #########     ##   ###          ###");
        Console.WriteLine("            ##         ##      ##    ###      ####   ##       ##       ##    ####    ##        ##");
        Console.WriteLine("            ##         ##      ##     ###     ##       ##    ##         ##   ##       ##       ##");
        Console.WriteLine("           #########  ####    #########      ####       ### ##           ## ####       ###    ####");
        Console.WriteLine("\n\n\n\n");
        Console.WriteLine("               ENTER : 선택                                                           ESC : 나가기");
        Console.WriteLine("\n");
    }

    public void ViewMenuSquare()
    {
        Console.WriteLine("             _____________________________________________________________________________________               ");
        Console.WriteLine("            |                                                                                     |              ");
        Console.WriteLine("            |                                                                                     |              ");
        Console.WriteLine("            |                                                                                     |              ");
        Console.WriteLine("            |                                                                                     |              ");
        Console.WriteLine("            ---------------------------------------------------------------------------------------                      ");
    }

}

