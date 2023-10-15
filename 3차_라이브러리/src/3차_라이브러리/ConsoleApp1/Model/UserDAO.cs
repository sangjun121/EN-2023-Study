using Microsoft.SqlServer.Server;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class UserDAO
    {
        ConnectionWithServer connectionWithServer;
        public UserDAO() 
        {
            this.connectionWithServer = new ConnectionWithServer();
        }

        public List<UserDTO> ReadAllUserNumber() //모든 유저의 번호를 불러오는 함수
        {
            List<UserDTO> userDTOList = new List<UserDTO>();

            string queryStatement = "SELECT UserNumber FROM user_data;";
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                UserDTO userDTO = new UserDTO();
                userDTO.UserNumber = readedData.GetInt32(0);
                userDTOList.Add(userDTO);
            }
            readedData.Close();
            return userDTOList;
        }
        public List<UserDTO> CompareUserAccountInformation(string id) // 유저모드 아이디 패스워드와 비교하는 함수
        {
            List<UserDTO> userDTOList = new List<UserDTO>();

            string queryStatement = string.Format("SELECT * FROM user_data WHERE UserId = '{0}';", id);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement); 

            if (readedData.Read()) //일치하는 아이디가 있는경우
            {
                UserDTO userDTO = new UserDTO(); //해당 레코드 값을 담을 UserDTO 그릇을 만들어주기
                userDTO.UserNumber = Convert.ToInt32(readedData["UserNumber"]);
                userDTO.Id = readedData["UserId"].ToString();
                userDTO.Password = readedData["UserPassword"].ToString();
                userDTO.UserPhoneNumber = readedData["UserPhoneNumber"].ToString();
                userDTO.UserName = readedData["UserName"].ToString();
                userDTO.UserAddress = readedData["UserAddress"].ToString() ;
                userDTO.UserAge = Convert.ToInt32(readedData["UserAge"]);

                userDTOList.Add(userDTO); //해당 유저 정보를 리스트에 추가하기 (리스트로 반환해주는 이유: 일치하는 값이 없을 경우 list의 원소개수가 0이므로 이 경우를 판단해주기 위해)
                readedData.Close();
                return userDTOList;
            }

            readedData.Close(); // MySqlDataReader 객체 닫아줌과 동시에 connection 닫아주기
            return userDTOList; //일치하는 아이디가 없으므로 리스트의 원소 개수는 0개 이다.
        }

        public List<UserDTO> CompareAdministratorAccountInformation(string id) // 관리자 모드 아이디 패스워드와 비교하는 함수
        {
            List<UserDTO> userDTOList = new List<UserDTO>();

            string queryStatement = string.Format("SELECT * FROM administrator_data WHERE UserId = '{0}';", id);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            if (readedData.Read()) //일치하는 아이디가 있는경우
            {
                UserDTO userDTO = new UserDTO(); //해당 레코드 값을 담을 UserDTO 그릇을 만들어주기
                userDTO.Id = readedData["UserId"].ToString();
                userDTO.Password = readedData["UserPassword"].ToString();
                userDTO.UserNumber = Convert.ToInt32(readedData["UserNumber"]);

                userDTOList.Add(userDTO); //해당 관리자 정보를 리스트에 추가하기 (리스트로 반환해주는 이유: 일치하는 값이 없을 경우 list의 원소개수가 0이므로 이 경우를 판단해주기 위해)
                readedData.Close();
                return userDTOList;
            }

            readedData.Close(); // MySqlDataReader 객체 닫아줌과 동시에 connection 닫아주기
            return userDTOList; //일치하는 아이디가 없으므로 리스트의 원소 개수는 0개 이다.
        }

        public void NewUserDataCreate(UserDTO newUserInformation)
        {
            string queryStatement = string.Format("INSERT INTO user_data ( UserId, UserPassword, UserPhoneNumber, UserName,UserAddress, UserAge) VALUES ('{0}','{1}','{2}','{3}', '{4}','{5}');", newUserInformation.Id, newUserInformation.Password, newUserInformation.UserPhoneNumber,newUserInformation.UserName,newUserInformation.UserAddress,newUserInformation.UserAge);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void EditUserDataUpdate(UserDTO editedUserInformation)
        {
            string queryStatement = string.Format("UPDATE user_data SET UserId = '{0}', UserPassword = '{1}', UserName = '{2}', UserAge = '{3}', UserPhoneNumber = '{4}', UserAddress = '{5}' WHERE UserNumber = '{6}';", editedUserInformation.Id, editedUserInformation.Password, editedUserInformation.UserName, editedUserInformation.UserAge, editedUserInformation.UserPhoneNumber, editedUserInformation.UserAddress, editedUserInformation.UserNumber);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }
        public void DeleteUserInformation(string userNumber)
        {
            string queryStatement = string.Format("DELETE FROM user_data WHERE UserNumber = '{0}';", userNumber);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }
        public string FindUserNameByUserNumber(int userNumber)
        {
            string queryStatement = string.Format("SELECT UserName FROM user_data WHERE UserNumber = '{0}';", userNumber);
            string userName =  (connectionWithServer.SelectUsedExecuteScalarMethod(queryStatement)).ToString();
            return userName;
        }
        public List <UserDTO> FindUserById(string userNumber) 
        {
            List<UserDTO> userInformationList = new List<UserDTO>();
            string queryStatement = string.Format("SELECT * FROM user_data WHERE UserNumber = '{0}';", userNumber);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while(readedData.Read())
            {
                UserDTO userInformation = new UserDTO();
                userInformation.UserNumber = readedData.GetInt32(0);
                userInformationList.Add(userInformation);
            }
            readedData.Close(); // MySqlDataReader 객체 닫아줌과 동시에 connection 닫아주기
            return userInformationList; 
        }
      

    }
}
