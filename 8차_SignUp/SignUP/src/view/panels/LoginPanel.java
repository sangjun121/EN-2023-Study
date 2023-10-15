package view.panels;

import model.LoginPanelComponentDTO;
import utility.Constants;
import view.component.ComponentCreator;

import javax.swing.*;
import java.awt.*;

public class LoginPanel extends JPanel {
    private ComponentCreator componentCreator;
    private Image backgroundImage;

    //loginBasePanel에 올라가는 컨포넌트
    private LoginPanelComponentDTO loginPanelComponentDTO;

    public LoginPanel(){
        //컨포넌트 생성 클래스 불러오기
        this.componentCreator = ComponentCreator.getInstance();
        setLoginPanel();
    }

    //배경사진 추가 및 투명도 메소드
    public void paintComponent(Graphics g){
        float transparency;

        //배경사진 추가
        super.paintComponent(g);

        //투명도
        Graphics2D g2d =(Graphics2D) g.create();
        transparency = 0.99f;
        g2d.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, transparency));

        //배경 그리기
        g2d.drawImage(backgroundImage, 0, 0, getWidth(), getHeight(), this);
        g2d.dispose();
    }

    private void setLoginPanel(){
        //프레임 배경화면 설정
        backgroundImage = new ImageIcon(MainPanel.class.getResource(Constants.loginPanelBackgroundImagePath)).getImage();
        //배경색 추가
        setBackground(new Color(78,138,208,200));
        this.setLayout(null);
        this.setBounds(25,10,400,350);
        addComponentOnLoginPanel();
    }
    private void addComponentOnLoginPanel(){
        loginPanelComponentDTO = componentCreator.createLoginPanelComponent();
        add(loginPanelComponentDTO.getLoginButton());
        add(loginPanelComponentDTO.getSignUpOpenButton());
        add(loginPanelComponentDTO.getUserIDInputField());
        add(loginPanelComponentDTO.getUserPasswordInputField());
    }
}
