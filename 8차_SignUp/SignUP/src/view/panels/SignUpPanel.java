package view.panels;

import model.LoginPanelComponentDTO;
import model.SignUpPanelComponentDTO;
import utility.Constants;
import view.component.ComponentCreator;

import javax.swing.*;
import java.awt.*;

public class SignUpPanel extends JPanel {
    private ComponentCreator componentCreator;
    private Image backgroundImage;

    //SignUpPanel에 올라가는 컨포넌트
    private SignUpPanelComponentDTO signUpPanelComponentDTO;
    public SignUpPanel(){
        //컨포넌트 생성 클래스 불러오기
        this.componentCreator = ComponentCreator.getInstance();
        setSignUpPanel();
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
    private void setSignUpPanel(){
        //프레임 배경화면 설정
        backgroundImage = new ImageIcon(MainPanel.class.getResource(Constants.signUpPanelBackgroundImagePath)).getImage();
        this.setLayout(null);
        this.setBounds(100,100,1000,600);
        addComponentOnSignUpPanel();
    }
    private void addComponentOnSignUpPanel(){
        signUpPanelComponentDTO = componentCreator.createSignUpPanelComponent();
        add(signUpPanelComponentDTO.getNewUserName());
        add(signUpPanelComponentDTO.getNewUserID());
        add(signUpPanelComponentDTO.getNewUserPassword());
        add(signUpPanelComponentDTO.getNewUserPasswordCheck());
        add(signUpPanelComponentDTO.getNewUserBirthDay());
        add(signUpPanelComponentDTO.getNewUserEmail());
        add(signUpPanelComponentDTO.getNewUserPhoneNumber());
        add(signUpPanelComponentDTO.getNewUserAddress());
        add(signUpPanelComponentDTO.getNewUserZipCode());
        add(signUpPanelComponentDTO.getAddressSearchingButton());
        add(signUpPanelComponentDTO.getSignUpButton());
    }

}
