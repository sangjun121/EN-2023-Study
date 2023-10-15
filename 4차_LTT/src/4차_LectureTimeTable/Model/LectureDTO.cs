using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.Model
{
    public class LectureDTO //변수 위로 올리기
    {
        private string lectureId;
        public string LectureId
        {
            get { return lectureId; }
            set { lectureId = value; }
        }

        private string major;
        public string Major
        {
            get { return major; }
            set { major = value; }
        }

        private string courseNumber;
        public string CourseNumber
        {
            get { return courseNumber; }
            set { courseNumber = value; }
        }

        private string courseClass;
        public string CourseClass
        {
            get { return courseClass; }
            set { courseClass = value; }
        }

        private string lectureName;
        public string LectureName
        {
            get { return lectureName; }
            set { lectureName = value; }
        }

        private string courseClassification;
        public string CourseClassification
        {
            get { return courseClassification; }
            set { courseClassification = value; }
        }

        private string grade;
        public string Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        private string credit;
        public string Credit
        {
            get { return credit; }
            set { credit = value; }
        }

        private string lectureTime;
        public string LectureTime
        {
            get { return lectureTime; }
            set { lectureTime = value; }
        }

        private string lectureClassroom;
        public string LectureClassroom
        {
            get { return lectureClassroom; }
            set { lectureClassroom = value; }
        }

        private string professor;
        public string Professor
        {
            get { return professor; }
            set { professor = value; }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

    }
}
