using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.Model
{
    public class UserDTO
    {
        public UserDTO() 
        {
            userInterestLecture = new List<LectureDTO>();
            userRegistratedLecture = new List<LectureDTO>();

            timeTable = new string[27, 6]; //시간표 시간 30분 단위, 월~금으로 배열 구성, 마지막줄에는 비대면 강의 
 
            timeTable[0, 1] = "월"; // 그릇이니까 대입은 다른 함수에서 
            timeTable[0, 2] = "화";
            timeTable[0, 3] = "수";
            timeTable[0, 4] = "목";
            timeTable[0, 5] = "금";
            timeTable[1, 0] = "08:00 ~ 08:30";
            timeTable[2, 0] = "08:30 ~ 09:00";
            timeTable[3, 0] = "09:00 ~ 09:30";
            timeTable[4, 0] = "09:30 ~ 10:00";
            timeTable[5, 0] = "10:00 ~ 10:30";
            timeTable[6, 0] = "10:30 ~ 11:00";
            timeTable[7, 0] = "11:00 ~ 11:30";
            timeTable[8, 0] = "11:30 ~ 12:00";
            timeTable[9, 0] = "12:00 ~ 12:30";
            timeTable[10, 0] = "12:30 ~ 13:00";
            timeTable[11, 0] = "13:00 ~ 13:30";
            timeTable[12, 0] = "13:30 ~ 14:00";
            timeTable[13, 0] = "14:00 ~ 14:30";
            timeTable[14, 0] = "14:30 ~ 15:00";
            timeTable[15, 0] = "15:00 ~ 15:30";
            timeTable[16, 0] = "15:30 ~ 16:00";
            timeTable[17, 0] = "16:00 ~ 16:30";
            timeTable[18, 0] = "16:30 ~ 17:00";
            timeTable[19, 0] = "17:00 ~ 17:30";
            timeTable[20, 0] = "17:30 ~ 18:00";
            timeTable[21, 0] = "18:00 ~ 18:30";
            timeTable[22, 0] = "18:30 ~ 19:00";
            timeTable[23, 0] = "19:00 ~ 19:30";
            timeTable[24, 0] = "19:30 ~ 20:00";
            timeTable[25, 0] = "20:00 ~ 20:30";

        }

        private string userId;
        private string userPassword;
        private string userName;
        private int availableCreditsForRegistrationOfInterestLecture; //관심과목 담기 가능 학점
        private int earnedCreditsOfInterestLecture; //담은 학점수
        private string userInterestLectureCredits;
        private int availableCreditsForRegistration;
        private int earnedCredits;

        private List<LectureDTO> userInterestLecture;  //사용자의 관심과목을 저장하는 리스트 선언
        private List<LectureDTO> userRegistratedLecture; //사용자의 수강 신청과목을 저장하는 리스트 선언

        private string[,] timeTable; //사용자의 시간표 저장하는 2차원 배열

        public string UserId
        {
            get { return userId; } 
            set { userId = value; }
        }

        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value; }
        }
        
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        
        public List<LectureDTO> UserInterestLecture
        {
            get { return userInterestLecture; }
        }
        
        public int AvailableCreditsForRegistrationOfInterestLecture
        {
            get { return  availableCreditsForRegistrationOfInterestLecture;}
            set { availableCreditsForRegistrationOfInterestLecture = value; }
        }

        public int EarnedCreditsOfInterestLecture
        {
            get { return earnedCreditsOfInterestLecture; }
            set { earnedCreditsOfInterestLecture = value; }
        }
        
        public string UserInterestLectureCredits
        {
            get { return userInterestLectureCredits; }
            set { userInterestLectureCredits = value; }
        }

        public List<LectureDTO> UserRegistratedLecture
        {
            get { return userRegistratedLecture; }
        }
        public int AvailableCreditsForRegistration
        {
            get { return availableCreditsForRegistration; }
            set { availableCreditsForRegistration = value; }
        }
        public int EarnedCredits
        {

            get { return earnedCredits; }
            set { earnedCredits = value; }

        }
        public string[,] TimeTable
        {
            get { return timeTable; }
        }
    }
}
