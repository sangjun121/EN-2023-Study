using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _4차_LectureTimeTable.DataBase;
using _4차_LectureTimeTable.Model;
using _4차_LectureTimeTable.ExceptionHandling;
using _4차_LectureTimeTable.Utility;
using System.Security.Policy;
using _4차_LectureTimeTable.View;
using System.Globalization;

namespace _4차_LectureTimeTable.Controller
{
    public class CourseFinder //강의 검색하는 클래스
    {
        protected DataStorage dataStorage;
        protected LectureException lectureException;
        protected MenuUi menuUi;
        protected MenuSelectController menuSelectController;
        public CourseFinder(DataStorage dataStorage, LectureException lectureException, MenuUi menuUi, MenuSelectController menuSelectController) 
        { 
            this.dataStorage = dataStorage;
            this.lectureException = lectureException;
            this.menuUi = menuUi;
            this.menuSelectController = menuSelectController;
        }

        //필드값으로 빼버리면 : 생성자로 뺄때 이거 초기화 안됨
        public string major="";
        public string courseClassification="";
        public string lectureName=""; //과목명
        public string professor="";   //교수명
        public string grade = "";       //학년
        public string courseCode = "";  //학수번호
        public string courseClass = ""; //분반

        
        public string[] majorPrintList = { "전체", "컴퓨터공학과", "소프트웨어학과", "지능기전공학부", "기계항공우주공학부" };
        public string[] courseClassificationPrintList = { "전체", "공통교양필수", "전공필수", "전공선택" };
        public string[] LectureEntries = { "개설학과 전공", "이수구분", "교과목명     :", "교수명       :", "학년         :", "학수번호     :", "분반         :", "<검색하기>" }; 

        public int lectureEntriesNumber; //메뉴 ENTER로 선택 했을때 해당 인덱스 저장하는 변수 // 지역변수로 내리고 인자로 넘기기
        public int majorNumber;
        public int courseClassificationNumber;

        private bool isInputValid = false;
        private bool isSearchCompleted = false;

        public void FindCourse()  //강의 찾기 내 메인 함수
        {
            Console.Clear();
            
            menuUi.PrintSearchLectureGuideUi();
            menuUi.PrintCourseFinderMenu();

            while (!isSearchCompleted)
            {
                lectureEntriesNumber = menuSelectController.SelectMenuWithUpAndDown(LectureEntries, 8, 5, 11); //상하키로 메뉴 선택하는 함수 호출
                SelectLectureEntriesMenu(); //선택한 메뉴 처리 하는 함수
            }
            Console.Clear();
            CompareWithData(); //검색한 값과 데이터 값 서로 비교하기

            Console.ReadKey(true); //아무키 눌러서 이전 메뉴로 돌아가기
        }
        
        public void SelectLectureEntriesMenu() //선택한 메뉴 처리하는 함수
        {
            isInputValid = false;
            isSearchCompleted = false;
            switch (lectureEntriesNumber)
            {
                case (int)LectureEntriesList.MAJOR_LECTURE: //전공선택
                    
                    majorNumber = menuSelectController.SelectMenuWithRightAndLeft(majorPrintList, 5, 20, 11); //좌우키로 메뉴선택하는 함수 호출
                    if (majorNumber == 0) major = "";
                    else major = majorPrintList[majorNumber];
                    break;

                case (int)LectureEntriesList.CLASSIFICATION: //이수구분 선택하기
                    courseClassificationNumber = menuSelectController.SelectMenuWithRightAndLeft(courseClassificationPrintList, 4, 20, 12); //좌우키로 메뉴 선택하는 함수 호출
                    if (courseClassificationNumber == 0) courseClassification = "";
                    else courseClassification = courseClassificationPrintList[courseClassificationNumber];
                    break;

                case (int)LectureEntriesList.LECTURE_NAME: //강의 이름 
                    while (!isInputValid) //case문안에 while 문 > 메소드로 빼기 // 공통으로 사용하는 클래스에
                    {
                        lectureName = ToReceiveInput.ReceiveInput(20, 13, 30, Constants.IS_NOT_PASSWORD);
                        isInputValid = lectureException.JudgeLectureNameRegularExpression(20, 13, lectureName);
                    }
                    break;

                case (int)LectureEntriesList.PROFESSOR_NAME: //교수명
                    while (!isInputValid)
                    {
                        professor = ToReceiveInput.ReceiveInput(20, 14, 25, Constants.IS_NOT_PASSWORD);
                        isInputValid = lectureException.JudgeProcessorRegularExpression(20, 14, professor);
                    }
                    break;

                case (int)LectureEntriesList.GRADE: //학점
                    while (!isInputValid)
                    {
                        grade = ToReceiveInput.ReceiveInput(20, 15, 1, Constants.IS_NOT_PASSWORD);
                        isInputValid = lectureException.JudgeGradeRegularExpression(20, 15, grade);
                    }
                    break;

                case (int)LectureEntriesList.COURSE_NUMBER: //학수번호
                    while (!isInputValid)
                    {
                        courseCode = ToReceiveInput.ReceiveInput(20, 16, 6, Constants.IS_NOT_PASSWORD);
                        isInputValid = lectureException.JudgeCourseCodeRegularExpression(20, 16, courseCode);
                    }
                    break;
                case (int)LectureEntriesList.CLASS_NUMBER: //분반
                    while (!isInputValid)
                    {
                        courseClass = ToReceiveInput.ReceiveInput(20, 17, 3, Constants.IS_NOT_PASSWORD);
                        isInputValid = lectureException.JudgeCourseClassRegularExpression(20, 17, courseClass);
                    }
                    break;
                case (int)LectureEntriesList.SEARCH: //ENTER 눌렀을 경우
                    isSearchCompleted = true; //반복문 탈출을 위한 bool형 변수 처리
                    break;
            }
        } 

