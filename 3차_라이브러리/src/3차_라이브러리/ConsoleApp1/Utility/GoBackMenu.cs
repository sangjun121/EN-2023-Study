using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utility
{
    public class GoBackMenu // 뒤로가기 구현
    {
        //싱글톤
        private static GoBackMenu instance;
        private GoBackMenu() { }
        public static GoBackMenu GetInstance()
        {
            if (instance == null)
            {
                instance = new GoBackMenu();
            }
            return instance;
        }

        private void CancelKeyEvent(object sender, ConsoleCancelEventArgs e) //ctrl 조합키 막기 위한 함수
        {
            e.Cancel = true;
        }

        public bool GoBackToBeforeFunction() // ESC 눌렀을때
        {
            Console.CancelKeyPress += CancelKeyEvent; //ctrl 조합키 막기
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                return Constants.ESC_END_FUNCTION;
            }
            return Constants.ENTER_AGAIN_FUNCTION;
        }

        public void ensureUiVisibility() // UI ConsoleClear로 묻히지 않도록 막는 함수
        {
            Console.CancelKeyPress += CancelKeyEvent; //ctrl 조합키 막기
            Console.ReadKey(true);
        }

        
    }
}
