using ConsoleApp1.Model;
using ConsoleApp1.Utility;
using System;

public class EditingUserInformation
{
    InputByReadKey InputByReadKey;
    RegularExpression regularExpression;
    UserModeUi userModeUi;
    DataLogging dataLogging;

    UserDAO userDAO;

    public EditingUserInformation()
    {
        this.InputByReadKey = InputByReadKey.GetInstance();
        this.regularExpression = RegularExpression.GetInstance();
        this.userModeUi = UserModeUi.GetInstance();
        this.dataLogging = DataLogging.GetInstance();

        this.userDAO = new UserDAO();
    }

    bool isJudgingCorrectString;
    public void EditUserInformation(UserDTO loggedInUserInformation)
	{
        string newId="";
        string newPassword="";
        String newName = "";
        string newAge = "";
        string newPhoneNumber = "";
        String newAddress = "";
        bool isJudgingCorrectString;
        bool isMenuExecute = true; //메뉴 탈출 진리형 변수

        while (isMenuExecute)
        {
            Console.Clear();
            userModeUi.PrintBeforeUserInformation(loggedInUserInformation); // 기존 유저 정보 출력하는 UI
            userModeUi.PrintAfterUserInformation(loggedInUserInformation); //유저 정보 수정하는 UI

            isJudgingCorrectString = false;
            Console.SetCursorPosition(54, 22);
            while (!isJudgingCorrectString)//아이디 입력
            {
                newId = InputByReadKey.ReceiveInput(54, 22, 15, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(54, 22, newId, Constants.USER_ID_REGULAR_EXPRESSION, Constants.USER_ID_ERROR_MESSAGE);
            }

            isJudgingCorrectString = false;
            Console.SetCursorPosition(54, 23);
            while (!isJudgingCorrectString) //비밀번호 입력
            {
                newPassword = InputByReadKey.ReceiveInput(54, 23, 15, Constants.IS_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(54, 23, newPassword, Constants.USER_PASSWORD_REGULAR_EXPRESSION, Constants.USER_PASSWORD_ERROR_MESSAGE);
            }

            isJudgingCorrectString = false;
            Console.SetCursorPosition(57, 24);
            while (!isJudgingCorrectString)//이름 입력
            {
                newName = InputByReadKey.ReceiveInput(57, 24, 10, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(57, 24, newName, Constants.USER_NAME_REGULAR_EXPRESSION, Constants.USER_NAME_ERROR_MESSAGE);
            } 

            isJudgingCorrectString = false;
            Console.SetCursorPosition(54, 25);
            while (!isJudgingCorrectString)//나이 입력
            {
                newAge = InputByReadKey.ReceiveInput(54, 25, 3, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(54, 25, newAge, Constants.USER_AGE_REGULAR_EXPRESSION, Constants.USER_AGE_ERROR_MESSAGE);
            }

            isJudgingCorrectString = false;
            Console.SetCursorPosition(57, 26);
            while (!isJudgingCorrectString)//휴대폰 번호 입력
            {
                newPhoneNumber = InputByReadKey.ReceiveInput(57, 26, 13, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(57, 26, newPhoneNumber, Constants.USER_NUMBER_REGULAR_EXPRESSION, Constants.USER_NUMBER_ERROR_MESSAGE);
            } 

            isJudgingCorrectString = false;
            Console.SetCursorPosition(60, 27);
            while (!isJudgingCorrectString)//주소 입력
            {
                newAddress = InputByReadKey.ReceiveInput(60, 27, 30, Constants.IS_NOT_PASSWORD);
                isJudgingCorrectString = true;  //아직 주소 정규표현식 미 구현
            }
          
            UserDTO editedUserInformation = new UserDTO();

            //데이터에 입력 받은 값 저장 //생성자로 저장해서 건네기
            editedUserInformation.UserNumber = loggedInUserInformation.UserNumber;
            editedUserInformation.Id = newId;
            editedUserInformation.Password = newPassword;
            editedUserInformation.UserName = newName;
            editedUserInformation.UserAddress = newAddress;
            editedUserInformation.UserAge = int.Parse(newAge);
            editedUserInformation.UserPhoneNumber = newPhoneNumber;

            userDAO.EditUserDataUpdate(editedUserInformation);

            //로그 수집
            dataLogging.SetLog(loggedInUserInformation.UserName, loggedInUserInformation.Id + Constants.CHANGE + newId, Constants.EDIT_USER_INF);

            Console.Clear();
            userModeUi.PrintUserInformationUpdateSuccess();

            //뒤로가기
            isMenuExecute = GoBackMenu.GetInstance().GoBackToBeforeFunction();
        }
    }
}
