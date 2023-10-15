using _4차_LectureTimeTable.DataBase;
using _4차_LectureTimeTable.ExceptionHandling;
using _4차_LectureTimeTable.Model;
using _4차_LectureTimeTable.Utility;
using _4차_LectureTimeTable.View;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace _4차_LectureTimeTable.Controller
{
    public class CourseOfInterestAdder :CourseFinder //관심과목 담기 클래스 (강의 검색 기능 클래스 상속 받기) //네이밍 바꾸기
    {
        LectureOfInterestAdderUi lectureOfInterestAdderUi = new LectureOfInterestAdderUi();

        public CourseOfInterestAdder( MenuUi menuUi,DataStorage dataStorage, LectureException lectureException, MenuSelectController menuSelectController) : base(dataStorage,lectureException, menuUi,menuSelectController)
        { 
        }

        public int selectedMenu;
        public string[] menuList = { "○ 관심과목 검색", "○ 관심과목 내역", "○ 관심과목 삭제" };
        bool isDoGoBackToBeforeMenu = false;

        public void ControllAddInterestLectureMenu(UserDTO userInformation) //관심과목 메뉴 선택 함수
        {
            Console.Clear();
            isDoGoBackToBeforeMenu = false;
            menuUi.PrintMenuUi(userInformation.UserName);
            selectedMenu = menuSelectController.SelectMenuWithUpAndDown(menuList, 3, 42, 12); //관심과목 메뉴 상하키로 선택하는 함수

            switch (selectedMenu)
            {
                case (int)InterestLectureMenuList.ADDER:
                    AddLecture(userInformation); //관심과목 추가하는 함수 호출
                    break;

                case (int)InterestLectureMenuList.CHECKER:
                    Console.Clear();
                    menuUi.PrintStatusOfInterestedLecture(userInformation.AvailableCreditsForRegistrationOfInterestLecture, userInformation.EarnedCreditsOfInterestLecture);
                    CheckInterestLecture(userInformation); //관심과목 담긴 목록 확인하는 함수 호출
                    break;

                case (int)InterestLectureMenuList.DELETER:
                    DeleteInterestLecture(userInformation);
                    break;
            }
        }

        public string courseRegistrationNumber; //담는 과목 번호 //지역변수로 내리기
        public bool isInputValid = false;
        public bool isAddPosibility = true;
        public bool isIdInTheSearchList = false;
        
        
        public void AddLecture(UserDTO userInformation) //관심과목 호출하는 함수 //함수 길이 줄이기
        {
            FindCourse();
            userInformation.AvailableCreditsForRegistrationOfInterestLecture = 24;
            userInformation.EarnedCreditsOfInterestLecture = 0;
            int CursorPositionX = 55;
            int CursorPositionY = Console.CursorTop + 1;

            while (!isDoGoBackToBeforeMenu)
            {
                isInputValid = false;
                isAddPosibility = true;
                isIdInTheSearchList = false;

                menuUi.PrintStatusOfInterestedLecture(userInformation.AvailableCreditsForRegistrationOfInterestLecture, userInformation.EarnedCreditsOfInterestLecture);

                while (!isInputValid) //담을 과목 번호 입력 받기
                {
                    courseRegistrationNumber = ToReceiveInput.ReceiveInput(CursorPositionX, CursorPositionY, 3, Constants.IS_NOT_PASSWORD);
                    isInputValid = lectureException.JudgeCourseNumberRegularExpression(CursorPositionX, CursorPositionY, courseRegistrationNumber);
                }

                //밑에 예외처리를 메소드 빼기 > 메소드는 하나의 기능만 하기

                //예외처리 1.이미 관심과목에 담았을때
                for (int i = 0; i < userInformation.UserInterestLecture.Count; i++) 
                {
                    if (userInformation.UserInterestLecture[i].LectureId == courseRegistrationNumber) 
                    {
                        menuUi.PrintAlreadyContainErrorMesseage(); //오류메세지 출력
                        isAddPosibility = false;
                    }
                }
                //예외처리 2. 학점수가 초과되었을때
                if ((userInformation.AvailableCreditsForRegistrationOfInterestLecture - Convert.ToInt32((dataStorage.lectureTotalData.GetValue(int.Parse(courseRegistrationNumber),8) )) )<0) 
                {
                    menuUi.PrintExcessCreditsErrorMesseage(); //오류메세지 출력
                   
                    isAddPosibility = false;
                }

                //예외처리 3. 리스트에 없는 항목이 추가 되었을때
                for (int i = 0; i < dataStorage.searchedLectureData.Count; i++) {
                    if (dataStorage.searchedLectureData[i] == int.Parse(courseRegistrationNumber)) //출력된 리스트에 항목이 있을 경우
                    {
                        isIdInTheSearchList = true;
                        break;
                    }
                }
                if(isIdInTheSearchList == false) //리스트에 없을 때
                {
                    menuUi.PrintLectureIdIsNotInTheSearchedList();
               
                    isAddPosibility = false;
                }


                if (isAddPosibility)//예외처리 후 isAddPosibility 가 true 일 경우
                {
                    for (int i = 1; i <= dataStorage.lectureTotalData.GetLength(0); i++) //엑셀 모든 열 탐색
                    {
                        if (dataStorage.lectureTotalData.GetValue(i, 1).ToString() == courseRegistrationNumber) // 엑셀 번호값이랑 입력번호값이랑 비교
                        {
                            LectureDTO lectureDTO = new LectureDTO();
                            lectureDTO.LectureId = dataStorage.lectureTotalData.GetValue(i, 1).ToString();
                            lectureDTO.Major = dataStorage.lectureTotalData.GetValue(i, 2).ToString();
                            lectureDTO.CourseNumber = dataStorage.lectureTotalData.GetValue(i, 3).ToString();
                            lectureDTO.CourseClass = dataStorage.lectureTotalData.GetValue(i, 4).ToString();
                            lectureDTO.LectureName = dataStorage.lectureTotalData.GetValue(i, 5).ToString();
                            lectureDTO.CourseClassification = dataStorage.lectureTotalData.GetValue(i, 6).ToString();
                            lectureDTO.Grade = dataStorage.lectureTotalData.GetValue(i, 7).ToString();
                            lectureDTO.Credit = dataStorage.lectureTotalData.GetValue(i, 8).ToString();
                            lectureDTO.LectureTime = dataStorage.lectureTotalData.GetValue(i, 9).ToString();
                            lectureDTO.LectureClassroom = dataStorage.lectureTotalData.GetValue(i, 10).ToString();
                            lectureDTO.Professor = dataStorage.lectureTotalData.GetValue(i, 11).ToString();
                            lectureDTO.Language = dataStorage.lectureTotalData.GetValue(i, 12).ToString();
                            //해당 번호 강의 정보 DTO에 저장하기

                            userInformation.AvailableCreditsForRegistrationOfInterestLecture -= int.Parse(lectureDTO.Credit); //신청가능 학점 수 줄이기
                            userInformation.EarnedCreditsOfInterestLecture += int.Parse(lectureDTO.Credit); //신청학점 수 추가하기

                            userInformation.UserInterestLecture.Add(lectureDTO);//해당 DTO 인스턴스 데이터 저장소에 저장
                        }
                    }
                    menuUi.PrintInterestLectureStoredSuccess();
                }
                Console.SetCursorPosition(0, CursorPositionY - 1);
                if (Console.ReadKey().Key == ConsoleKey.Escape) isDoGoBackToBeforeMenu = true;
            }
        }


        public void CheckInterestLecture(UserDTO userInformation) //관심과목 내역 조회
        {
            for (int i = 0; i < userInformation.UserInterestLecture.Count; i++)
            {
                string[] maximumLengthOfStringsInEachRow = { "184", "기계항공우주공학부", "004714", "001", "Capstone디자인(산학협력프로젝트)", "공통교양필수", "1", "1", "수 16:30~18:30, 금 09:00~11:00", "센B201,센B209", "Abolghasem Sadeghi-Niaraki", "영어/한국어" };

                //문자열과 출력해야 하는 공백 칸수 함수에 인자로 전달 
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].LectureId, userInformation.UserInterestLecture[i].LectureId.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[0]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].LectureId));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].Major, userInformation.UserInterestLecture[i].Major.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[1]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].Major));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].CourseNumber, userInformation.UserInterestLecture[i].CourseNumber.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[2]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].CourseNumber));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].CourseClass, userInformation.UserInterestLecture[i].CourseClass.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[3]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].CourseClass));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].LectureName, userInformation.UserInterestLecture[i].LectureName.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[4]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].LectureName));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].CourseClassification, userInformation.UserInterestLecture[i].CourseClassification.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[5]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].CourseClassification));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].Grade, userInformation.UserInterestLecture[i].Grade.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[6]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].Grade));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].Credit, userInformation.UserInterestLecture[i].Credit.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[7]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].Credit));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].LectureTime, userInformation.UserInterestLecture[i].LectureTime.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[8]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].LectureTime));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].LectureClassroom, userInformation.UserInterestLecture[i].LectureClassroom.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[9]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].LectureClassroom));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].Professor, userInformation.UserInterestLecture[i].Professor.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[10]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].Professor));
                menuUi.PrintExistLectureInformation(userInformation.UserInterestLecture[i].Language, userInformation.UserInterestLecture[i].Language.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[11]) - Encoding.Default.GetByteCount(userInformation.UserInterestLecture[i].Language));
                Console.WriteLine("");//줄 띄우기
            }
            Console.ReadKey(true);
        }

        public void DeleteInterestLecture(UserDTO userInformation) 
        {
            string DeletionLectureId = "";
            int CursorPositionX = 55;
            int CursorPositionY = Console.CursorTop + 1;
            bool isIdInTheList = false;
            bool isInputValid = false;

            Console.Clear();
            while (!isDoGoBackToBeforeMenu)
            {
                isInputValid = false;

                CheckInterestLecture(userInformation); //관심과목 리스트 출력
                menuUi.PrintDeletionLecture(userInformation.AvailableCreditsForRegistrationOfInterestLecture, userInformation.EarnedCreditsOfInterestLecture); //관심과목 삭제 메뉴 창 출력

                while (!isInputValid) //삭제할 과목 번호 입력 받기
                {
                    DeletionLectureId = ToReceiveInput.ReceiveInput(CursorPositionX, CursorPositionY, 3, Constants.IS_NOT_PASSWORD);
                    isInputValid = lectureException.JudgeCourseNumberRegularExpression(CursorPositionX, CursorPositionY, DeletionLectureId);
                }

                for (int i = 0; i < userInformation.UserInterestLecture.Count; i++) //관심과목 리스트와 비교하면서 탐색
                {
                    if (DeletionLectureId == userInformation.UserInterestLecture[i].LectureId) // 관심과목 리스트에 해당 아이디가 있다면
                    {
                        userInformation.UserInterestLecture.RemoveAt(i);
                        isIdInTheList = true;
                        break;
                    }
                }

                if (isIdInTheList)
                {
                    menuUi.PrintDeleteLectureSuccess();

                }
                else // 관심과목 리스트에 검색한 아이디가 없는 경우
                {
                    menuUi.PrintDeleteInterestLectureFail();
                }

                Console.SetCursorPosition(0, CursorPositionY - 1);
                if (Console.ReadKey().Key == ConsoleKey.Escape) isDoGoBackToBeforeMenu = true;
            }

        }


    }
}
