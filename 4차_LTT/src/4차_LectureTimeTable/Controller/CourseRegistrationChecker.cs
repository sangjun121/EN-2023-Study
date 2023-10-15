using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using _4차_LectureTimeTable.DataBase;
using _4차_LectureTimeTable.Model;

namespace _4차_LectureTimeTable.Controller
{
    public class CourseRegistrationChecker
    {
        UserDTO userInformation;

        public CourseRegistrationChecker(UserDTO userInformation){     
            this.userInformation = userInformation;
        }

        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;

        public void ViewTimeTable()
        {
            ReportExcelFile();
        }

        public void ReportExcelFile()
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  // 바탕화면 경로
                string path = Path.Combine(desktopPath, "23학년도_1학기_시간표.xlsx");  // 엑셀 파일 저장 경로

                excelApp = new Excel.Application();                             // 엑셀 어플리케이션 생성
                workBook = excelApp.Workbooks.Add();                            // 워크북 추가
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기

                //함수 추가
                FillTheCellWithRegistratedLecture();
                workSheet.Columns.AutoFit();                                    // 열 너비 자동 맞춤
                workBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);    // 엑셀 파일 저장
                workBook.Close(true);
                excelApp.Quit();
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);
            }
        }

        /// 액셀 객체 해제 메소드
        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);  // 액셀 객체 해제
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();   // 가비지 수집
            }
        }

        public void FillTheCellWithRegistratedLecture()
        {
            Excel.Range range = null;

            //수강신청 내역 출력
            workSheet.Cells[1, 1] = "수 강 신 청 내 역";
            range = workSheet.get_Range("A1", "L1");
            range.Merge(true);
            workSheet.Cells[2, 1] = "NO";
            workSheet.Cells[2, 2] = "개설학과전공";
            workSheet.Cells[2, 3] = "학수번호";
            workSheet.Cells[2, 4] = "분반";
            workSheet.Cells[2, 5] = "교과목명";
            workSheet.Cells[2, 6] = "이수구분";
            workSheet.Cells[2, 7] = "학년";
            workSheet.Cells[2, 8] = "학점";
            workSheet.Cells[2, 9] = "요일 및 강의시간";
            workSheet.Cells[2, 10] = "강의실";
            workSheet.Cells[2, 11] = "교수명";
            workSheet.Cells[2, 12] = "강의언어";
            range = workSheet.get_Range("A1", "L2");
            range.Font.Bold = true; 

            for(int i=0; i< userInformation.UserRegistratedLecture.Count; i++)
            {
                workSheet.Cells[i + 3, 1] = userInformation.UserRegistratedLecture[i].LectureId;
                workSheet.Cells[i + 3, 2] = userInformation.UserRegistratedLecture[i].Major;
                workSheet.Cells[i + 3, 3] = userInformation.UserRegistratedLecture[i].CourseNumber;
                workSheet.Cells[i + 3, 4] = userInformation.UserRegistratedLecture[i].CourseClass;
                workSheet.Cells[i + 3, 5] = userInformation.UserRegistratedLecture[i].LectureName;
                workSheet.Cells[i + 3, 6] = userInformation.UserRegistratedLecture[i].CourseClassification;
                workSheet.Cells[i + 3, 7] = userInformation.UserRegistratedLecture[i].Grade;
                workSheet.Cells[i + 3, 8] = userInformation.UserRegistratedLecture[i].Credit;
                workSheet.Cells[i + 3, 9] = userInformation.UserRegistratedLecture[i].LectureTime;
                workSheet.Cells[i + 3, 10] = userInformation.UserRegistratedLecture[i].LectureClassroom;
                workSheet.Cells[i + 3, 11] = userInformation.UserRegistratedLecture[i].Professor;
                workSheet.Cells[i + 3, 12] = userInformation.UserRegistratedLecture[i].Language;
            }

            //시간표 출력
            workSheet.Cells[1, 14] = "시 간 표";
            range = workSheet.get_Range("N1", "S1");
            range.Merge(true);
            for(int i=0; i<27; i++)
            {
                for(int j =0; j<6; j++) 
                {
                    workSheet.Cells[i + 2, 14 + j] = userInformation.TimeTable[i,j];
                }
            }

            range = workSheet.get_Range("A1,S28");
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

        } 

       

    }



}
