package controller.command;

import utility.Constants;
import view.CMDUI;

public class ClearScreenCommand { // cls 명령어
    public void clearAll(){
        CMDUI.printNotice(Constants.CLEAR_SCREEN);
    }
}
