package controller.frameEvent;

import view.frames.MainFrame;
import view.panels.MainPanel;

import java.awt.*;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;

public class LoginFrameEvent extends WindowAdapter {
    public void windowClosing(WindowEvent e){
        MainPanel.loginPanelOpenButton.setEnabled(true);
    }
}
