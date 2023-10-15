using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using _4차_LectureTimeTable.View;

namespace _4차_LectureTimeTable.ExceptionHandling
{
    public class UserException
    {
        public bool JudgeIdWithRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[0-9]{8}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.CursorVisible = false;
            Console.WriteLine("학번 8자리를 입력해 주세요!");
            Console.ResetColor();
            Console.ReadKey(true);
            Console.CursorVisible = true;

            return false;
        }

        public bool JudgePasswordWithRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[a-zA-Z0-9]{8,15}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("숫자 + 영어 8~15글자를 입력해 주세요!");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            }
        }
    }
}
