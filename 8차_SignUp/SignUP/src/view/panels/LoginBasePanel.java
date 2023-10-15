package view.panels;

import utility.Constants;
import view.component.ComponentCreator;
import model.LoginPanelComponentDTO;

import javax.swing.*;
import java.awt.*;

public class LoginBasePanel extends JPanel{
    private Image backgroundImage;

    //LoginBasePanel위에 올라가는 컨포넌트
    LoginPanel loginPanel;

    public LoginBasePanel(){
        setLoginBasePanel();
    }
    //배경사진 추가 메소드
    public void paintComponent(Graphics g){
        super.paintComponent(g);
        g.drawImage(backgroundImage, 0, 0, getWidth(), getHeight(), this);
    }

    private void setLoginBasePanel(){
        //프레임 배경화면 설정
        backgroundImage = new ImageIcon(MainPanel.class.getResource(Constants.loginBasePanelBackgroundImagePath)).getImage();
        this.setLayout(null);
        this.setBounds(0,0,450,600);
        addComponentOnLoginBasePanel();
    }

    private void addComponentOnLoginBasePanel(){
        loginPanel = new LoginPanel();
        add(loginPanel);
    }
}
