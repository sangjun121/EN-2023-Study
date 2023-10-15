using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.ExceptionHandling
{
    public class LectureException
    {
        public bool JudgeLectureNameRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[A-Za-z가-힣0-9+#:\-\(\)]{1,15}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("한영문자 또는 숫자 또는 ?!+= 1개 이상 15개 이하로 작성 하세요!");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            }
        }

        public bool JudgeProcessorRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[A-Za-z가-힣-]{1,30}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("한글이름 또는 영어 이름( - : 포함가능)을 입력해 주세요");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            }
        }
        public bool JudgeGradeRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[1-4]$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("한글이름 또는 영어 이름( - : 포함가능)을 입력해 주세요");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            }
        }
        public bool JudgeCourseCodeRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[0-9]{6}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("학수번호 6자리를 정확하게 입력해 주세요!");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            }
        }

        public bool JudgeCourseClassRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^0[0-9]{2}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0xx 형태의 분반을 입력해 주세요");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            }
        }

        public bool JudgeCourseNumberRegularExpression(int cursorPositionX, int cursorPositionY, string inputString)
        {

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            string pattern = @"^[1-9][0-9]{0,2}$";
            bool isMatch = Regex.IsMatch(inputString, pattern);

            if (isMatch == true)
            {
                return true;
            }
            
                Console.SetCursorPosition(cursorPositionX, cursorPositionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1 부터 999 사이의 숫자를 입력해 주세요");
                Console.ResetColor();
                Console.ReadKey(true);
                return false;
            
        }

        public bool JudgeTimeTypeRegularExpression(string inputString, string regularExpression) 
        {
            string pattern = regularExpression;
            bool isMatch = Regex.IsMatch(inputString, pattern);
            if (isMatch == true) 
            {
                return true;

            }
            return false;
        }
    }
}
