using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

public class UserLogin //유저모드 로그인 기능 클래스
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    MainMenuUi mainMenuUi;
    SignUpAndLoginUi signUpAndLoginUi;

    UserDAO userDAO;
    UserMenu usermenu;

    private string id;
    private string password;

    public UserLogin()
    { 
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.mainMenuUi = MainMenuUi.GetInstance();
        this.signUpAndLoginUi = SignUpAndLoginUi.GetInstance();

        this.userDAO = new UserDAO();
        this.usermenu = new UserMenu();
    }

    public void GetUserLogin()
    {
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수
        bool isJudgingCorrectInput = false; //로그인 성공여부를 판단하는 bool형 변수
        UserDTO loggedInUserInformation = null; //로그인 성공 후 로그인 한 유저 정보를 담을 DTO (빌린책 정보나 반납한 책 정보 저장할때 유저 아이디가 필요하기 때문)
        List<UserDTO> userinformation;

        while (isMenuExecute) {

            while (!isJudgingCorrectInput)
            {
                Console.Clear();
                mainMenuUi.ViewMainMenu();
                mainMenuUi.ViewMenuSquare();
                signUpAndLoginUi.PrintUserLoginMenu();  //메뉴 인터페이스 출력

                ReceiveIdAndPassword(); //아이디 비밀번호 입력 받기
                userinformation = userDAO.CompareUserAccountInformation(id); //일치하는 id가 있는지 판단

                if (userinformation.Count == 0) //리스트 원소가 없는경우 == 일치하는 id가 없는 경우
                {
                    Console.SetCursorPosition(40, 27);
                    Console.WriteLine("해당 아이디가 없습니다. 다시 입력하세요");
                    Console.ReadKey(true);
                }
                else //일치하는 id가 있는 경우
                {
                    if (userinformation[0].Password == password) //비밀번호가 일치할 경우 로그인 성공
                    {

                        loggedInUserInformation = userinformation[0];//로그인 한 회원정보 담기
                        isJudgingCorrectInput = true; //반복문 탈출
                    }
                    else //비밀번호가 틀릴경우
                    {
                        Console.SetCursorPosition(40, 27);
                        Console.WriteLine("비밀번호 입력이 틀렸습니다. 다시 입력하세요");
                        Console.ReadKey(true);
                    }
                }
            }

            Console.Clear();
            usermenu.ControllUserMenu(loggedInUserInformation); //유저 모드 메뉴로 진입
        }
   
        //뒤로가기
        isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
    }
    public void ReceiveIdAndPassword()
    {
        bool isJudgingCorrectString; //입력값 검사 후 탈출용 진리형 변수 

        isJudgingCorrectString = false;
        Console.SetCursorPosition(53, 23);
        while (!isJudgingCorrectString) //아이디 입력
        {
            id = InputByReadKey.ReceiveInput(53, 23, 15, Constants.IS_NOT_PASSWORD); //입력값 키값으로 검사
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(53, 23, id, Constants.USER_ID_REGULAR_EXPRESSION, Constants.USER_ID_ERROR_MESSAGE); //정규표현식 이용하여 검사
        }  //정규표현식 확인후 거짓일 때만 재실행 

        isJudgingCorrectString = false;
        Console.SetCursorPosition(61, 24);
        while (!isJudgingCorrectString) //비밀번호 입력
        {
            password = InputByReadKey.ReceiveInput(61, 24, 15, Constants.IS_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(53, 23, id, Constants.USER_PASSWORD_REGULAR_EXPRESSION, Constants.USER_PASSWORD_ERROR_MESSAGE);
        } 

    }
}
