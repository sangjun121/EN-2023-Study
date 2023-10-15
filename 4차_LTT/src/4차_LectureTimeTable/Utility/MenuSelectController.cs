using _4차_LectureTimeTable.Utility;
using _4차_LectureTimeTable.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.Controller
{
    public class MenuSelectController
    {
        MenuUi menuUi;

        public MenuSelectController(MenuUi menuUi)
        {
            this.menuUi = menuUi;
        }

        public int SelectMenuWithUpAndDown(string[] menuList, int menuNumber, int cursorPositionX, int cursorPositionY) 
            //위아래 키 입력 받고 처리하는 함수
        {
            ConsoleKeyInfo inputKey;
            bool isEnter = false;
            int selectedMenu = 0;

            Console.CursorVisible = false;
            SetAndPrintColorMenuSentence(menuList, selectedMenu, cursorPositionX, cursorPositionY,Constants.IS_PRINT_NEXT_LINE); //색깔 적용 후 메뉴 리스트 출력해 주는 함수

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

                SetAndPrintColorMenuSentence(menuList, selectedMenu, cursorPositionX, cursorPositionY, Constants.IS_PRINT_NEXT_LINE);
            }

            return selectedMenu;
        }

        public int SelectMenuWithRightAndLeft(string[] menuList, int menuNumber, int cursorPositionX, int cursorPositionY) 
            //좌우 키 입력 받고 처리하는 함수
        {
            ConsoleKeyInfo inputKey;
            bool isEnter = false;
            int selectedMenu = 0;

            Console.CursorVisible = false;
            SetAndPrintColorMenuSentence(menuList, selectedMenu, cursorPositionX, cursorPositionY, Constants.IS_PRINT_NEXT_SIDE);
            

            while (!isEnter)
            {
                inputKey = Console.ReadKey();
                if ((inputKey.Key == ConsoleKey.LeftArrow) && (selectedMenu > 0))
                {
                    selectedMenu--;
                }
                else if ((inputKey.Key == ConsoleKey.RightArrow) && (selectedMenu < (menuNumber - 1)))
                {
                    selectedMenu++;
                }
                else if ((inputKey.Key == ConsoleKey.Enter))
                {
                    isEnter = true;
                }

                SetAndPrintColorMenuSentence(menuList, selectedMenu, cursorPositionX, cursorPositionY, Constants.IS_PRINT_NEXT_SIDE);
            }

            return selectedMenu;
        }

        public void SetAndPrintColorMenuSentence(string[] menuList,int selectedMenu, int cursorPositionX, int cursorPositionY, bool isDownOrRight) //해당 메뉴 위치 보여주는 함수
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

                if (isDownOrRight) //상하키 함수에서 호출 할 경우
                {
                    cursorPositionY++;
                }
                else
                {
                    cursorPositionX += ((menuList[i].Length) * 2 + 5);  //좌우 키 함수에서 호출 할 경우
                    //한글은 콘솔창 2칸씩 차지 하므로 겹치지 않도록 2배 처리
                }
            }

        }
    }
}