using System;
using System.Runtime.InteropServices;

public partial class ScreenBoard
{   

    public int[] scoreSavedArr = { 0, 0 };

    public int PrintUserScreenBoard(int user1Score, int user2Score)
    {
        scoreSavedArr[0] = user1Score;
        scoreSavedArr[1] = user2Score;

        Ui ui = new Ui();
        ui.PrintUserScoreBoardUi(scoreSavedArr[0], scoreSavedArr[1]);
        ui.DoItAgain();
        int doOrNot = int.Parse(Console.ReadLine());
        
        if (doOrNot == 1)
        {
            return 1;
        }
        else if (doOrNot == 2)
        {
            return 2;
        }
        else
        {
            return 3;
            //예외처리
        }
    }

    public int PrintComputerScreenBoard(int user1Score, int user2Score)
    {
        
        scoreSavedArr[0] = user1Score;
        scoreSavedArr[1] = user2Score;

        Ui ui = new Ui();
        ui.PrintComputerScoreBoardUi(scoreSavedArr[0], scoreSavedArr[1]);
        
        ui.DoItAgain();
        int doOrNot = int.Parse(Console.ReadLine());
        if (doOrNot == 1)
        {
            return 1;
        }
        else if (doOrNot == 2)
        {
            return 2;
        }
        else
        {
            return 3;
            //예외처리
        }
    }
}