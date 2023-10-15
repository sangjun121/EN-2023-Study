package view.component;

import controller.buttonEvent.LoginPanelOpenButtonEvent;
import controller.buttonEvent.SignUpOpenButtonEvent;
import model.LoginPanelComponentDTO;
import model.SignUpPanelComponentDTO;
import utility.Constants;

import javax.swing.*;
import java.awt.*;

public class ComponentCreator {
    //싱글턴 처리
    private static ComponentCreator instance = new ComponentCreator();
    private ComponentCreator() {}
    public static ComponentCreator getInstance() {
        return instance;
    }

    //Panel클래스로 보낼 컨포넌트 DTO
    LoginPanelComponentDTO loginPanelComponentDTO;
    SignUpPanelComponentDTO signUpPanelComponentDTO;
    //버튼 이벤트
    LoginPanelOpenButtonEvent loginPanelOpenButtonEvent;
    SignUpOpenButtonEvent signUpOpenButtonEvent;

    //MainPanel Component
    private JButton loginPanelOpenButton;
    //LoginPanel component
    private JButton loginButton;
    private JButton signUpOpenButton;
    private JTextField userIDInputField;
    private JPasswordField userPasswordInputField;
    //SignUpPanel Component
    private JTextField newUserName;
    private JTextField newUserID;
    private JPasswordField newUserPassword;
    private JPasswordField newUserPasswordCheck;
    private JTextField newUserBirthDay;
    private JTextField newUserEmail;
    private JTextField newUserPhoneNumber;
    private JTextField newUserAddress;
    private JTextField newUserZipCode;
    private JButton addressSearchingButton;
    private JButton signUpButton;

    //MainPanel Component 생성
    public JButton createMainPanelComponent(){
        loginPanelOpenButton = new JButton();
        loginPanelOpenButton.setBounds(420,600,350,135); //버튼 사이즈 지정

        //버튼에 이미지 처리
        loginPanelOpenButton = putImageOnButton(Constants.loginPanelOpenButtonBaseImagePath, Constants.loginPanelOpenButtonHoverImagePath, loginPanelOpenButton,loginPanelOpenButton.getWidth(),loginPanelOpenButton.getHeight());

        //버튼에 이벤트 처리
        loginPanelOpenButtonEvent = new LoginPanelOpenButtonEvent();
        loginPanelOpenButton.addActionListener(loginPanelOpenButtonEvent);
        return loginPanelOpenButton;
    }

    //LoginPanel Component 생성
    public LoginPanelComponentDTO createLoginPanelComponent(){
        loginPanelComponentDTO = new LoginPanelComponentDTO();
        //컨포넌트 생성
        loginButton = new JButton();
        signUpOpenButton = new JButton();
        userIDInputField = new JTextField();
        userPasswordInputField = new JPasswordField();

        //컨포넌트 속성 추가(크기,위치,투명도)
        loginButton.setBounds(51,232,300,50);
        signUpOpenButton.setBounds(110,310,180,20);
        userIDInputField.setBounds(100,102,250,40);
        userPasswordInputField.setBounds(100,170,250,40);
        loginButton.setBorderPainted(false);
        signUpOpenButton.setBorderPainted(false);
        userIDInputField.setBorder(null);
        userPasswordInputField.setBorder(null);

        //컨포넌트 이벤트 추가
        signUpOpenButtonEvent = new SignUpOpenButtonEvent();
        signUpOpenButton.addActionListener(signUpOpenButtonEvent);

        loginPanelComponentDTO.setLoginButton(loginButton);
        loginPanelComponentDTO.setSignUpOpenButton(signUpOpenButton);
        loginPanelComponentDTO.setUserIDInputField(userIDInputField);
        loginPanelComponentDTO.setUserPasswordInputField(userPasswordInputField);

        return loginPanelComponentDTO;
    }

