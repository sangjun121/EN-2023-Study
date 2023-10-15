using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.View
{
    public class MenuUi
    {
        //싱글턴 디자인 패턴
        private static MenuUi instance;
        private MenuUi() { }
        public static MenuUi GetInstance()
        {
            if (instance == null)
            {
                instance = new MenuUi();
            }
            return instance;
        }

        public void PrintColorSentence(int cursorPositionX, int cursorPositionY, string menuIndexLine) //초록색 줄 처리 함수
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(menuIndexLine);
            Console.ResetColor();
        }

        public void PrintNotColorSentence(int cursorPositionX, int cursorPositionY, string menuIndexLine) //기존 리셋값 흰색 줄 처리 함수
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.Write(menuIndexLine);
        }

    }
}
