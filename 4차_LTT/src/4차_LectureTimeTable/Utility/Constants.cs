using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.Utility
{
    enum MenuList //초기 메뉴 리스트
    {
        COURSE_FINDER,
        COURSE_OF_INTEREST_ADDER,
        COURSE_REGISTRATION,
        COURSE_REGISTRATION_CHECKER
    }

    enum InterestLectureMenuList //관심과목 담기의 메뉴
    {
        ADDER,
        CHECKER,
        DELETER
    }

    enum LectureRegistrationMenuList
    {
        LECTURE_REGISTRATION,
        CHECK_LECTURE_REGISTRATION,
        TIMETABLE_OF_LECTURE_REGISTRATION,
        DELETE_REGISTRATED_LECTURE
    }
    enum KindOfLectureRegistration
    {
        WITH_INTEREST_LECTURE,
        WITH_SEARCH_FUNCTION
    }

    enum LectureEntriesList //강의 검색시 강의 세부사항 표현하는 상수 
    {
        MAJOR_LECTURE,
        CLASSIFICATION,
        LECTURE_NAME,
        PROFESSOR_NAME,
        GRADE,
        COURSE_NUMBER,
        CLASS_NUMBER,
        SEARCH
    }

    public class Constants
    {
        public const bool IS_PASSWORD = true;  //입력값 마스킹 처리 여부 판별용 상수
        public const bool IS_NOT_PASSWORD = false;

        public const bool IS_PRINT_NEXT_LINE = true; //상하, 좌우키 메뉴 출력 함수 구분용 상수
        public const bool IS_PRINT_NEXT_SIDE = false;

    }
}
