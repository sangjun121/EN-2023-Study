using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace ConsoleApp1.Utility
{
    public class InputProcess
    {
        public InputByReadKey inputByReadKey;
        public RegularExpression regularExpression;

        //싱글턴 디자인 패턴
        private static InputProcess instance;
        private InputProcess() 
        {
            this.inputByReadKey = InputByReadKey.GetInstance();
            this.regularExpression = RegularExpression.GetInstance();
        }
        public static InputProcess GetInstance()
        {
            if (instance == null)
            {
                instance = new InputProcess();
            }
            return instance;
        }


        public string InputProcessFunction(int consolePositionX, int consolePositionY, int limitLength, bool judgmentMasking, string regularExpressionString, string errorMessage)
        {
            bool isJudgingCorrectString; //입력값 검사 후 탈출용 진리형 변수
            string inputTargetVariable = string.Empty;

            isJudgingCorrectString = false;
            Console.SetCursorPosition(consolePositionX, consolePositionY);
            while (!isJudgingCorrectString) 
            {
                inputTargetVariable = inputByReadKey.ReceiveInput(consolePositionX, consolePositionY, limitLength, judgmentMasking); //입력값 키값으로 검사
                isJudgingCorrectString = regularExpression.JudgeWithRegularExpression(consolePositionX, consolePositionY, inputTargetVariable, regularExpressionString, errorMessage); //정규표현식 이용하여 검사
            }  //정규표현식 확인후 거짓일 때만 재실행 

            return inputTargetVariable;
        }
    
    }
}
