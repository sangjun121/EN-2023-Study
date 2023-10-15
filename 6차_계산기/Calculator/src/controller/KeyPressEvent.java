package controller;

import view.CalculatorFrame;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

public class KeyPressEvent {
    CalculatorFrame calculatorFrame;
    public KeyPressEvent(CalculatorFrame calculatorFrame){
        this.calculatorFrame = calculatorFrame;
    }
    public class KeyInputListener extends KeyAdapter  {
        public void keyPressed(java.awt.event.KeyEvent e) {
            switch (e.getKeyCode()) {
                case KeyEvent.VK_0:
                    calculatorFrame.calculatebuttons[17].doClick();
                    break;
                case KeyEvent.VK_1:
                    calculatorFrame.calculatebuttons[12].doClick();
                    break;
                case KeyEvent.VK_2:
                    calculatorFrame.calculatebuttons[13].doClick();
                    break;
                case KeyEvent.VK_3:
                    calculatorFrame.calculatebuttons[14].doClick();
                    break;
                case KeyEvent.VK_4:
                    calculatorFrame.calculatebuttons[8].doClick();
                    break;
                case KeyEvent.VK_5:
                    calculatorFrame.calculatebuttons[9].doClick();
                    break;
                case KeyEvent.VK_6:
                    calculatorFrame.calculatebuttons[10].doClick();
                    break;
                case KeyEvent.VK_7:
                    calculatorFrame.calculatebuttons[4].doClick();
                    break;
                case KeyEvent.VK_8: //shift 와 * 조합키 판단 즉, 곱하기 판단
                    if(e.isShiftDown()) calculatorFrame.calculatebuttons[7].doClick();
                    else calculatorFrame.calculatebuttons[5].doClick();
                    break;
                case KeyEvent.VK_9:
                    calculatorFrame.calculatebuttons[6].doClick();
                    break;
                case KeyEvent.VK_DELETE:
                    calculatorFrame.calculatebuttons[0].doClick();
                    break;
                case KeyEvent.VK_ESCAPE:
                    calculatorFrame.calculatebuttons[1].doClick();
                    break;
                case KeyEvent.VK_BACK_SPACE:
                    calculatorFrame.calculatebuttons[2].doClick();
                    break;
                case KeyEvent.VK_EQUALS://shift 와 = 조합키 즉, + 판단
                    if(e.isShiftDown()) calculatorFrame.calculatebuttons[15].doClick();
                    else calculatorFrame.calculatebuttons[19].doClick();
                    break;
                case KeyEvent.VK_PERIOD:
                    calculatorFrame.calculatebuttons[18].doClick();
                    break;
                case KeyEvent.VK_SLASH:
                    calculatorFrame.calculatebuttons[3].doClick();
                    break;
                case KeyEvent.VK_MINUS:
                    calculatorFrame.calculatebuttons[11].doClick();
                    break;
                case KeyEvent.VK_ENTER:
                    calculatorFrame.calculatebuttons[19].doClick();
                    break;
                case KeyEvent.VK_F9:
                    calculatorFrame.calculatebuttons[16].doClick(); //plusAndMinus 버튼
                    break;
            }
        }
    }
}