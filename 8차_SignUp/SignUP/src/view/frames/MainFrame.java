package view.frames;

import view.panels.MainPanel;

import javax.swing.*;

public class MainFrame extends JFrame {

    private MainPanel mainPanel;

    public MainFrame(){
        setMainFrame(); //프레임 생성
        addPanelOnFrame(); //프레임 위에 패널 올리기
        setVisible(true);
    }

    private void setMainFrame(){
        setSize(1200,750);
        setTitle("지브리 스튜디오");
        setLayout(null);
        setLocationRelativeTo(null);
        setResizable(false);
        setDefaultCloseOperation(EXIT_ON_CLOSE);
    }

    private void addPanelOnFrame(){
        mainPanel = new MainPanel();
        this.add(mainPanel);
    }
}
