using System;
using System.Text.RegularExpressions;

public class RegularExpression
{
    //싱글톤 디자인 패턴
    private static RegularExpression instance;
    private RegularExpression() { }
    public static RegularExpression GetInstance()
    {
        if(instance == null)
        {
            instance = new RegularExpression();
        }
        return instance;
    }

    public bool JudgeWithRegularExpression(int cursorPositionX, int cursorPositionY, string inputString, string pattern, string errorMessage)
    {
        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        bool isMatch = Regex.IsMatch(inputString, pattern);

        if (isMatch == true)
        {
            return true;
        }

        Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(errorMessage);
        Console.ResetColor();
        Console.ReadKey(true);
        return false;
    }
}
