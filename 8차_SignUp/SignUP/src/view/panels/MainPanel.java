package view.panels;

import view.component.ComponentCreator;
import utility.Constants;

import javax.swing.*;
import java.awt.*;

public class MainPanel extends JPanel{
    private ComponentCreator componentCreator;
    private Image backgroundImage;

    //MainPanel에 올라가는 컨포넌트
    public static JButton loginPanelOpenButton;
    public MainPanel(){
        //컨포넌트 생성 클래스 불러오기
        this.componentCreator = ComponentCreator.getInstance();
        setMainPanel();
    }

    //배경사진 추가 메소드
    public void paintComponent(Graphics g){
        super.paintComponent(g);
        g.drawImage(backgroundImage, 0, 0, getWidth(), getHeight(), this);
    }

    private void setMainPanel(){
        //프레임 배경화면 설정
        backgroundImage = new ImageIcon(MainPanel.class.getResource(Constants.mainPanelBackgroundImagePath)).getImage();

        this.setLayout(null);
        this.setBounds(0,0,1200,750);
        //mainPanel위에 컨포넌트 올리기
        addSubPanelOnPanel();
    }
    private void addSubPanelOnPanel(){
        loginPanelOpenButton = componentCreator.createMainPanelComponent();
        add(loginPanelOpenButton);
    }
}
