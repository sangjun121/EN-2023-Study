using _4차_LectureTimeTable.DataBase;
using _4차_LectureTimeTable.ExceptionHandling;
using _4차_LectureTimeTable.Model;
using _4차_LectureTimeTable.Utility;
using _4차_LectureTimeTable.View;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.Controller
{
    public class CourseRegistration : CourseFinder
    {
        //인스턴스 상속받기
        public CourseRegistration(MenuUi menuUi, DataStorage dataStorage, LectureException lectureException, MenuSelectController menuSelectController) : base(dataStorage, lectureException, menuUi, menuSelectController)
        {
        }

        public string[] lectureRegistrationPrintList = { "수강신청", "수강신청 내역", "수강신청 시간표", "수강과목 삭제" };
        public string[] kindOfLectureRegistrationList = { "관심과목으로 신청", "강의 검색 신청" };
        public int selectedMenu;
        public bool isDoGoBackToBeforeMenu = false;
        public int countOFHalfHourIntervals; //총 수강시간이 30분단위로 몇번 반복 되는지 저장 ( 1시간 30분의 경우 3이 저장된다.) 

        public void ControllCourseRegistrationMenu(UserDTO userInformation) //수강 신청 전체 통제 함수
        {
            Console.Clear();
            isDoGoBackToBeforeMenu = false;
            menuUi.PrintMenuUi(userInformation.UserName); 
            selectedMenu = menuSelectController.SelectMenuWithUpAndDown(lectureRegistrationPrintList, 4, 42, 12);

            switch (selectedMenu)
            {
                case (int)LectureRegistrationMenuList.LECTURE_REGISTRATION: //수강신청 하기
                    Console.SetWindowSize(192, 30);
                    RegistrateLecture(userInformation);
                    break;
                case (int)LectureRegistrationMenuList.CHECK_LECTURE_REGISTRATION: // 수강 신청 내역 조회
                    Console.SetWindowSize(192, 30);
                    CheckRegistratedLecture(userInformation);
                    break;
                case (int)LectureRegistrationMenuList.TIMETABLE_OF_LECTURE_REGISTRATION: //시간표 출력하기
                    Console.SetWindowSize(235, 30);
                    CheckTimeTable(userInformation);
                    break;
                case (int)LectureRegistrationMenuList.DELETE_REGISTRATED_LECTURE: //신청과목 삭제하기
                    Console.SetWindowSize(192, 30);
                    DeleteRegistratedLecture(userInformation);
                    break;
            }
            
        }
        
        public void RegistrateLecture(UserDTO userInformation) //수강 신청 두번째 메뉴선택 함수
        {
            Console.Clear();
            isDoGoBackToBeforeMenu = false;
            menuUi.PrintMenuUi(userInformation.UserName);
            selectedMenu = menuSelectController.SelectMenuWithUpAndDown(kindOfLectureRegistrationList, 2, 42, 12);
            switch (selectedMenu)
            {
                case (int)KindOfLectureRegistration.WITH_INTEREST_LECTURE: //관심과목으로 수강신청 하기
                    RegistrateLectureWithInterestList(userInformation);
                    break;
                case (int)KindOfLectureRegistration.WITH_SEARCH_FUNCTION: //강의 검색으로 수강신청하기
                    RegistrateLectureWithSearch(userInformation);
                    break;
            }
        }
        public void CheckRegistratedLecture(UserDTO userInformation) //수강신청 내역 조회 함수   // 공통함수 클래스 만들기 동일한 함수
        {
            Console.Clear();
            for (int i = 0; i < userInformation.UserRegistratedLecture.Count; i++)
            {
                string[] maximumLengthOfStringsInEachRow = { "184", "기계항공우주공학부", "004714", "001", "Capstone디자인(산학협력프로젝트)", "공통교양필수", "1", "1", "수 16:30~18:30, 금 09:00~11:00", "센B201,센B209", "Abolghasem Sadeghi-Niaraki", "영어/한국어" };

                //문자열과 출력해야 하는 공백 칸수 함수에 인자로 전달 
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].LectureId, userInformation.UserRegistratedLecture[i].LectureId.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[0]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].LectureId));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].Major, userInformation.UserRegistratedLecture[i].Major.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[1]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].Major));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].CourseNumber, userInformation.UserRegistratedLecture[i].CourseNumber.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[2]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].CourseNumber));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].CourseClass, userInformation.UserRegistratedLecture[i].CourseClass.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[3]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].CourseClass));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].LectureName, userInformation.UserRegistratedLecture[i].LectureName.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[4]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].LectureName));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].CourseClassification, userInformation.UserRegistratedLecture[i].CourseClassification.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[5]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].CourseClassification));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].Grade, userInformation.UserRegistratedLecture[i].Grade.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[6]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].Grade));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].Credit, userInformation.UserRegistratedLecture[i].Credit.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[7]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].Credit));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].LectureTime, userInformation.UserRegistratedLecture[i].LectureTime.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[8]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].LectureTime));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].LectureClassroom, userInformation.UserRegistratedLecture[i].LectureClassroom.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[9]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].LectureClassroom));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].Professor, userInformation.UserRegistratedLecture[i].Professor.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[10]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].Professor));
                menuUi.PrintExistLectureInformation(userInformation.UserRegistratedLecture[i].Language, userInformation.UserRegistratedLecture[i].Language.Length + Encoding.Default.GetByteCount(maximumLengthOfStringsInEachRow[11]) - Encoding.Default.GetByteCount(userInformation.UserRegistratedLecture[i].Language));
                Console.WriteLine("");//줄 띄우기
            }
            Console.ReadKey(true);

        }

        public bool isInputVaild = false;

        public void RegistrateLectureWithInterestList(UserDTO userInformation) // 관심과목 리스트로 수강신청하는 함수
        {
            userInformation.AvailableCreditsForRegistration = 24;
            userInformation.EarnedCredits = 0;

            Console.Clear();
            CheckInterestLecture(userInformation); //관심과목 담긴 목록 확인하는 함수 호출
            int CursorPositionX;
            int CursorPositionY; //관심과목 목록을 출력한 뒤 CursorTop을 이용해서 좌표구하기

            string courseRegistrationNumber=""; // 수강신청 강의 번호
            bool isIdInTheSearchList = false;
            bool isRegistratePosibility = true;

            while (!isDoGoBackToBeforeMenu)
            {
                isInputVaild = false;
                isIdInTheSearchList = false;
                isRegistratePosibility = true;

                Console.Clear();
                CheckInterestLecture(userInformation); //관심과목 담긴 목록 확인하는 함수 호출
                menuUi.PrintLectureRegistration(userInformation.AvailableCreditsForRegistration, userInformation.EarnedCredits); //수강신청 입력창 출력
                CursorPositionX = 55;
                CursorPositionY = Console.CursorTop - 2;

                
                while (!isInputVaild) //신청할 과목 번호 입력 받기
                {
                    courseRegistrationNumber = ToReceiveInput.ReceiveInput(CursorPositionX, CursorPositionY, 3, Constants.IS_NOT_PASSWORD);
                    if(courseRegistrationNumber == "ESC")
                    {
                        isDoGoBackToBeforeMenu = true;
                        break;
                    }
                    isInputVaild = lectureException.JudgeCourseNumberRegularExpression(CursorPositionX, CursorPositionY, courseRegistrationNumber);
                }

                //예외처리. 리스트에 없는 항목이 추가 되었을때
                for (int i = 0; i < userInformation.UserInterestLecture.Count; i++)
                {
                    if (userInformation.UserInterestLecture[i].LectureId == courseRegistrationNumber) //출력된 리스트에 항목이 있을 경우
                    {
                        isIdInTheSearchList = true;
                        break;
                    }
                }

                if (isIdInTheSearchList == false) //리스트에 없을 때
                {
                    Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                    menuUi.PrintLectureIdIsNotInTheSearchedList();
                    isRegistratePosibility = false;
                }
                

                for (int i = 0;  i < userInformation.UserInterestLecture.Count; i++)
                {
                    if (isRegistratePosibility && (courseRegistrationNumber == userInformation.UserInterestLecture[i].LectureId))
                    {
                        ChangeTimeTypeToDateTimeStruct(userInformation.UserInterestLecture[i].LectureTime, userInformation.UserInterestLecture[i].LectureName, userInformation, i, CursorPositionX, CursorPositionY);
                    }
                }

                Console.ReadKey(true);
            }

        }


        public void CheckInterestLecture(UserDTO userInformation) //관심과목 리스트 내역 조회하는 함수
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

        
        public void  ChangeTimeTypeToDateTimeStruct(string time,string lectureName, UserDTO userInformation, int index,int CursorPositionX, int CursorPositionY)//시간대 저장형태 구분하고 DateTime 형태로 변환 하는 함수
        {
            string dayOfTheWeek;
            DateTime startTime;
            DateTime endTime;

            if (lectureException.JudgeTimeTypeRegularExpression(time, @"^[^가-힣]*[가-힣][^가-힣]*[가-힣][^가-힣]*$")) //한글이 2번 있을 때 > 일주일에 수업이 2번 있는 경우
            {
                if (lectureException.JudgeTimeTypeRegularExpression(time, @"^[^,] *,[^,] *$")) //일주일에 수업 두번 있고 서로 시간대가 다른 경우 (쉼표로 구분되어 있다)
                {
                    //두번 수업중 처음 요일 처리
                    dayOfTheWeek = time.Substring(0, 1);
                    startTime = DateTime.ParseExact(time.Substring(2,5), "HH:mm", CultureInfo.InvariantCulture);
                    endTime = DateTime.ParseExact(time.Substring(8, 5), "HH:mm", CultureInfo.InvariantCulture);
                    GetClassTime(startTime, endTime);
                    SetTimeTable(userInformation, startTime, countOFHalfHourIntervals, dayOfTheWeek, lectureName, index, CursorPositionX, CursorPositionY);

                    //두번 수업중 두번째 요일 처리
                    dayOfTheWeek = time.Substring(15, 1);
                    startTime = DateTime.ParseExact(time.Substring(17, 5), "HH:mm", CultureInfo.InvariantCulture);
                    endTime = DateTime.ParseExact(time.Substring(23, 5), "HH:mm", CultureInfo.InvariantCulture);
                    GetClassTime(startTime, endTime);
                    SetTimeTable(userInformation, startTime, countOFHalfHourIntervals, dayOfTheWeek, lectureName, index, CursorPositionX, CursorPositionY);
                }
                else //일주일에 수업이 두번 있고 서로 시간대가 같은 경우 (쉼표가 없다)
                {
                    //두번 수업중 처음 요일 처리
                    dayOfTheWeek = time.Substring(0, 1);
                    startTime = DateTime.ParseExact(time.Substring(4, 5), "HH:mm", CultureInfo.InvariantCulture);
                    endTime = DateTime.ParseExact(time.Substring(10, 5), "HH:mm", CultureInfo.InvariantCulture);
                    GetClassTime(startTime, endTime);
                    SetTimeTable(userInformation, startTime, countOFHalfHourIntervals, dayOfTheWeek, lectureName, index, CursorPositionX, CursorPositionY);

                    //두번 수업중 두번째 요일 처리
                    dayOfTheWeek = time.Substring(2, 1);
                    startTime = DateTime.ParseExact(time.Substring(4, 5), "HH:mm", CultureInfo.InvariantCulture);
                    endTime = DateTime.ParseExact(time.Substring(10, 5), "HH:mm", CultureInfo.InvariantCulture);
                    GetClassTime(startTime, endTime);
                    SetTimeTable(userInformation, startTime, countOFHalfHourIntervals, dayOfTheWeek, lectureName, index, CursorPositionX, CursorPositionY);
                } 
            }

            else if(lectureException.JudgeTimeTypeRegularExpression(time, @"^[^가-힣]*[가-힣][^가-힣]*$"))//일주일에 수업이 한번 있는 경우
            {
                dayOfTheWeek = time.Substring(0, 1);
                startTime = DateTime.ParseExact(time.Substring(2, 5), "HH:mm", CultureInfo.InvariantCulture);
                endTime = DateTime.ParseExact(time.Substring(8, 5), "HH:mm", CultureInfo.InvariantCulture);
                GetClassTime(startTime, endTime);
                SetTimeTable(userInformation, startTime, countOFHalfHourIntervals, dayOfTheWeek, lectureName, index, CursorPositionX, CursorPositionY);
            }

            else if (lectureException.JudgeTimeTypeRegularExpression(time, @"^[가-힣]{0}$")) // 시간 정보란이 공백일 때 > 비대면 강의일 경우
            {
                userInformation.TimeTable[26,0] = lectureName;
            }
        }

        public void GetClassTime(DateTime startTime, DateTime endTime) // 수업시간 구하기
        {
            TimeSpan timeDifference = endTime - startTime; //수업시간을 구하기 위해 시간 차이 빼기
            countOFHalfHourIntervals = (int)(timeDifference.TotalMinutes / 30);
        }

        //시간표에 저장하는 함수
        public void SetTimeTable(UserDTO userInformation, DateTime startTime, int countOFHalfHourIntervals, string dayOfTheWeek,string lectureName,int index, int CursorPositionX, int CursorPositionY)
        {
            int filledCount = 0; 
            // 배열의 x y 좌표값 구하기
            int arrayColumn = 0;
            int arrayRow = 26;

            //열 탐색
            for(int i = 1; i<=5; i++)//월~금 탐색
            {
                if (userInformation.TimeTable[0,i] == dayOfTheWeek)
                {
                    arrayColumn = i;
                    break;
                }
            }
            //요일이 없을 경우, 예외값처리하기

            //행 탐색
            for(int i =1; i<=25; i++) 
            {
                 //시간표 0번 열(좌측 열)에 저장된 시간값과 강의의 시작시간과 비교해서 시간표에 저장할 위치 찾기
                if (DateTime.ParseExact(userInformation.TimeTable[i, 0].Substring(0, 5), "HH:mm", CultureInfo.InvariantCulture) == startTime)
                {
                    arrayRow = i;
                    break;
                }
            }

            for(int i=0; i< countOFHalfHourIntervals; i++)
            {
                if (userInformation.TimeTable[arrayRow, arrayColumn] == null) 
                {
                    userInformation.TimeTable[arrayRow, arrayColumn] = lectureName;
                    arrayRow += 1;
                    filledCount +=1;
                }
                else // 한칸이라도 겹치면 안내문 출력후 탈출하기
                {
                    Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                    menuUi.PrintLectureIsTimeOverLaped(); 
                    break;
                }
            }

            if(filledCount == countOFHalfHourIntervals) // 다른강의랑 한칸도 안겹칠 경우(수강신청 가능)
            {
                Console.SetCursorPosition(CursorPositionX, CursorPositionY);
                menuUi.PrintLectureIsSuccess();

                //수강신청 성공한 과목 리스트에 저장
                LectureDTO lectureInformation = new LectureDTO(); //굳이 대입 안하고 그냥 리스트에 add 해버리면 그만
                lectureInformation.LectureId = userInformation.UserInterestLecture[index].LectureId;
                lectureInformation.Major = userInformation.UserInterestLecture[index].Major;
                lectureInformation.CourseNumber = userInformation.UserInterestLecture[index].CourseNumber;
                lectureInformation.CourseClass = userInformation.UserInterestLecture[index].CourseClass;
                lectureInformation.LectureName = userInformation.UserInterestLecture[index].LectureName;
                lectureInformation.CourseClassification = userInformation.UserInterestLecture[index].CourseClassification;
                lectureInformation.Grade = userInformation.UserInterestLecture[index].Grade;
                lectureInformation.Credit = userInformation.UserInterestLecture[index].Credit;
                lectureInformation.LectureTime = userInformation.UserInterestLecture[index].LectureTime;
                lectureInformation.LectureClassroom = userInformation.UserInterestLecture[index].LectureClassroom;
                lectureInformation.Professor = userInformation.UserInterestLecture[index].Professor;
                lectureInformation.Language = userInformation.UserInterestLecture[index].Language;

                userInformation.UserRegistratedLecture.Add(lectureInformation);

                //수강신청 가능학점 및 등록된 학점 계산
                userInformation.AvailableCreditsForRegistration -= int.Parse(userInformation.UserInterestLecture[index].Credit);
                userInformation.EarnedCredits += int.Parse(userInformation.UserInterestLecture[index].Credit);

                userInformation.UserInterestLecture.RemoveAt(index);
            }
        }

        //강의 조회로 수강신청하는 함수
        public void RegistrateLectureWithSearch(UserDTO userInformation)
        {
            FindCourse();
            userInformation.AvailableCreditsForRegistration = 24;
            userInformation.EarnedCredits = 0;
            int CursorPositionX = 55;
            int CursorPositionY = Console.CursorTop + 1;
            string courseRegistrationNumber="";
            bool isInputValid = false;
            bool isAddPosibility = true;
            bool isIdInTheSearchList = false;

            while (!isDoGoBackToBeforeMenu)
            {
                isInputValid = false;
                isAddPosibility = true;
                isIdInTheSearchList = false;

                menuUi.PrintLectureRegistration(userInformation.AvailableCreditsForRegistration, userInformation.EarnedCredits);

                while (!isInputValid) //신청 과목 번호 입력 받기
                {
                    courseRegistrationNumber = ToReceiveInput.ReceiveInput(CursorPositionX, CursorPositionY, 3, Constants.IS_NOT_PASSWORD);
                    isInputValid = lectureException.JudgeCourseNumberRegularExpression(CursorPositionX, CursorPositionY, courseRegistrationNumber);
                }

                //밑에 예외처리를 메소드 빼기 > 메소드는 하나의 기능만 하기

                //예외처리 1.이미 수강신청 했을때
                for (int i = 0; i < userInformation.UserRegistratedLecture.Count; i++)
                {
                    if (userInformation.UserRegistratedLecture[i].LectureId == courseRegistrationNumber)
                    {
                        menuUi.PrintAlreadyRegistratedErrorMesseage(); //오류메세지 출력
                        isAddPosibility = false;
                    }
                }
                //예외처리 2. 학점수가 초과되었을때
                if ((userInformation.AvailableCreditsForRegistration - Convert.ToInt32((dataStorage.lectureTotalData.GetValue(int.Parse(courseRegistrationNumber), 8)))) < 0)
                {
                    menuUi.PrintExcessCreditsErrorMesseage(); //오류메세지 출력

                    isAddPosibility = false;
                }

                //예외처리 3. 리스트에 없는 항목이 추가 되었을때
                for (int i = 0; i < dataStorage.searchedLectureData.Count; i++)
                {
                    if (dataStorage.searchedLectureData[i] == int.Parse(courseRegistrationNumber)) //출력된 리스트에 항목이 있을 경우
                    {
                        isIdInTheSearchList = true;
                        break;
                    }
                }
                if (isIdInTheSearchList == false) //리스트에 없을 때
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

                            userInformation.AvailableCreditsForRegistration -= int.Parse(lectureDTO.Credit); //신청가능 학점 수 줄이기
                            userInformation.EarnedCredits += int.Parse(lectureDTO.Credit); //신청학점 수 추가하기

                            userInformation.UserRegistratedLecture.Add(lectureDTO);//해당 DTO 인스턴스 데이터 저장소에 저장

                        }
                    }
                    menuUi.PrintLectureIsSuccess();
                }
                Console.SetCursorPosition(0, CursorPositionY - 1);
                if (Console.ReadKey().Key == ConsoleKey.Escape) isDoGoBackToBeforeMenu = true;
            }

            //시간표에 추가하기
            for (int i = 0; i < userInformation.UserRegistratedLecture.Count; i++)
            { 
                ChangeTimeTypeToDateTimeStruct(userInformation.UserRegistratedLecture[i].LectureTime, userInformation.UserRegistratedLecture[i].LectureName, userInformation, i, CursorPositionX, CursorPositionY);
            }

        }


        //수강신청 시간표 출력 함수
        public void CheckTimeTable(UserDTO userInformation)
        {
            Console.Clear();
            menuUi.PrintTimeTableMain();
            for(int i=0; i <27; i++)
            {
                for(int j=0; j<6; j++)
                {
                    Console.SetCursorPosition(30 * j, Console.CursorTop);
                    Console.Write(userInformation.TimeTable[i, j]);
                }
                Console.WriteLine();//한줄 띄우기
            }
            Console.ReadKey(true);
        }

        //수강신청 삭제
        public void DeleteRegistratedLecture(UserDTO userInformation) 
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

                CheckRegistratedLecture(userInformation); //수강신청 리스트 출력
                menuUi.PrintDeletionLecture(userInformation.AvailableCreditsForRegistration, userInformation.EarnedCredits); //강의 삭제 메뉴 창 출력

                while (!isInputValid) //삭제할 과목 번호 입력 받기
                {
                    DeletionLectureId = ToReceiveInput.ReceiveInput(CursorPositionX, CursorPositionY, 3, Constants.IS_NOT_PASSWORD);
                    isInputValid = lectureException.JudgeCourseNumberRegularExpression(CursorPositionX, CursorPositionY, DeletionLectureId);
                }

                for (int i = 0; i < userInformation.UserRegistratedLecture.Count; i++) //수강신청 리스트와 비교하면서 탐색
                {
                    if (DeletionLectureId == userInformation.UserRegistratedLecture[i].LectureId) // 수강신청 리스트에 해당 아이디가 있다면
                    {
                        userInformation.UserRegistratedLecture.RemoveAt(i);
                        isIdInTheList = true;
                        break;
                    }
                }

                if (isIdInTheList)
                {
                    menuUi.PrintDeleteLectureSuccess();

                }
                else // 수강신청 리스트에 검색한 아이디가 없는 경우
                {
                    menuUi.PrintDeleteRegistratedLectureFail();
                }

                Console.SetCursorPosition(0, CursorPositionY - 1);
                if (Console.ReadKey().Key == ConsoleKey.Escape) isDoGoBackToBeforeMenu = true;
            }
        }
    }
}
