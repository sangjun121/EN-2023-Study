package controller.frameEvent;

import view.panels.MainPanel;

import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;

public class SignUpFrameEvent extends WindowAdapter {
    public void windowClosing(WindowEvent e){
        MainPanel.loginPanelOpenButton.setEnabled(true);
    }
}