    //SignUpPanel Component 생성
    public SignUpPanelComponentDTO createSignUpPanelComponent() {
        signUpPanelComponentDTO = new SignUpPanelComponentDTO();
        //컴포넌트 생성
        newUserName = new JTextField();
        newUserID = new JTextField();
        newUserPassword = new JPasswordField();
        newUserPasswordCheck = new JPasswordField();
        newUserBirthDay = new JTextField();
        newUserEmail = new JTextField();
        newUserPhoneNumber = new JTextField();
        newUserAddress = new JTextField();
        newUserZipCode = new JTextField();
        addressSearchingButton = new JButton();
        signUpButton = new JButton();

        //컴포넌트 속성 추가
        newUserName.setBounds(130,120,250,30);
        newUserID.setBounds(130,190,250,30);
        newUserPassword.setBounds(130,265,250,30);
        newUserPasswordCheck.setBounds(130,340,250,30);
        newUserBirthDay.setBounds(130,415,250,30);
        newUserEmail.setBounds(600,120,220,30);
        newUserPhoneNumber.setBounds(600,190,220,30);
        newUserAddress.setBounds(600,265,220,30);
        newUserZipCode.setBounds(600,340,220,30);
        addressSearchingButton.setBounds(850,325,70,50);
        signUpButton.setBounds(750, 500, 350, 140);


        //경계선 제거
        newUserName.setBorder(null);
        newUserID.setBorder(null);
        newUserPassword.setBorder(null);
        newUserPasswordCheck.setBorder(null);
        newUserBirthDay.setBorder(null);
        newUserEmail.setBorder(null);
        newUserPhoneNumber.setBorder(null);
        newUserAddress.setBorder(null);
        newUserZipCode.setBorder(null);

        //버튼에 속성 처리
        addressSearchingButton.setBorderPainted(false);
        signUpButton = putImageOnButton(Constants.signUpButtonBaseImagePath, Constants.signUpButtonHoverImagePath, signUpButton,signUpButton.getWidth(),signUpButton.getHeight());

        signUpPanelComponentDTO.setNewUserName(newUserName);
        signUpPanelComponentDTO.setNewUserID(newUserID);
        signUpPanelComponentDTO.setNewUserPassword(newUserPassword);
        signUpPanelComponentDTO.setNewUserPasswordCheck(newUserPasswordCheck);
        signUpPanelComponentDTO.setNewUserBirthDay(newUserBirthDay);
        signUpPanelComponentDTO.setNewUserEmail(newUserEmail);
        signUpPanelComponentDTO.setNewUserPhoneNumber(newUserPhoneNumber);
        signUpPanelComponentDTO.setNewUserAddress(newUserAddress);
        signUpPanelComponentDTO.setNewUserZipCode(newUserZipCode);
        signUpPanelComponentDTO.setAddressSearchingButton(addressSearchingButton);
        signUpPanelComponentDTO.setSignUpButton(signUpButton);
        return signUpPanelComponentDTO;
    }

    //버튼에 이미지 올리는 함수
    private JButton putImageOnButton(String imageBasePath, String imageHoverPath , JButton button, int buttonWidth, int buttonHeight){
        //버튼 기본 UI값들 삭제
        button.setBorderPainted(false);
        button.setFocusPainted(false);
        button.setFocusable(false);
        button.setContentAreaFilled(false);

        //이미지 아이콘 생성
        ImageIcon baseImage = new ImageIcon(ComponentCreator.class.getResource(imageBasePath));
        ImageIcon hoverImage = new ImageIcon(ComponentCreator.class.getResource(imageHoverPath));
        //이미지 사이즈 변경
        Image resizedBaseImage = baseImage.getImage().getScaledInstance(buttonWidth,buttonHeight,Image.SCALE_SMOOTH);
        Image resizedHoverImage = hoverImage.getImage().getScaledInstance(buttonWidth,buttonHeight,Image.SCALE_SMOOTH);
        //사이즈 변경된 이미지 다시 아이콘으로 변환
        baseImage = new ImageIcon(resizedBaseImage);
        hoverImage = new ImageIcon(resizedHoverImage);

        //버튼에 이미지 올리기 및 호버처리
        button.setIcon(baseImage);
        button.setRolloverIcon(hoverImage);

        return button;
    }
}
