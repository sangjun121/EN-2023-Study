using System;

public partial class Ui
{
    public void PrintMenuUi()
    {
        Console.WriteLine("\n\n");
        Console.WriteLine("                             *************************************************");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                   [ MENU ]                    *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *              메뉴를 골라 주세요.              *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *             1. USER VS COMPUTER               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *             2. USER VS USER                   *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *             3. 종료하기                       *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *             4. 점수판 보기                    *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *************************************************");
        Console.Write("                                        ▶메뉴 입력:    ");

    }

    public void PrintGameBoard(string[,] room)
    {

        int i;
        int j;
        Console.WriteLine("\n\n\n\n");
        Console.WriteLine("                             *************************************************");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.Write("                             *                  ");

        for (i = 0; i < 3; i++)
        {   
            for (j = 0; j < 3; j++)
            {
                Console.Write(room[i, j]);
                Console.Write("    ");
            }
            Console.WriteLine("              *");
            Console.WriteLine("                             *                                               *");
            Console.WriteLine("                             *                                               *");
            Console.WriteLine("                             *                                               *");
            if (i != 2)
            {
                Console.Write("                             *                  ");
            }
        }
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *************************************************");

    }
        
    public void PrintUserScoreBoardUi(int user1, int user2)
    {

            Console.WriteLine("                             *************************************************");
            Console.WriteLine("                             *                                               *");
            Console.WriteLine("                             *                 SCREEN BOARD                  *");
            Console.WriteLine("                             *                                               *");
            Console.Write("                             *");
            Console.Write("             [USER 1] {0}: [USER 2] {1} ", user1, user2);
        Console.WriteLine("           *");
        Console.WriteLine("                             *                                               *");
            Console.WriteLine("                             *************************************************");
    }
    public void PrintComputerScoreBoardUi(int user1, int user2)
    {

        Console.WriteLine("                             *************************************************");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                 SCREEN BOARD                  *");
        Console.WriteLine("                             *                                               *");
        Console.Write("                             *");
        Console.Write("             [USER] {0}: [COMPUTER] {1} ", user1, user2);
        Console.WriteLine("           *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *************************************************");
    }

    public void DoItAgain()
    {
            Console.WriteLine("▶ 다시 하시겠습니까?");
            Console.WriteLine("        1. YES       ");
            Console.WriteLine("        2. NO        ");
            Console.WriteLine("입력하시오:");

    }
    
    public void WrongMenuNumInput()
    {
        Console.Write("                                        ▷잘못된 입력입니다. 다시 입력하세요: ");
    }

    public void WrongBoardNumInput()
    {
        Console.Write("                                        ▷잘못된 입력입니다. 다시 입력하세요: ");
    }
    public void PrintEndSign()
    {
        Console.WriteLine("                             *************************************************");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *               게임이 종료됩니다               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *************************************************");
    }
    public void PrintExitConfirmation()
    {
        Console.WriteLine("                             *************************************************");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *              A.메뉴 다시 선택하기             *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                 B.게임 종료하기               *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *************************************************");
    }
    public void PrintReAskExitConfirmation()
    {
        Console.WriteLine("                             *************************************************");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *             정말로 종료하시겠습니까?          *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *                 1. 종료하기                   *");
        Console.WriteLine("                             *                 2. 돌아가기                   *");
        Console.WriteLine("                             *                                               *");
        Console.WriteLine("                             *************************************************");
        Console.Write("                                            ▷선택 : ");
    }

}
