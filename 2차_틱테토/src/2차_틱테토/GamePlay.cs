using System;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

public partial class GamePlay
{
    string[,] room = new string[3, 3] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } }; //보드판 배열로 정의
    string[,] winCase = new string[8, 3] { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" }, { "1", "4", "7" }, { "2", "5", "8" }, { "3", "6", "9" }, { "1", "5", "9" }, { "3", "5", "7" } };
    //이기는 케이스 배열로 정리

    Ui ui = new Ui();
    ExceptionHandling exceptionHandling = new ExceptionHandling();
    ExitConfirmation exitConfirmation = new ExitConfirmation();

    int exitConfirmationNumber;
    int CHOOSINGMENUAGAIN = 0; //메뉴 다시 선택할때
    int ENDINGGAME = 1; //게임 종료할때
    int CHOOSINGERROR = 2; // 종료하기 버튼 잘못 눌렀을때



    public int PlayWithComputer(int returnResult1, int returnResult2) //컴퓨터 VS USER
    {
        int i, j, k, l, m;
        int[] userChoice = new int[5];
        int[] computerChoice = new int[4];
        string userNum;
        int numberOfUserDo = 0;  
        int numberOfComputerDo = 0;
        string userFirstLocation = " ";
        int winDistinction = 0;
        string winUser;
        int sameNumCount = 0;
        string notSameIndex = "0";
        int computerWinCount = 0, userWinCount = 0;


        while (true) { //사용자 선공, 컴퓨터 후공
                       //사용자 입력
            ui.PrintGameBoard(room); //보드판 출력
            ui.PrintExitConfirmation();

            Console.Write("                                        ▶ [USER]의 번호를 입력하세요:  ");
            userNum = Console.ReadLine();
            exceptionHandling.PutNumberWrongFix(userNum);
            exitConfirmationNumber = exitConfirmation.JudgeExitCode(userNum);
            if (exitConfirmationNumber == 0)
            {
                return CHOOSINGMENUAGAIN;
            }
            else if (exitConfirmationNumber == 1)
            {
                return ENDINGGAME;
            }
            else if (exitConfirmationNumber == 2)
            {
                ui.PrintGameBoard(room); //보드판 출력
                ui.PrintExitConfirmation();
                Console.Write("                                        ▶ [USER]의 번호를 입력하세요:  ");
                userNum = Console.ReadLine();
            }

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (userNum == room[i, j]) //사용자가 입력한 인덱스 찾아서 해당 배열에 저장
                    {
                        userChoice[numberOfUserDo] = int.Parse(room[i, j]); // 사용자가 입력한 칸의 숫자 정보 저장
                        room[i, j] = "O"; //사용자가 입력한 칸 O로 표기
                        numberOfUserDo++; //다음 인덱스로 표시하기 위해 1 증가

                        Console.Clear();
                        ui.PrintGameBoard(room); //보드판 출력

                        //밑의 내용은 프로그램 종료 조건이다. (이기거나, 비기거나 -> 9칸이 다 찼을때)
                        winDistinction = JudgeWinner("O");

                        if ((winDistinction == 1)) //중간에 이겼을 때
                        {
                            winUser = "user";
                            break;
                        }
                        if (numberOfUserDo + numberOfComputerDo == 9) //비겼을때
                        {
                            winUser = "DRAW";
                            break;
                        }
                        //여기까지

                        if (numberOfUserDo == 1) //처음 사용자가 입력하고 다음 컴퓨터가 어디를 차지해야 필승할지 정리한 메소드 호출 
                        {
                            userFirstLocation = JudgeFirstComputerChoice(i, j);
                        }
                    }

                }
                //밑의 내용은 프로그램 종료 조건이다. (이기거나, 비기거나 -> 9칸이 다 찼을때)
                winDistinction = JudgeWinner("O");

                if ((winDistinction == 1)) //중간에 이겼을 때
                {
                    winUser = "user";
                    break;
                }
                if (numberOfUserDo + numberOfComputerDo == 9) //비겼을때
                {
                    winUser = "DRAW";
                    break;
                }
                //여기까지
            }

            //밑의 내용은 프로그램 종료 조건이다. (이기거나, 비기거나 -> 9칸이 다 찼을때)
            winDistinction = JudgeWinner("O");

            if (winDistinction == 1) //중간에 이겼을 때
            {
                winUser = "user";
                break;
            }

            if ((numberOfUserDo + numberOfComputerDo) == 9) //비겼을때
            {
                winUser = "DRAW";
                break;
            }
            //여기까지

            Console.WriteLine();

            //컴퓨터 입력
            if (numberOfUserDo == 1) //사용자가 먼저 입력하고 컴퓨터가 "처음" 입력할때
            {
                if (userFirstLocation == "corner") //사용자가 처음에 모서리를 선택한 경우
                {
                    room[1, 1] = "X"; //중앙을 컴퓨터는 골라야 한다.
                    computerChoice[numberOfComputerDo] = 5; //중앙의 칸 값은 "5" 이므로 컴퓨터가 고른 숫자 배열에 5를 저장
                    ui.PrintGameBoard(room); //보드판 출력
                    numberOfComputerDo++; //다음 원소 접근을 위해 1증가

                }
                else if (userFirstLocation == "side") //사용자가 처음에 사이드를 선택한 경우
                {
                    room[1, 1] = "X"; //컴퓨터는 중앙을 골라야 한다.
                    computerChoice[numberOfComputerDo] = 5; //중앙의 칸 값은 "5" 이므로 컴퓨터가 고른 숫자 배열에 5를 저장
                    ui.PrintGameBoard(room); //보드판 출력
                    numberOfComputerDo++; //다음 원소 접근을 위해 1증가

                }
                else//중앙을 골랐을 경우
                {
                    room[0, 0] = "X"; //모서리를 고르면 되므로 임의의 모서리 (0,0)을 선택
                    computerChoice[numberOfComputerDo] = 1; //모서리 칸 값 "1" 저장
                    ui.PrintGameBoard(room); //보드판 출력
                    numberOfComputerDo++; // 다음 원소 접근을 위해 1 증가
                }
            }

            else
            {
                for (i = 0; i < 8; i++) //배열 열
                {
                    sameNumCount = 0;
                    for (j = 0; j < numberOfUserDo; j++) //사용자가 입력한 숫자 저장한 배열
                    {
                        for (k = 0; k < 3; k++) //배열 행
                        {
                            if (int.Parse(winCase[i, k]) == userChoice[j])
                            {
                                sameNumCount++;
                            }
                            else
                            {
                                notSameIndex = winCase[i, k];
                            }
                        }

                    }

                    if (sameNumCount == 2)
                    {
                        for (l = 0; l < 3; l++)
                        {
                            for (m = 0; m < 3; m++)
                            {
                                if (notSameIndex == room[l, m])
                                {
                                    room[l, m] = "X";
                                    computerChoice[numberOfComputerDo] = int.Parse(notSameIndex);
                                    ui.PrintGameBoard(room); //보드판 출력
                                    numberOfComputerDo++;
                                    //밑은 종료조건 이다.
                                    winDistinction = JudgeWinner("X");
                                    if ((winDistinction == 1))
                                    {
                                        winUser = "computer";
                                        break;
                                    }
                                    if (numberOfUserDo + numberOfComputerDo == 9)
                                    {
                                        winUser = "DRAW";
                                        break;
                                    }
                                    //여기까지
                                }
                            }
                            //밑은 종료조건 이다.
                            winDistinction = JudgeWinner("X");
                            if ((winDistinction == 1))
                            {
                                winUser = "computer";
                                break;
                            }
                            if (numberOfUserDo + numberOfComputerDo == 9)
                            {
                                winUser = "DRAW";
                                break;
                            }
                            //여기까지
                        }

                    }
                    //밑은 종료조건 이다.
                    winDistinction = JudgeWinner("X");
                    if ((winDistinction == 1))
                    {
                        winUser = "computer";
                        break;
                    }
                    if (numberOfUserDo + numberOfComputerDo == 9)
                    {
                        winUser = "DRAW";
                        break;
                    }
                    //여기까지
                }


            }
            //밑은 종료조건 이다.
            winDistinction = JudgeWinner("X");
            if ((winDistinction == 1))
            {
                winUser = "user";
                break;
            }
            if (numberOfUserDo + numberOfComputerDo == 9)
            {
                winUser = "DRAW";
                break;
            }
            //여기까지


        }

        if (winUser == "computer")
        {
            computerWinCount += 1;
        }
        else if (winUser == "user")
        {
            userWinCount += 1;
        }

        ScreenBoard screenBoard = new ScreenBoard();
        int DoOrNOt = screenBoard.PrintComputerScreenBoard(computerWinCount + returnResult1, userWinCount + returnResult2);
        if (DoOrNOt == 1)
        {
            GamePlay gamePlay = new GamePlay();
            gamePlay.PlayWithComputer(computerWinCount + returnResult1, userWinCount + returnResult2);
        }

        return -1;
    }

    public string JudgeFirstComputerChoice(int indexI, int indexJ)
    {
        if (((indexI == 0) && (indexJ == 0)) || ((indexI == 0) && (indexJ == 2)) || ((indexI == 2) && (indexJ == 0)) || ((indexI == 2) && (indexJ == 2)))
        {
            return "corner";
        }
        else if (((indexI == 0) && (indexJ == 1)) || ((indexI == 1) && (indexJ == 0)) || ((indexI == 2) && (indexJ == 1)) || ((indexI == 1) && (indexJ == 2)))
        {
            return "side";
        }
        else
        {
            return "center";
        }
    }


    public int JudgeWinner(string OorX) //종료 판독 메소드
    {
        int WIN=1;
        int NOTWIN=0;

        if ((room[0, 0] == OorX) && (room[1, 0] == OorX) && (room[2, 0] == OorX))
        {
            return WIN;
        }
        else if ((room[0, 1] == OorX) && (room[1, 1] == OorX) && (room[2, 1] == OorX))
        {
            return WIN;
        }
        else if ((room[0, 2] == OorX) && (room[1, 2] == OorX) && (room[2, 2] == OorX))
        {
            return WIN;
        }
        else if ((room[0, 0] == OorX) && (room[0, 1] == OorX) && (room[0, 2] == OorX))
        {
            return WIN;
        }
        else if ((room[1, 0] == OorX) && (room[1, 1] == OorX) && (room[1, 2] == OorX))
        {
            return WIN;
        }
        else if ((room[2, 0] == OorX) && (room[2, 1] == OorX) && (room[2, 2] == OorX))
        {
            return WIN;
        }
        else if ((room[0, 0] == OorX) && (room[1, 1] == OorX) && (room[2, 2] == OorX))
        {
            return WIN;
        }
        else if ((room[0, 2] == OorX) && (room[1, 1] == OorX) && (room[2, 0] == OorX))
        {
            return WIN;
        }
        else
        {
            return NOTWIN;
        }
    }


    public int PlayWithUser(int returnResult1, int returnResult2)
    {

        string user1Num;
        string user2Num;
        string winUser = "NONE";

        int user1WinCount = 0, user2WinCount = 0;
        int i, j;
        int PlayCount = 0;
        //ExceptionHandling exceptionHandling = new ExceptionHandling();

        while (true)
        {
            ui.PrintGameBoard(room); //보드판 출력
            ui.PrintExitConfirmation();

            Console.Write("                                        ▶ [USER1] 번호를 입력하세요:  ");
            user1Num = Console.ReadLine();    //user 1
            exceptionHandling.PutNumberWrongFix(user1Num);
            exitConfirmationNumber = exitConfirmation.JudgeExitCode(user1Num);
            if (exitConfirmationNumber == 0)
            {
                return CHOOSINGMENUAGAIN;
            }
            else if (exitConfirmationNumber == 1)
            {
                return ENDINGGAME;
            }
            else if (exitConfirmationNumber == 2)
            {
                ui.PrintGameBoard(room); //보드판 출력
                ui.PrintExitConfirmation();
                Console.Write("                                        ▶ [USER]의 번호를 입력하세요:  ");
                user1Num = Console.ReadLine();
            }

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (room[i, j] == user1Num)
                    {
                        room[i, j] = "O";
                        PlayCount++;
                    }

                }
            }

            ui.PrintGameBoard(room); //보드판 출력

            user1WinCount = JudgeWinner("O");
            if (user1WinCount == 1)
            {
                winUser = "user1";
                break;
            }
            if (PlayCount == 9) break;

            Console.Write("                                        ▶ [USER2] 번호를 입력하세요:  ");
            user2Num = Console.ReadLine(); //user 2
            exceptionHandling.PutNumberWrongFix(user2Num);
            exitConfirmationNumber = exitConfirmation.JudgeExitCode(user2Num);
            if (exitConfirmationNumber == 0)
            {
                return CHOOSINGMENUAGAIN;
            }
            else if (exitConfirmationNumber == 1)
            {
                return ENDINGGAME;
            }
            else if (exitConfirmationNumber == 2)
            {
                ui.PrintGameBoard(room); //보드판 출력
                ui.PrintExitConfirmation();
                Console.Write("                                        ▶ [USER]의 번호를 입력하세요:  ");
                user2Num = Console.ReadLine();
            }

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (room[i, j] == user2Num)
                    {
                        room[i, j] = "X";
                        PlayCount++;
                    }

                }
            }

            ui.PrintGameBoard(room);

            user2WinCount = JudgeWinner("X");
            if (user2WinCount == 1)
            {
                winUser = "user2";
                break;
            }

            if (PlayCount == 9) break;
        }

        if (winUser == "NONE")
        {
            if (PlayCount >= 9)
            {
                winUser = "DRAW";
            }
        }

        ScreenBoard screenBoard = new ScreenBoard();
        int DoOrNOt = screenBoard.PrintUserScreenBoard(user1WinCount + returnResult1, user2WinCount + returnResult2);
        if (DoOrNOt == 1)
        {
            GamePlay gamePlay = new GamePlay();
            gamePlay.PlayWithUser(user1WinCount + returnResult1, user2WinCount + returnResult2);
        }
        return -1;
    }
    
}