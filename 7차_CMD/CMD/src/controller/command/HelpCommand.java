package controller.command;

import utility.Constants;
import view.CMDUI;

public class HelpCommand {
    public void printHelpPhrase(){
        CMDUI.printNotice(Constants.HELP_NOTICE);
    }

}
