using Microsoft.SqlServer.Server;
using Org.BouncyCastle.Bcpg;
using System;
using System.Linq.Expressions;

enum ModeNumber
{
    USER_MODE,
    ADMIN_MODE
}
enum UserMenuNumber
{
    //유저 모드 메뉴 번호 할당
    BOOK_FINDER,
    BORROWING_BOOK,
    BOOK_BORROW_LIST,
    RETURNING_BOOK,
    BOOK_RETURN_LIST,
    EDIT_USER_INF,
    DELETE_USER_INFORMATION,
    SEARCH_APPLY_NAVER_BOOK_API

}
enum AdministratorMenuNumber
{
    //관리자 모드 메뉴 번호 할당
    BOOK_FINDER,
    ADDING_BOOK,
    DELETING_BOOK,
    EDITING_BOOK,
    MEMBER_MANAGER,
    BOOK_BORROWING_STATUS,
    SEARCHING_NAVER,
    CONTROLL_LOG,
    REQUESTED_BOOK

}

enum LogManagerMenuNumber
{
    //로그 메뉴 번호 할당
    LOG_EDIT,
    LOG_SAVE_TEXT_FILE,
    LOG_DELETE_TEXT_FILE,
    LOG_RESET
}
enum LoginOrSignUpNumber
{
    LOGIN,
    SIGN_UP
}

enum UserManagementNumber
{
    DELETEING_USER,
    SAVING_USER
}

public class Constants
{
    public const string URL = "naverOpenAPIURL";


    public const bool ESC_END_FUNCTION = false; // 뒤로가기
    public const bool ENTER_AGAIN_FUNCTION = true; //다시하기 while문 재실행

    public const bool IS_PASSWORD = true;  //입력값 마스킹 처리 여부 판별용 상수
    public const bool IS_NOT_PASSWORD = false;

    public const bool IS_CANCELLATION_OF_MEMBERSHIP = true; //user모드에서 회원탈퇴 성공시 로그인 메뉴로 돌아가기 위한 불리언 변수
    public const bool IS_NOT_CANCELLATION_OF_MEMBERSHIP = false;

    public const string BOOK_NAME_REGULAR_EXPRESSION = @"^[A-Za-z가-힣0-9?!+=]{1,15}$"; //책이름
    public const string BOOK_AUTHOR_REGULAR_EXPRESSION = @"^[A-Za-z가-힣]{1,15}$"; //저자
    public const string BOOK_PUBLISHER_REGULAR_EXPRESSION = @"^[A-Za-z가-힣0-9]{1,15}$"; //출판사
    public const string BOOK_QUANTITY_REGULAR_EXPRESSION = @"^[0-9]{1,3}$"; //수량
    public const string BOOK_PRICE_REGULAR_EXPRESSION = @"^[1-9]\d{0,6}$"; // 책 가격
    public const string BOOK_PUBLISH_DATE_REGULAR_EXPRESSION = @"^(19\d{2}|20\d{2})-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$"; //출판일시
    public const string BOOK_ISBN_REGULAR_EXPRESSION = @"^(\d{3}-\d{2}-\d{6}-\d-\d)$"; //ISBN
    public const string BOOK_INFORMATION_REGULAR_EXPRESSION = @"^[A-Za-z가-힣]+$"; // 책 정보

    public const string BOOK_NAME_ERROR_MESSAGE = "한영문자 또는 숫자 또는 ?!+= 1개 이상 15개 이하로 작성 하세요!";
    public const string BOOK_AUTHOR_ERROR_MESSAGE = "한영문자 1개 이상 15개 이하로 작성 하세요!"; //저자
    public const string BOOK_PUBLISHER_ERROR_MESSAGE = "한영문자 또는 숫자 1개 이상 15개 이하로 작성 하세요!"; //출판사
    public const string BOOK_QUANTITY_ERROR_MESSAGE = "0~999 사이의 정수를 입력해 주세요!"; //수량
    public const string BOOK_PRICE_ERROR_MESSAGE = "1~999999 사이의 정수를 입력해 주세요!"; // 책 가격
    public const string BOOK_PUBLISH_DATE_ERROR_MESSAGE = "19xx or 20xx-xx-xx 를 입력해 주세요!"; //출판일시
    public const string BOOK_ISBN_ERROR_MESSAGE = "국제 표준 ISBN 형식 xxx-xx-xxxxxx-x-x 를 입력해 주세요!"; //ISBN
    public const string BOOK_INFORMATION_ERROR_MESSAGE = "최소 1개의 문자를 입력해 주세요!"; // 책 정보

    public const string USER_ID_REGULAR_EXPRESSION = @"^[a-zA-Z0-9]{8,15}$"; //ID
    public const string USER_PASSWORD_REGULAR_EXPRESSION = @"^[a-zA-Z0-9]{8,15}$"; // PASSWORD
    public const string USER_NAME_REGULAR_EXPRESSION = @"[ㄱ-ㅎ가-힣a-zA-Z]+"; //이름
    public const string USER_AGE_REGULAR_EXPRESSION = @"^(0|[1-9]\d?|1\d{2}|200)$"; // 나이
    public const string USER_NUMBER_REGULAR_EXPRESSION = @"01{1}[016789]{1}-[0-9]{3,4}-[0-9]{4}"; //휴대폰 번호

