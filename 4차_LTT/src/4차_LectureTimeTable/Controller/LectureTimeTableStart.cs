using _4차_LectureTimeTable.DataBase;
using _4차_LectureTimeTable.ExceptionHandling;
using _4차_LectureTimeTable.View;
using _4차_LectureTimeTable.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;

namespace _4차_LectureTimeTable.Controller
{
    public class LectureTimeTableStart
    {

        public MainUi mainUi;
        public UserException userException;
        public DataStorage dataStorage;
        LectureTimeTableMenu lectureTimeTableMenu;
        public LectureTimeTableStart()
        {
            this.mainUi = new MainUi();
            this.userException = new UserException();
            this.dataStorage = new DataStorage();
            this.lectureTimeTableMenu = new LectureTimeTableMenu(dataStorage);
        }
        public string id;
        public string password;
        public bool isInputValid;
        public bool isAccountExist;

        public void GetLogin()
        {
            GetExcelFile();
            while (true)
            {
                Console.SetWindowSize(100, 30);
                mainUi.PrintMainUi();
                mainUi.PrintLoginUi();//UI 출력

                //아이디 비밀번호 입력 받기
                InputUserIdAndPassword();

                //데이터 베이스와 비교하기
                isAccountExist = false;
                for (int i = 0; i < dataStorage.userData.Count; i++)
                {
                    if (string.Equals(id, dataStorage.userData[i].UserId)) //아이디 및 비밀번호 검사 // 아이디 비밀번호 따로 검사
                    {
                        isAccountExist = true;
                        if (string.Equals(password, dataStorage.userData[i].UserPassword))
                        {
                            Console.Clear();
                            lectureTimeTableMenu.ControllLectureTimeTableMenu(dataStorage.userData[i]); //메뉴로 이동
                            break;
                        }
                        
                        else
                        {
                            Console.SetCursorPosition(30, 22);
                            Console.WriteLine("비밀번호 입력이 틀렸습니다. 다시 입력하세요");
                        }
                    }
                }

                if (isAccountExist == false)
                {
                    Console.SetCursorPosition(35, 22);
                    Console.WriteLine("아이디 또는 비밀 번호가 틀렸습니다.");
                }

                Console.ReadKey(true);
                Console.Clear();

                //시스템 종료기능 추가하기
            }
        }

        public void InputUserIdAndPassword() // 사용자로 부터 아이디 및 비밀번호 입력 받기 
        {
            isInputValid = false;
            while (!isInputValid)
            {
                id = ToReceiveInput.ReceiveInput(38, 18 ,8, Constants.IS_NOT_PASSWORD);
                isInputValid = userException.JudgeIdWithRegularExpression(38, 18, id);
            }

            isInputValid = false;
            while (!isInputValid)
            {
                password = ToReceiveInput.ReceiveInput(38, 19 ,35, Constants.IS_PASSWORD);
                isInputValid = userException.JudgePasswordWithRegularExpression(38, 19, password);
            }
        }

        //엑셀 불러오기 //따로 패키지 하기
        public void GetExcelFile() // EXCEL 클래스 만들어서 밖으로 빼기 (이것보다 밖으로 빼면 (메인클래스로 빼면) 오류 발생)
        {
            try
            {
                // Excel Application 객체 생성
                Excel.Application application = new Excel.Application();

                // Workbook 객체 생성 및 파일 오픈 (바탕화면에 있는 excelStudy.xlsx 가져옴)
                Excel.Workbook workbook = application.Workbooks.Open(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\2023년도 1학기 강의시간표.xlsx");

                // sheets에 읽어온 엑셀값을 넣기 (한 workbook 내의 모든 sheet 가져옴)
                Excel.Sheets sheets = workbook.Sheets;

                // 특정 sheet의 값 가져오기
                Excel.Worksheet worksheet = sheets["Sheet1"] as Excel.Worksheet;

                // 범위 설정 (좌측 상단, 우측 하단)
                Excel.Range cellRange = worksheet.get_Range("A2", "L185") as Excel.Range;

                // 설정한 범위만큼 데이터 담기 (Value2 -셀의 기본 값 제공)
                Array lectureData = cellRange.Cells.Value2;

                dataStorage.lectureTotalData = lectureData;

                // 모든 워크북 닫기
                application.Workbooks.Close();

                // application 종료
                application.Quit();
            }
            catch (SystemException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
