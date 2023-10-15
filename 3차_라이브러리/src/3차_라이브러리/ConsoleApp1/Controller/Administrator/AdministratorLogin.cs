using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

public class AdministratorLogin
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    MainMenuUi mainMenuUi;
    SignUpAndLoginUi signUpAndLoginUi;

    AdministratorMenu administratorMenu;
    UserDAO userDAO;

    public AdministratorLogin( )
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.mainMenuUi = MainMenuUi.GetInstance();
        this.signUpAndLoginUi = SignUpAndLoginUi.GetInstance();

        this.administratorMenu = new AdministratorMenu();
        userDAO = new UserDAO();
    }

    private string id;
    private string password;

    public void GetAdministratorLogin()
    {
        bool isJudgingCorrectInput = false; //로그인 성공여부를 판단하는 bool형 변수
        List<UserDTO> administratorInformation = new List<UserDTO>();

        while (!isJudgingCorrectInput)
        {
            Console.Clear();
            mainMenuUi.ViewMainMenu();
            mainMenuUi.ViewMenuSquare();
            signUpAndLoginUi.PrintAdministratorLoginMenu();  //메뉴 인터페이스 출력

            ReceiveIdAndPassword(); //아이디 비밀번호 입력 받기
            administratorInformation = userDAO.CompareAdministratorAccountInformation(id); //일치하는 id가 있는지 판단

            if (administratorInformation.Count == 0) //리스트 원소가 없는경우 == 일치하는 id가 없는 경우
            {
                Console.SetCursorPosition(40, 27);
                Console.WriteLine("해당 아이디가 없습니다. 다시 입력하세요");
                Console.ReadKey(true);
            }
            else //일치하는 id가 있는 경우
            {
                if (administratorInformation[0].Password == password) //비밀번호가 일치할 경우 로그인 성공
                {
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
        administratorMenu.ControllAdministratorMenu(administratorInformation[0]); //관리자 모드 메뉴로 진입

    
    }

    public void ReceiveIdAndPassword()
    {
        bool isJudgingCorrectString;

        isJudgingCorrectString = false;
        Console.SetCursorPosition(53, 23);
        while (!isJudgingCorrectString)//정규표현식 확인후 거짓일 때만 재실행  
        {
            id = InputByReadKey.ReceiveInput(53, 23, 15, Constants.IS_NOT_PASSWORD); //아이디 입력
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(53, 23, id, Constants.USER_ID_REGULAR_EXPRESSION, Constants.USER_ID_ERROR_MESSAGE); //정규표현식 이용하여 검사
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(61, 24);
        while (!isJudgingCorrectString)
        {
            password = InputByReadKey.ReceiveInput(61, 24, 15, Constants.IS_PASSWORD);//비밀번호 입력
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(61, 24, password, Constants.USER_PASSWORD_REGULAR_EXPRESSION, Constants.USER_PASSWORD_ERROR_MESSAGE);
        }
    }

}

