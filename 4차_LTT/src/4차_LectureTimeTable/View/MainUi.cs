using _4차_LectureTimeTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.View
{
    public class MainUi
    {
        public void PrintMainUi()
        {

            Console.WriteLine("\n\n\n");
            Console.WriteLine("                             ==============================================");
            Console.WriteLine("                               ##     ##    ##     ##   ##  #           #  ");
            Console.WriteLine("                               ##     ##    ##     ##       ##         ##  ");
            Console.WriteLine("                               ##     ##    ####   ##   ##   ##       ##   ");
            Console.WriteLine("                               ##     ##    ## ##  ##   ##    ##     ##    ");
            Console.WriteLine("                               ##     ##    ##  ## ##   ##     ##   ##     ");
            Console.WriteLine("                               ##     ##    ##   ####   ##      ## ##      ");
            Console.WriteLine("                                #######     ##     ##   ##       ###       ");
            Console.WriteLine("                             ==============================================");
            Console.WriteLine("                             □       세종대학교 수강신청 프로그램       □");
            Console.WriteLine("                             ==============================================");

        }

        public void PrintLoginUi()
        {
            Console.WriteLine("                         ┌───────────────────────────────────────────────────┐");
            Console.WriteLine("                         │                      로그인                       │");
            Console.WriteLine("                         │                                                   │");
            Console.WriteLine("                         │ 아이디   :                                        │");
            Console.WriteLine("                         │ 패스워드 :                                        │");
            Console.WriteLine("                         │                                                   │");
            Console.WriteLine("                         └───────────────────────────────────────────────────┘");
        }

    }
}
