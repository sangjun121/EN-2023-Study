using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System.Collections.Generic;
using System;
using System.Threading;

public class LibraryMode
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    MainMenuUi mainMenuUi;
    SignUpAndLoginUi signUpAndLoginUi;
    MenuSelectController menuSelectController;

    UserLogin login;
    SignUp signUp;
    AdministratorLogin administratorLogin;

    public LibraryMode()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.mainMenuUi = MainMenuUi.GetInstance();
        this.signUpAndLoginUi = SignUpAndLoginUi.GetInstance();
        this.menuSelectController = MenuSelectController.GetInstance();

        this.login = new UserLogin();
        this.signUp = new SignUp();
        this.administratorLogin = new AdministratorLogin();
    }

    public void SelectMenu()
    {
        string[] mainMenuList = { "○ 유저모드", "○ 관리자 모드" };
        string[] userMenuList = { "○ 로그인", "○ 회원가입" };
        int mainMenuNumber;
        int userMenuNumber;
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수

        while (isMenuExecute)
        {
            Console.Clear();
            mainMenuUi.ViewMainMenu();
            mainMenuUi.ViewMenuSquare();

            mainMenuNumber = menuSelectController.SelectMenuWithUpAndDown(mainMenuList, 2, 50, 23);

            if (mainMenuNumber == (int)(ModeNumber.USER_MODE)) //유저 모드 진입
            {
                Console.Clear();
                mainMenuUi.ViewMainMenu();
                mainMenuUi.ViewMenuSquare();

                userMenuNumber = menuSelectController.SelectMenuWithUpAndDown(userMenuList, 2, 50, 23);
              
                if (userMenuNumber == (int)(LoginOrSignUpNumber.LOGIN))
                {
                    login.GetUserLogin();
                }
                else if (userMenuNumber == (int)(LoginOrSignUpNumber.SIGN_UP))
                {
                    signUp.SignUpAccount();
                }
            }

            else if (mainMenuNumber == (int)(ModeNumber.ADMIN_MODE))//관리자 모드 진입
            {
                administratorLogin.GetAdministratorLogin();
            }

            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }
	

}
