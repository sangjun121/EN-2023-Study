package view.panels;

import utility.Constants;

import javax.swing.*;
import java.awt.*;

public class SignUpBasePanel extends JPanel {
    private Image backgroundImage;
    //SignUpBasePanel위에 올라가는 컨포넌트
    SignUpPanel signUpPanel;

    public SignUpBasePanel(){
        setSignUpBasePanel();
    }
    //배경사진 추가 메소드
    public void paintComponent(Graphics g){
        super.paintComponent(g);
        g.drawImage(backgroundImage, 0, 0, getWidth(), getHeight(), this);
    }
    private void setSignUpBasePanel(){
        //프레임 배경화면 설정
        backgroundImage = new ImageIcon(MainPanel.class.getResource(Constants.signUpBasePanelBackgroundImagePath)).getImage();
        this.setLayout(null);
        this.setBounds(0,0,1200,800);
        addComponentOnSignUpBasePanel();
    }
    private void addComponentOnSignUpBasePanel(){
        signUpPanel = new SignUpPanel();
        add(signUpPanel);
    }
}
