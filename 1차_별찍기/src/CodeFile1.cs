using System;

namespace Starpoint // 별찍기 네임스페이스 생성
{
    class ChooseMenu //메뉴 선택 관련 클래스 생성
    {

        public static void InputMenuNumber() //메뉴판 출력 및 번호 입력받는 메소드 생성 //UI, 함수, 예외처리 다 따로따로 함수로 만들기
        {
            Console.WriteLine("+------------------<MENU>------------------+");
            Console.WriteLine("|              1. 피라미드                 |");
            Console.WriteLine("|              2.역피라미드                |");
            Console.WriteLine("|              3. 모래시계                 |");
            Console.WriteLine("|              4. 마름모                   |");
            Console.WriteLine("+------------------------------------------+");
            Console.Write("[Menu] 번호를 입력하세요: "); // 메뉴판 출력
            
            string number = Console.ReadLine(); //메뉴판 번호 입력받기
            Console.Clear(); // 콘솔창 다음 화면으로 
            if ((int.Parse(number) != 1) && (int.Parse(number) != 2) && (int.Parse(number) != 3) && (int.Parse(number) != 4)) //메뉴 번호 제외 하고 다른 입력값이 들어온 경우 (예외처리) //논리연산자
            {
                WrongInput.WrongMemuNumber(); // WrongInput에서 메뉴 번호 잘못입력받은 경우 처리하는 메소드 WrongMenuNumber 호출
                return; // 잘못된 정보를 입력했을때 밑의 메소드 호출을 부르면 루프가 형성되기 때문에 return 처리
            }
            SelectShape.TypeOfShape(int.Parse(number)); // 입력 받은 메뉴 번호 정수형으로 변환한 후, 출력할 형태를 고르는 클래스 SelectShape의 TypeOfShape메소드에 파라멘트 전달하기 

        }

    }

    class Enternumber // 줄 개수와 관련된 클래스
    {
        public static int InputLineNumber()  //줄 개수를 입력받고 정수형으로 변환 후 반환해 주는 메소드
        {
            Console.Write("줄 갯수를 입력하세요: ");
            string number = Console.ReadLine(); //string 변수로 입력 받기
            int convertednumber = int.Parse(number); // 정수형으로 형 변환
            Console.Clear();
            return convertednumber; //줄 개수 반환


        }
    }

