package view.frames;

import controller.frameEvent.LoginFrameEvent;
import controller.frameEvent.SignUpFrameEvent;
import view.panels.LoginBasePanel;
import view.panels.SignUpBasePanel;

import javax.swing.*;

public class SignUpFrame extends JFrame {
    private SignUpBasePanel signUpBasePanel;

    //signUp 프레임 이벤트 리스너
    private SignUpFrameEvent signUpFrameEvent;
    public SignUpFrame(){
        setSignUpFrame(); //프레임 생성
        addPanelOnFrame(); //프레임 위에 패널 올리기
        addSetFrameEvent(); //프레임 이벤트 올리기 > 창 종료시 메인화면 버튼 활성화
        setVisible(true);
    }
    private void setSignUpFrame(){
        setSize(1200,800);
        setTitle("회원가입");
        setLayout(null);
        setLocationRelativeTo(null);
        setResizable(false);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE); //해당 프레임만 닫기
    }
    private void addPanelOnFrame(){
        signUpBasePanel = new SignUpBasePanel();
        this.add(signUpBasePanel);
    }
    private void addSetFrameEvent(){
        this.signUpFrameEvent = new SignUpFrameEvent(); //윈도우 이벤트 리스너 생성
        //로그인 창 종료시 버튼 활성화
        addWindowListener(signUpFrameEvent);
    }
}
