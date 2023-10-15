using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;

public class SignUp
{
    InputByReadKey inputByReadKey;
    RegularExpression regularExpression;
    MainMenuUi mainMenuUi;
    SignUpAndLoginUi signUpAndLoginUi;
    DataLogging dataLogging;

    UserDTO newUserInformation;
    UserDAO userDAO;

    public SignUp() 
    {
        this.inputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.mainMenuUi = MainMenuUi.GetInstance();
        this.signUpAndLoginUi = SignUpAndLoginUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.newUserInformation = new UserDTO();
        this.userDAO = new UserDAO();
    }

    public void SignUpAccount() //회원가입
	{
        bool isJudgingCorrectString = false;
        string passwordConfirmation = "";

        Console.Clear();
        mainMenuUi.ViewMainMenu();
        mainMenuUi.ViewMenuSquare();
        signUpAndLoginUi.PrintSignUpMenu();
        signUpAndLoginUi.PrintSignUpInputMenu();

        Console.SetCursorPosition(60, 28);
        while (!isJudgingCorrectString) //아이디 입력
        {
            newUserInformation.Id = inputByReadKey.ReceiveInput(60, 28, 15, Constants.IS_NOT_PASSWORD); //입력값 키값으로 검사
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(60, 28, newUserInformation.Id, Constants.USER_ID_REGULAR_EXPRESSION, Constants.USER_ID_ERROR_MESSAGE); //정규표현식 이용하여 검사
        } //정규표현식 확인후 거짓일 때만 재실행 

        isJudgingCorrectString = false;
        Console.SetCursorPosition(60, 29);
        while (!isJudgingCorrectString) //비밀번호 입력
        {
            newUserInformation.Password = inputByReadKey.ReceiveInput(60, 29, 15, Constants.IS_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(60, 29, newUserInformation.Password, Constants.USER_PASSWORD_REGULAR_EXPRESSION, Constants.USER_PASSWORD_ERROR_MESSAGE);
        } 
        
        while (true)
        {
            isJudgingCorrectString = false;
            Console.SetCursorPosition(60, 30);
            while (!isJudgingCorrectString) //비밀번호 확인 입력
            {
                passwordConfirmation = inputByReadKey.ReceiveInput(60, 30, 15, Constants.IS_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(60, 30, passwordConfirmation, Constants.USER_PASSWORD_REGULAR_EXPRESSION, Constants.USER_PASSWORD_ERROR_MESSAGE);
            } 
            
            if (passwordConfirmation != newUserInformation.Password) //비밀번호가 서로 다른 경우
            {
                signUpAndLoginUi.PrintpasswordConfirmation(60,30);
                Console.ReadKey(true);
                continue;
            }
            break;

        }
        isJudgingCorrectString = false;
        Console.SetCursorPosition(64, 31);
        while (!isJudgingCorrectString) //이름 입력
        {
            newUserInformation.UserName = inputByReadKey.ReceiveInput(64, 31, 15, Constants.IS_NOT_PASSWORD); 
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(64, 31, newUserInformation.UserName,Constants.USER_NAME_REGULAR_EXPRESSION,Constants.USER_NAME_ERROR_MESSAGE);
        }

        isJudgingCorrectString = false;
        Console.SetCursorPosition(59, 32);
        while (!isJudgingCorrectString)//나이 입력
        {
            newUserInformation.UserAge = int.Parse(inputByReadKey.ReceiveInput(59, 32, 3, Constants.IS_NOT_PASSWORD));
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(59, 32, (newUserInformation.UserAge).ToString(), Constants.USER_AGE_REGULAR_EXPRESSION, Constants.USER_AGE_ERROR_MESSAGE);
        } 

        isJudgingCorrectString = false;
        Console.SetCursorPosition(62, 33);
        while (!isJudgingCorrectString)//핸드폰 번호 입력
        {
            newUserInformation.UserPhoneNumber = inputByReadKey.ReceiveInput(62, 33, 13, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(62, 33, newUserInformation.UserPhoneNumber, Constants.USER_NUMBER_REGULAR_EXPRESSION, Constants.USER_NUMBER_ERROR_MESSAGE);
        } 

        isJudgingCorrectString = false;
        Console.SetCursorPosition(69, 34);
        while (!isJudgingCorrectString)//주소 입력
        {
            newUserInformation.UserAddress = inputByReadKey.ReceiveInput(69, 34, 30, Constants.IS_NOT_PASSWORD);
            isJudgingCorrectString = true; //아직 주소 정규식 처리 미완료
        }

        userDAO.NewUserDataCreate(newUserInformation);      //DAO로 새 유저정보 넘겨주기

        //로그 수집
        dataLogging.SetLog(newUserInformation.UserName, newUserInformation.Id, Constants.SIGN_UP);

        Console.Clear();
        signUpAndLoginUi.PrintAccountDeletionSentence(newUserInformation.UserName);
    }
}
