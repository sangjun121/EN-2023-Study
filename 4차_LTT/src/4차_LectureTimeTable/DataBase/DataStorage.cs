using _4차_LectureTimeTable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4차_LectureTimeTable.DataBase
{
    public class DataStorage
    {
        public DataStorage()
        {
            userData = new List<UserDTO>();
            searchedLectureData = new List<int>();

            UserDTO userOneData = new UserDTO(); //기존에 저장 된 유저 정보 
            userOneData.UserId = "";
            userOneData.UserPassword = "";
            userOneData.UserName = "";
            userData.Add(userOneData);

            UserDTO userTwoData = new UserDTO();
            userTwoData.UserId = "";
            userTwoData.UserPassword = "";
            userTwoData.UserName = "";
            userData.Add(userTwoData);

            UserDTO userThreeData = new UserDTO();
            userThreeData.UserId = "";
            userThreeData.UserPassword = "";
            userThreeData.UserName = "";
            userData.Add(userThreeData);
        }

        public List<UserDTO> userData;
        public List<int> searchedLectureData;

        public Array lectureTotalData; //엑셀에서 불러온 데이터 저장하는 배열



    }
}
