package controller.buttonEvent;

import view.frames.LoginFrame;
import view.frames.MainFrame;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class LoginPanelOpenButtonEvent implements ActionListener{
    public void actionPerformed(ActionEvent e) {
        //비활성화
        JButton loginPanelOpenButton = (JButton)e.getSource();
        loginPanelOpenButton.setEnabled(false);
        //로그인 프레임 생성
        new LoginFrame();
    }
}