        public void CompareWithData() //데이터와 입력 받은값 서로 비교하기
        {
            bool isExistCourseInData = false;
            dataStorage.searchedLectureData.Clear(); //이전 검색에 대한 기록 삭제

            for (int i = 1; i <= dataStorage.lectureTotalData.GetLength(0); i++)
            {
                if (
                    (dataStorage.lectureTotalData.GetValue(i, 2).ToString()).Contains(major) &&
                    (dataStorage.lectureTotalData.GetValue(i, 6).ToString()).Contains(courseClassification) &&
                    (dataStorage.lectureTotalData.GetValue(i, 5).ToString()).Contains(lectureName) &&
                    (dataStorage.lectureTotalData.GetValue(i, 11).ToString()).Contains(professor) &&
                    (dataStorage.lectureTotalData.GetValue(i, 7).ToString()).Contains(grade) &&
                    (dataStorage.lectureTotalData.GetValue(i, 3).ToString()).Contains(courseCode) &&
                    (dataStorage.lectureTotalData.GetValue(i, 4).ToString()).Contains(courseClass))
                {
                    isExistCourseInData = true;
                    dataStorage.searchedLectureData.Add(i); //검색값과 교집합으로 일치하는 강의의 아이디를 배열에 저장 > 관심과목 담기와 수강신청에서 출력된 강의만 담게 하기 위한 리스트
                }

                if (isExistCourseInData) // 일치하면 출력
                {
                    SetAndArrangeData(dataStorage.lectureTotalData, i);//문자열 수 확인하는 함수

                    isExistCourseInData = false;
                }
            }
        }

        //문자열 바이트 수 확인하고 출력 정렬 맞춰주는 함수
        public void SetAndArrangeData(Array lectureTotalData, int index)
        {
            string[] maximumLengthOfStringsInEachRow = { "184", "기계항공우주공학부", "004714","001", "Capstone디자인(산학협력프로젝트)", "공통교양필수", "1", "1", "수 16:30~18:30, 금 09:00~11:00", "센B201,센B209", "Abolghasem Sadeghi-Niaraki", "영어/한국어" };
            for (int i = 1; i <= 12; i++)
            {
                //예외처리. 엑셀 값이 공백 일 때
                if (lectureTotalData.GetValue(index, i) == null) lectureTotalData.SetValue("", index, i);
                //문자열과 출력해야 하는 공백 칸수 함수에 인자로 전달 
                menuUi.PrintExistLectureInformation(lectureTotalData.GetValue(index, i).ToString(), lectureTotalData.GetValue(index, i).ToString().Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[i - 1]) - Encoding.Default.GetByteCount(lectureTotalData.GetValue(index, i).ToString()));
            }
            Console.WriteLine("");//줄 띄우기

        }
        //함수 패키징 기능별로 하자
    }



}
