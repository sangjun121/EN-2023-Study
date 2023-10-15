using System;

public partial class ExceptionHandling
{

    public void SelectMenuWrongFix(string menuNumber) 
    {
        Ui ui = new Ui(); // (메소드 밖에 선언하면 안되는 이유 찾아보기)
        SelectingMenu selectMenu = new SelectingMenu();

        if ( (menuNumber != "1") && (menuNumber != "2") && (menuNumber != "3") && (menuNumber != "4"))
        {
            ui.WrongMenuNumInput();
            selectMenu.MenuFinder();
        }
        
    }
    public void PutNumberWrongFix(string number)
    {
        Ui ui = new Ui();
        
        if ( (number != "1") && (number != "2") && (number != "3") && (number != "4") && (number != "5") && (number != "6") && (number != "7") && (number != "8") && (number != "9") && (number != "A") && (number != "B"))
        {
            ui.WrongBoardNumInput();
            return;
        }
    }
}
