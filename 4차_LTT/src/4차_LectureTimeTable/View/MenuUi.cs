using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.View
{
    public class MenuUi
    {
        public void PrintMenuUi(string userName)
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("                             ==============================================");
            Console.WriteLine("                                             학사 정보 시스템              ");
            Console.WriteLine("                             ==============================================");
            Console.Write("                             회원 :");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(userName);
            Console.ResetColor();
            Console.Write(" 님");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                 ESC : 로그아웃");
            Console.ResetColor();
            Console.WriteLine("                             ==============================================");
            Console.WriteLine("                             □                  메뉴선택                □");
            Console.WriteLine("                             ==============================================");
        }

        public void PrintColorSentence(int cursorPositionX, int cursorPositionY, string menuIndexLine) //초록색 줄 처리 함수
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(menuIndexLine);
            Console.ResetColor();
        }

        public void PrintNotColorSentence(int cursorPositionX, int cursorPositionY, string menuIndexLine) //기존 리셋값 흰색 줄 처리 함수
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.Write(menuIndexLine);
        }

        public void PrintCourseFinderMenu()
        {
            Console.WriteLine("==========================================================================================");
            Console.WriteLine("                                 23학년도 1학기 강의 검색         ");
            Console.WriteLine("==========================================================================================");

        }

        public void PrintSearchLectureGuideUi()
        {
            Console.WriteLine("┏────────────────────────────────────강의 검색 가이드────────────────────────────────────┓");
            Console.WriteLine("┃                                                                                        ┃");
            Console.WriteLine("┃  ◎  방향키를 이용하여 옵션을 이동 후 ENTER를 누르면 선택 또는 입력 가능 합니다.       ┃");
            Console.WriteLine("┃  ◎  검색 도중 ESC를 누르면 다시 입력 할 수 있습니다.                                  ┃");
            Console.WriteLine("┃  ◎  모든 입력을 완료하면 <검색하기>를 눌러 주세요.                                    ┃");
            Console.WriteLine("┃                                                                                        ┃");
            Console.WriteLine("┗────────────────────────────────────────────────────────────────────────────────────────┛");
        }
        public void PrintLectureDetailInformationInputMenu(int cursorPositionX, int cursorPositionY)
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.WriteLine(" > 교과목명 : ");
            cursorPositionY++;
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.WriteLine(" > 교수명   : ");
            cursorPositionY++;
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.WriteLine(" > 학년     : ");
            cursorPositionY++;
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.WriteLine(" > 학수번호 : ");
            cursorPositionY++;
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
            Console.WriteLine(" > 분반     : ");
        }

        public void PrintExistLectureInformation(string lectureData, int emptyCount)
        {

            lectureData = lectureData.PadRight(emptyCount);
            Console.Write(lectureData);
            Console.Write("  ");

        }

        public void PrintStatusOfInterestedLecture(int availableCreditsForRegistration, int earnedCredits)
        {
            Console.WriteLine("==============================================================================================================");
            Console.WriteLine("등록가능 학점 : {0}    담은학점 : {1}     담을과목 (NO) : ", availableCreditsForRegistration, earnedCredits);
            Console.WriteLine("==============================================================================================================");
        }

        public void PrintAlreadyContainErrorMesseage() //예외 1) 이미 관심과목 리스트에 있을 경우
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("이미 관심과목에 담겨진 항목입니다. 다시 선택하세요. ");
            Console.ResetColor();
        }
        public void PrintAlreadyRegistratedErrorMesseage() //이미 수강신청 했을 경우
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("이미 신청된 강의 입니다. 다시 신청하세요. ");
            Console.ResetColor();
        }
        public void PrintExcessCreditsErrorMesseage() //예외 2) 학점수가 초과되었을 경우
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("등록 가능한 학점 수를 초과 하였습니다.");
            Console.ResetColor();
        }
        public void PrintLectureIdIsNotInTheSearchedList() //예외 3) 검색된 리스트에 강의가 없을 경우
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("검색된 리스트에 강의가 없습니다.");
            Console.ResetColor();
        }
        public void PrintInterestLectureStoredSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("관심과목에 저장 되었습니다!.");
            Console.ResetColor();
        }

        public void PrintDeleteLectureSuccess()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("삭제가 완료 되었습니다.");
            Console.ResetColor();
        }
        public void PrintDeleteInterestLectureFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("관심과목 리스트에 해당 아이디가 없습니다! 다시 입력하세요");
            Console.ResetColor();
        }
        public void PrintDeleteRegistratedLectureFail()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("수강신청 리스트에 해당 아이디가 없습니다! 다시 입력하세요");
            Console.ResetColor();
        }

        public void PrintDeletionLecture(int availableCreditsForRegistration, int earnedCredits)
        {
            Console.WriteLine("==============================================================================================================");
            Console.WriteLine("등록가능 학점 : {0}    담은학점 : {1}     삭제할 과목 (NO) : ", availableCreditsForRegistration, earnedCredits);
            Console.WriteLine("==============================================================================================================");
        }
        public void PrintLectureRegistration(int availableCreditsForRegistration, int earnedCredits)
        {
            Console.WriteLine("==============================================================================================================");
            Console.WriteLine("등록가능 학점 : {0}    담은학점 : {1}     신청과목 (NO) : ", availableCreditsForRegistration, earnedCredits);
            Console.WriteLine("==============================================================================================================");
        }
        public void PrintLectureIsTimeOverLaped() //이미 신청된 과목과 시간이 겹치는 경우
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("등록된 강의와 시간이 겹칩니다.");
            Console.ResetColor();
        }
        public void PrintLectureIsSuccess() //수강신청이 성공한 경우
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("수강신청이 완료 되었습니다.");
            Console.ResetColor();
        }
        public void PrintTimeTableMain()
        {
            Console.WriteLine("==========================================================================================================================================================================================");
            Console.WriteLine("======================================================================================= 2023 시간표 ======================================================================================");
        }
    }
}
