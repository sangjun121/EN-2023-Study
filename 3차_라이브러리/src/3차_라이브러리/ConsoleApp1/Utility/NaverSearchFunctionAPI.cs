using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1.Utility
{
    public class NaverSearchFunctionAPI
    {

        public List<BookDTO> SearchBook(string bookName, string bookCount)
        {
            List<BookDTO> searchBookList = new List<BookDTO>();

            string url = string.Format(Constants.URL, bookName, bookCount);
            string id = "";
            string secret = "";

            //웹서비스 요청
            //네이버 검색 서비스를 사용하기 위해서 요청할 때 Client ID와 Client Secret을 헤더에 추가하여 보내기
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", id);
            request.Headers.Add("X-Naver-Client-Secret", secret);

            //웹서비스 응답
            //네이버 검색 서비스를 실제 사용하기위해 WebRequest 개체의 GetResponse 메서드를 호출해서 응답받기
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //응답 결과 처리하기
            string status = response.StatusCode.ToString(); //응답 상태 코드가져오기
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream(); //응답 본문에 대한 스트림 가져오기
                StreamReader reader = new StreamReader(stream, Encoding.UTF8); //응답 스트림 읽기
                string jsonString = reader.ReadToEnd(); //응답 본문 문자열에 저장
                reader.Close();
                searchBookList = ParseJSON(jsonString); // 파싱하고 불러온 데이터 DTO에 담기
            }

            else
            {
                Console.WriteLine("Error 발생=" + status);
            }

            return searchBookList; //위에 else문 에러 발생시 리스트 원소 값 0개
        }

        public List<BookDTO> ParseJSON(string jsonString)
        {
            List<BookDTO> searchBookList = new List<BookDTO>();

            // Native Object 생성
            JObject json = JObject.Parse(jsonString);
            JArray itemsArray = (JArray)json["items"];
            string isbnFormating;
            int id = 1;

            foreach (JObject index in itemsArray)
            {
                BookDTO searchedBookDTO = new BookDTO();

                searchedBookDTO.BookId = id;
                id++;
                searchedBookDTO.BookName = ((string)index["title"]).Replace("'", "\""); // '따움표가 쿼리문에 걸리지 않게 하기 위해서 처리
                searchedBookDTO.BookAuthor = (string)index["author"];
                searchedBookDTO.BookPublisher = (string)index["publisher"];
                searchedBookDTO.BookPrice = (int)index["discount"];
                searchedBookDTO.BookPublicationDate = (string)index["pubdate"];              
                searchedBookDTO.BookDescription = ((string)index["description"]).Replace("'", "\""); ;
                isbnFormating = (string)index["isbn"]; // isbn 문자열 틀에 맞게 저장
                isbnFormating = isbnFormating.Insert(3, "-");
                isbnFormating = isbnFormating.Insert(6, "-");
                isbnFormating = isbnFormating.Insert(13, "-");
                isbnFormating = isbnFormating.Insert(15, "-");
                searchedBookDTO.Isbn = isbnFormating;

                searchBookList.Add(searchedBookDTO);
            }
            return searchBookList;
        }
    }
}
