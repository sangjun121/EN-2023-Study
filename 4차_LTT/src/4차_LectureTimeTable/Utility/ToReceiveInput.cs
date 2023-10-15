using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.Utility
{
    public class ToReceiveInput //이름 변경 // 싱글턴
    {
        public ToReceiveInput() { } //모든 입력을 입력받는 함수(예외처리)
        public static string ReceiveInput(int cursorPositionX, int cursorPositionY , int maxLength, bool isPassword)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.Write("                                       ");
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            while (true)
            {
                Console.TreatControlCAsInput = true; //control C로 강제종료 막기
                ConsoleKeyInfo inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter) //입력 완료
                {
                    break;
                }
                else if (inputKey.Key == ConsoleKey.Escape) // ESC 키 눌렀을 때 두가지 경우 
                {
                    if (stringBuilder.Length == 0) //뒤로가기
                    {
                        stringBuilder.Append("ESC");
                        return stringBuilder.ToString();
                    }

                    else if (stringBuilder.Length > 0) //다시 입력하기
                    {
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                        for (int i = 0; i < stringBuilder.Length; i++)
                        {
                            Console.Write("\n \n");
                        }
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                        stringBuilder.Remove(0, stringBuilder.Length);
                    }
                }
                else if (inputKey.Key == ConsoleKey.Backspace && stringBuilder.Length > 0) //백스페이스 눌렀을때 처리
                {
                    Console.Write("\b \b");
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                }
                else if ((inputKey.Modifiers == ConsoleModifiers.Control && inputKey.Key == ConsoleKey.C)
                    || (inputKey.Modifiers == ConsoleModifiers.Control && inputKey.Key == ConsoleKey.V)
                    || (inputKey.Modifiers == ConsoleModifiers.Control && inputKey.Key == ConsoleKey.Z)) //Control 조합키 막기
                {
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.Write("                                       ");
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("잘못된 입력입니다. 다시 입력하세요!");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                    Console.Write("                                       ");
                    Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                }
                else if ( (Char.IsLetterOrDigit(inputKey.KeyChar)
                    || Char.IsWhiteSpace(inputKey.KeyChar)
                    || Char.IsPunctuation(inputKey.KeyChar)
                    || Char.IsSymbol(inputKey.KeyChar)) && stringBuilder.Length < maxLength)    //문자,숫자,기호,구두점 입력 받았을 시
                { 
                    stringBuilder.Append(inputKey.KeyChar);
                    if(isPassword)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(inputKey.KeyChar);
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
