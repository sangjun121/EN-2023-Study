using System;

public class ExitConfirmation
{
	Ui ui = new Ui();

	public int JudgeExitCode(string selectedString)
	{
		int CHOOSINGMENUAGAIN = 0; //메뉴 다시 선택할때
        int ENDINGGAME = 1; //게임 종료할때
        int CHOOSINGERROR= 2; // 종료하기 버튼 잘못 눌렀을때
		string reallyQuit;
		string ENDGAME ="1";
		string REBACK = "2";

		if (selectedString == "A")
		{
            return CHOOSINGMENUAGAIN;

        }
        else if (selectedString == "B")
		{
			
            ui.PrintReAskExitConfirmation();
            reallyQuit = Console.ReadLine();
            if (reallyQuit == ENDGAME)
            {
                return ENDINGGAME;
            }
            else if (reallyQuit == REBACK)
            {
                return CHOOSINGERROR;
            }
        }
		
		return -1;
	}
}