    class WrongInput // 잘못입력된 정보를 처리하는 클래스(예외처리)
    {
        public static void WrongMemuNumber() //메뉴 번호를 잘못 입력한 경우
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
            Program.Main(); //처음 메뉴 번호를 입력받는 순서로 돌아가기
        }
        public static void WrongEndOrAgainNumber() //줄 개수를 잘못 입력한 경우
        {
            Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
            EndProgram.EndOrAgain(); // 끝낼지 다시 할지 정하는 클래스의 메소드로 돌아가기
        }

    }

    class SelectShape //별찍기의 모양을 정해주는 클래스
    {

        public static void TypeOfShape(int number) //메뉴판에서 입력받은 정수를 인수로 하고 모양의 타입을 확인하고 해당 메소드를 호출하는 메소드 //메소드 겹치지 않게
        {
            if (number == 1) //메뉴판 1번
            {
                StarPrint.Pyramid(); //별을 출력하는 StarPrint 클래스의 피라미드 모양을 출력하는 메소드 호출
            }

            else if (number == 2)//메뉴판 2번
            {
                StarPrint.InvertedPyramid(); //별을 출력하는 StarPrint 클래스의 역피라미드 모양을 출력하는 메소드 호출
            }

            else if (number == 3)//메뉴판 3번
            {
                StarPrint.Hourglass(); //별을 출력하는 StarPrint 클래스의 모래시계 모양을 출력하는 메소드 호출
            }
            else if (number == 4)//메뉴판 4번
            {
                StarPrint.Rhombus(); //별을 출력하는 StarPrint 클래스의 마름모 모양을 출력하는 메소드 호출
            }
            EndProgram.EndOrAgain(); //실행이 끝난 후 프로그램 재실행을 판단하는 클래스 EndProgram의 메소드 EndOrAgain 호출
        }
    }
    class EndProgram // 프로그램을 재실행 할지 아니면 종료할지 판단하는 클래스
    {
        public static void EndOrAgain() // 종료여부 입력받고 판단하는 메소드
        {
            int selectendoragain; // 재실행 여부 (1:재실행, 2:종료) 입력받고 저장할 변수

            Console.WriteLine();
            Console.WriteLine("-----------------------"); //UI출력
            Console.WriteLine("     메뉴로 돌아가기   ");
            Console.WriteLine("      1: 다시하기      ");
            Console.WriteLine("      2: 종료하기      ");
            Console.WriteLine("-----------------------");
            selectendoragain = int.Parse(Console.ReadLine()); //string 변수로 입력받으므로 정수형으로 변환 후 저장
            
            Console.Clear();
            
            if (selectendoragain == 1) //다시하기를 골랐을 경우
            { 
                Program.Main(); // 다시 Main 함수로 돌아가서 재실행하기
            }
            else if (selectendoragain == 2) // 종료하기를 골랐을 경우
            {
                return; //프로그램 종료
            }
            else //이외의 값을 받았을 경우 예외처리하기
            {
                WrongInput.WrongEndOrAgainNumber(); //예외처리관련 클래스 호출 후 재실행 여부 입력 잘못 받았을 경우를 처리하는 WrongEndOrAgainNumber 메소드 호출
                return; //예외값 처리하고 루프를 막기 위해 반환
            }

        }
    }

    class StarPrint //별을 모양별로 출력해주는 클래스
    {
        public static void Pyramid() //피라미드 모양 출력 메소드
        {
            int i, j;
            int linenumber = Enternumber.InputLineNumber(); //줄 개수 입력받기

            for (i = 0; i < linenumber; i++) 
            {
                for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++) //앞쪽 공백 출력 
                {
                    Console.Write(" ");
                }
                for (j = 0; j < (2 * i + 1); j++) //중간 별 출력
                {
                    Console.Write("*");
                }
                for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++) //뒷쪽 공백 출력
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public static void InvertedPyramid() //역피라미드 출력 메소드
        {
            int i, j;
            int linenumber = Enternumber.InputLineNumber(); // 줄 개수 입력받기

            for (i = 0; i < linenumber; i++)
            {
                for (j = 0; j < i; j++) //앞쪽 공백 출력
                {
                    Console.Write(" ");
                }
                for (j = 0; j < ((2 * linenumber - 1) - 2 * i); j++) //중간 별 출력
                {
                    Console.Write("*");
                }
                for (j = 0; j < i; j++) //뒷쪽 공백 출력
                {
                    Console.Write(" ");
                }
                Console.WriteLine(); // 한줄 띄기위해 Console.WriteLine 호출
            }
        }

        public static void Hourglass() //모래시계 모양 출력해주는 메소드
        {
            int i, j;
            int linenumber = Enternumber.InputLineNumber();

            if (linenumber % 2 == 0) //줄개수가 짝수인 경우
            {
                linenumber /= 2; //별 찍기의 상단부와 하단부를 나누기위해 반으로 나누기
                for (i = 0; i < linenumber; i++)  //역피라미드 찍기
                {
                    for (j = 0; j < i; j++) 
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - 2 * i); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                for (i = 0; i < linenumber; i++) //피라미드 찍기
                {
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < (2 * i + 1); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }

            else//줄 개수가 홀수인 경우
            {
                linenumber = linenumber / 2 + 1; 
                for (i = 0; i < linenumber; i++) //역피라미드 찍기
                {
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - 2 * i); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                for (i = 1; i < linenumber; i++) //피라미드 찍기
                {
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < (2 * i + 1); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }

        }

        public static void Rhombus() //마름모 출력 메소드
        {
            int i, j;
            int linenumber = Enternumber.InputLineNumber();

            if (linenumber % 2 == 0) //줄 개수가 짝수인 경우 //if 문안에 간략화 >함수
            {
                linenumber /= 2; //상단부와 하단부를 나누기 위해 절반으로 나누기

                for (i = 0; i < linenumber; i++) //피라미드 찍기
                {
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < (2 * i + 1); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                for (i = 0; i < linenumber; i++) //역피라미드 찍기
                {
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - 2 * i); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }

            else //홀수인 경우
            {
                linenumber = linenumber / 2 + 1;

                for (i = 0; i < linenumber; i++) //피라미드 찍기
                {
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < (2 * i + 1); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - (2 * i + 1)) / 2; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }

                for (i = 1; i < linenumber; i++) //역피라미드 찍기
                {
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < ((2 * linenumber - 1) - 2 * i); j++)
                    {
                        Console.Write("*");
                    }
                    for (j = 0; j < i; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }

        }

    }

    class Program //의미있는 걸로 네이밍
    {
        public static void Main() // 메인 함수 생성
        {
            ChooseMenu.InputMenuNumber(); // 메뉴판 번호 입력받기 -> 클래스 속 메소드 호출
        }
    }

}
//static 말고, 객체 지향적으로 코딩
//스위치 문
//접근 제한자 
//UI 신경쓰기
//