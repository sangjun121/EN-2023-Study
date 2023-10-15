using System;
using System.Runtime.Intrinsics.X86;

public partial class SelectingMenu
{
	string menuNumber;
	int intMenuNumber;
	

    ExceptionHandling exceptionHandling = new ExceptionHandling();
	Ui ui = new Ui();
	ScreenBoard screenBoard = new ScreenBoard();
    GamePlay gamePlay = new GamePlay();


    public void MenuFinder() // 메뉴 고르기
	{
		int judgingEnd;

		while (true)
		{
            ui.PrintMenuUi();
            string menuNumber = Console.ReadLine(); //메뉴 번호 입력받기
			intMenuNumber = int.Parse(menuNumber);

			exceptionHandling.SelectMenuWrongFix(menuNumber); //예외처리
			Console.Clear();


			if (intMenuNumber == 1)
			{
                judgingEnd = gamePlay.PlayWithComputer(0, 0);
				if (judgingEnd == 1)
				{
					break;
				}
			}
			else if (intMenuNumber == 2)
			{
				gamePlay.PlayWithUser(0, 0);
			}
			else if (intMenuNumber == 3)
			{
				//프로그램 종료 위해 while 탈출
				ui.PrintEndSign();
                break;
			}
            else if (intMenuNumber == 4)
            {
				// 점수판 출력
				ui.PrintUserScoreBoardUi(screenBoard.scoreSavedArr[0], screenBoard.scoreSavedArr[1]);
            }
        }
		return; //프로그램 종료
	}
}
