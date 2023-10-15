package controller.buttonEvent;

import view.frames.SignUpFrame;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class SignUpOpenButtonEvent implements ActionListener {
    public void actionPerformed(ActionEvent e) {
        JButton signUpOpenButton = (JButton)e.getSource();
        JFrame loginFrame = (JFrame)SwingUtilities.getWindowAncestor(signUpOpenButton); //상위 프레임 찾기
        loginFrame.dispose();
        new SignUpFrame();
    }
}
