using ConsoleApp1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class MenuSelectController //싱글톤 처리
    {
        private static MenuSelectController instance;
 
        public static MenuSelectController GetInstance()
        {
            if (instance == null)
            {
                instance = new MenuSelectController();
            }
            return instance;
        }

        MenuUi menuUi;
        private MenuSelectController( )
        {
            this.menuUi = MenuUi.GetInstance();
        }

        public int SelectMenuWithUpAndDown(string[] menuList, int menuNumber, int cursorPositionX, int cursorPositionY)
        //위아래 키 입력 받고 처리하는 함수
        {
            ConsoleKeyInfo inputKey;
            bool isEnter = false;
            int selectedMenu = 0;

            Console.CursorVisible = false;
            SetAndPrintColorMenuSentence(menuList, selectedMenu, cursorPositionX, cursorPositionY); //색깔 적용 후 메뉴 리스트 출력해 주는 함수

            while (!isEnter) //위아래 키에 맞게 메뉴의 번호 설정하기
            {
                inputKey = Console.ReadKey();
                if ((inputKey.Key == ConsoleKey.UpArrow) && (selectedMenu > 0))
                {
                    selectedMenu--;
                }
                else if ((inputKey.Key == ConsoleKey.DownArrow) && (selectedMenu < (menuNumber - 1)))
                {
                    selectedMenu++;
                }
                else if ((inputKey.Key == ConsoleKey.Enter))
                {
                    isEnter = true;
                }

                SetAndPrintColorMenuSentence(menuList, selectedMenu, cursorPositionX, cursorPositionY);
            }

            return selectedMenu;
        }

        public void SetAndPrintColorMenuSentence(string[] menuList, int selectedMenu, int cursorPositionX, int cursorPositionY) //해당 메뉴 위치 보여주는 함수
        {   //선택한 메뉴 색처리 및 메뉴 출력해주는 함수
            for (int i = 0; i < menuList.Length; i++)
            {
                if (i == selectedMenu)
                {
                    menuUi.PrintColorSentence(cursorPositionX, cursorPositionY, menuList[i]); //색 처리
                }

                else
                {
                    menuUi.PrintNotColorSentence(cursorPositionX, cursorPositionY, menuList[i]); //색 미처리
                }

                cursorPositionY++;

            }

        }
    }
}
