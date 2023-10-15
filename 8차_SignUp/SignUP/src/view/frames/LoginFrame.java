package view.frames;

import controller.frameEvent.LoginFrameEvent;
import view.panels.LoginBasePanel;
import view.panels.MainPanel;

import javax.swing.*;
import java.awt.*;

public class LoginFrame extends JFrame {
    private LoginBasePanel loginBasePanel;

    //login프레임 이벤트 리스너
    private LoginFrameEvent loginFrameEvent;

    public LoginFrame(){
        setLoginFrame(); //프레임 생성
        addPanelOnFrame(); //프레임 위에 패널 올리기
        addSetFrameEvent(); //프레임 이벤트 올리기 > 창 종료시 메인화면 버튼 활성화
        setVisible(true);
    }
    private void setLoginFrame(){
        setSize(450,600);
        setTitle("로그인");
        setLayout(null);
        setLocationRelativeTo(null);
        setResizable(false);
        setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE); //해당 프레임만 닫기
    }

    private void addPanelOnFrame(){
        loginBasePanel = new LoginBasePanel();
        this.add(loginBasePanel);
    }
    private void addSetFrameEvent(){
        this.loginFrameEvent = new LoginFrameEvent(); //윈도우 이벤트 리스너 생성
        //로그인 창 종료시 버튼 활성화
        addWindowListener(loginFrameEvent);
    }
}