    public const string USER_ID_ERROR_MESSAGE = "숫자 + 영어 8~15글자를 입력해 주세요!"; //ID
    public const string USER_PASSWORD_ERROR_MESSAGE = "숫자 + 영어 8~15글자를 입력해 주세요!"; // PASSWORD
    public const string USER_NAME_ERROR_MESSAGE = "한글,영어포함 1글자 이상 입력해 주세요!"; // 이름
    public const string USER_AGE_ERROR_MESSAGE = "0또는 1부터 200사이의 자연수를 입력해 주세요!"; // 나이
    public const string USER_NUMBER_ERROR_MESSAGE = "01x-xxxx-xxxx 형식으로 입력해 주세요!"; //휴대폰 번호

    public const string NUMBER_REGULAR_EXPRESSION = @"^\d+$"; //숫자인지 판단하는 정규표현식
    public const string NUMBER_ERROR_MESSAGE = "숫자를 입력해 주세요!";

    public const string BOOK_APPLY_REQUEST = "신청하려면 ENTER를 눌러 주세요";
    public const string BOOK_ADD = "도서관에 추가하려면 ENTER를 눌러 주세요";
    public const string BOOK_APPLY_SUCCESS = "도서 신청이 완료 되었습니다!";
    public const string BOOK_ALREADY_IN_APPLY_LIST = "이미 신청된 도서 입니다. 다시 입력해주세요";
    public const string BOOK_ALREADY_IN_BOOK_DATA = "도서관에 존재하는 도서 입니다.";
    public const string GOBACK_OR_AGAIN = "다시 신청하려면 ENTER, 뒤로가려면 ESC를 눌러주세요";
    public const string NO_BOOK_IN_LIST = "리스트에 책이 없습니다. 다시 검색하세요.";
    public const string BOOK_REGISTRATE_SUCCESS = "도서 추가에 성공 했습니다.";
    public const string NO_LOG_IN_LIST = "리스트에 로그가 없습니다. 다시 검색하세요.";
    public const string LOG_DELETE_SUCCESS = "로그 삭제에 성공 했습니다.";
    public const string BLANK = "                                                                                       ";
    public const string REASK_RESET_INTENTION = "정말로 초기화 하시겠습니까?";
    public const string LOG_RESET_SUCCESS = "로그 초기화에 성공 했습니다.";


    //로그활동
    public const string LOGIN = "로그인";
    public const string SIGN_UP = "회원가입";
    public const string SELECT_MENU = "메뉴선택";
    public const string ALL_BOOK_VIEW = "전체 책 조회";

    public const string FIND_BOOK = "책 검색";
    public const string BORROWING_BOOK = "책 대여";
    public const string BOOK_BORROW_LIST = "대여목록";
    public const string RETURNING_BOOK = "책 반납";
    public const string BOOK_RETURN_LIST = "반납목록";
    public const string EDIT_USER_INF = "유저 정보 수정";
    public const string DELETE_USER_INFORMATION = "회원 탈퇴";
    public const string SEARCH_APPLY_NAVER_BOOK_API = "네이버 책 검색 및 도서신청";
    public const string BOOK_APPLY = "책 신청";

    public const string ADDING_BOOK = "책 추가";
    public const string DELETING_BOOK = "책 삭제";
    public const string EDITING_BOOK = "책 수정";
    public const string MEMBER_MANAGER = "회원 관리";
    public const string BOOK_BORROWING_STATUS = "대여 리스트 현황";
    public const string REQUESTED_BOOK = "신청 도서 추가하기";
    public const string CONTROLL_LOG = "로그 관리";
    public const string SEARCH_NAVER_ADD_BOOK = "네이버 도서검색 및 도서 추가";

    public const string DELETE_LOG = "로그 TEXT 파일 삭제";
    public const string SAVE_LOG = "로그 TEXT 파일 저장";
    public const string EDIT_LOG = "로그 수정";
    public const string RESET_LOG = "로그 초기화";
    public const string RESET = "초기화";
    public const string LOG_ID_SUB = "로그 아이디: ";
    public const string BOOK_ID = "책 아이디: ";
    public const string USER_NUMBER = "유저 번호: ";
    public const string CHANGE = "->";

    public const string ADMINSTRATOR = "관리자";
    public const string ADMINSTRATOR_AUTHORITY = "관리자 권한";

    public const string LOG_TEXT_FRAME_1 = "========================================================================================================================\n";
    public const string LOG_TEXT_FRAME_2 = "                                                     로그 목록\n";
    public const string LOG_TEXT_FRAME_3 = "========================================================================================================================\n";

    public const string LOG_LIST_FRAME_1 = "========================================================================================================================";
    public const string LOG_ID = "\n로그 ID     :  ";
    public const string LOG_TIME = "\n로그 시간   :  ";
    public const string LOG_USER = "\n로그 사용자 :  ";
    public const string LOG_INFORMATION = "\n로그 정보   :  ";
    public const string LOG_ACTION = "\n로그 활동   :  ";
    public const string LOG_LIST_FRAME_2 = "\n========================================================================================================================\n";




}