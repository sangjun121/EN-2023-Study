using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class BookDAO
    {
        public ConnectionWithServer connectionWithServer;

        public BookDAO()
        {
            this.connectionWithServer = new ConnectionWithServer();
        }

        public List<BookDTO> ReadAllBookData() //모든 책 정보를 가져오는 함수
        {
            BookDTO bookDTO;
            List<BookDTO> allBookInformation = new List<BookDTO>();  //모든 책 정보를 담을 리스트 선언

            string queryStatement = "SELECT * FROM book_data ;";
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                bookDTO.BookId = readedData.GetInt32(0);
                bookDTO.BookName = readedData.GetString(1);
                bookDTO.BookAuthor = readedData.GetString(2);
                bookDTO.BookPublisher = readedData.GetString(3);
                bookDTO.BookQuantity = readedData.GetInt32(4);
                bookDTO.BookPrice = readedData.GetInt32(5);
                bookDTO.BookPublicationDate = readedData.GetString(6);
                bookDTO.Isbn = readedData.GetString(7);
                bookDTO.BookDescription = readedData.GetString(8);

                allBookInformation.Add(bookDTO);//책 리스트에 추가
            }
            readedData.Close();
            return allBookInformation; //모든 책정보가 담겨 있는 리스트 전달
        }
        public void IncreaseAndDecreaseBookQuantity(BookDTO bookDTO) //책 빌린후 책 수량 1개 감소 된 상태 데이터 베이스에 업데이트 하는 함수
        {
            string queryStatement = string.Format("UPDATE book_data SET BookQuantity = '{0}' WHERE BookId = '{1}';", bookDTO.BookQuantity, bookDTO.BookId);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void SaveBorrowedBookToData(BookDTO bookDTO, UserDTO loggedInUserInformation)
        {
            string queryStatement = string.Format("INSERT INTO user_borrowed_book_list (UserId, BookId, BookName, BookAuthor, BookPublisher, BookQuantity, BookPrice, BookPublicationDate, Isbn, BookDescription, BorrowTime, ReturnTime ) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');", loggedInUserInformation.UserNumber, bookDTO.BookId, bookDTO.BookName, bookDTO.BookAuthor, bookDTO.BookPublisher, bookDTO.BookQuantity, bookDTO.BookPrice, bookDTO.BookPublicationDate, bookDTO.Isbn, bookDTO.BookDescription, bookDTO.BorrowTime.ToString("yyyy-MM-dd HH:mm:ss"), bookDTO.ReturnTime.ToString("yyyy-MM-dd HH:mm:ss"));
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public List<BookDTO> ReadBorrowedBookList(UserDTO loggedInUserInformation)
        {
            BookDTO bookDTO;
            List<BookDTO> borrowedBookInformation = new List<BookDTO>();  //빌린 책 정보를 담을 리스트 선언

            string queryStatement = string.Format("SELECT * FROM  user_borrowed_book_list WHERE UserId = '{0}';", loggedInUserInformation.UserNumber);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                bookDTO.BookId = readedData.GetInt32(1);
                bookDTO.BookName = readedData.GetString(2);
                bookDTO.BookAuthor = readedData.GetString(3);
                bookDTO.BookPublisher = readedData.GetString(4);
                bookDTO.BookQuantity = readedData.GetInt32(5);
                bookDTO.BookPrice = readedData.GetInt32(6);
                bookDTO.BookPublicationDate = readedData.GetString(7);
                bookDTO.Isbn = readedData.GetString(8);
                bookDTO.BookDescription = readedData.GetString(9);
                bookDTO.BorrowTime = readedData.GetDateTime(10);
                bookDTO.ReturnTime = readedData.GetDateTime(11);

                borrowedBookInformation.Add(bookDTO);//책 리스트에 추가
            }
            readedData.Close();
            return borrowedBookInformation; //모든 책정보가 담겨 있는 리스트 전달
        }

        public List<BookDTO> ReadReturnedBookList(UserDTO loggedInUserInformation)
        {
            BookDTO bookDTO;
            List<BookDTO> returnedBookInformation = new List<BookDTO>();  //빌린 책 정보를 담을 리스트 선언

            string queryStatement = string.Format("SELECT * FROM  user_returned_book_list WHERE UserId = '{0}';", loggedInUserInformation.UserNumber);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                bookDTO.BookId = readedData.GetInt32(1);
                bookDTO.BookName = readedData.GetString(2);
                bookDTO.BookAuthor = readedData.GetString(3);
                bookDTO.BookPublisher = readedData.GetString(4);
                bookDTO.BookQuantity = readedData.GetInt32(5);
                bookDTO.BookPrice = readedData.GetInt32(6);
                bookDTO.BookPublicationDate = readedData.GetString(7);
                bookDTO.Isbn = readedData.GetString(8);
                bookDTO.BookDescription = readedData.GetString(9);
                bookDTO.BorrowTime = readedData.GetDateTime(10);
                bookDTO.ReturnTime = readedData.GetDateTime(11);

                returnedBookInformation.Add(bookDTO);//책 리스트에 추가
            }
            readedData.Close();
            return returnedBookInformation; //모든 책정보가 담겨 있는 리스트 전달
        }

        public List<BookDTO> SearchForInputIdInBorrowedBookList(UserDTO loggedInUserInformation, string bookId)
        {
            BookDTO bookDTO;
            List<BookDTO> borrowedBookInformationOfInputId = new List<BookDTO>();  //반납할 책 정보를 담을 리스트 선언

            string queryStatement = string.Format("SELECT * FROM  user_borrowed_book_list WHERE UserId = '{0}' AND BookId = '{1}' ;", loggedInUserInformation.UserNumber, bookId);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                bookDTO.BookId = readedData.GetInt32(1);
                bookDTO.BookName = readedData.GetString(2);
                bookDTO.BookAuthor = readedData.GetString(3);
                bookDTO.BookPublisher = readedData.GetString(4);
                bookDTO.BookQuantity = readedData.GetInt32(5);
                bookDTO.BookPrice = readedData.GetInt32(6);
                bookDTO.BookPublicationDate = readedData.GetString(7);
                bookDTO.Isbn = readedData.GetString(8);
                bookDTO.BookDescription = readedData.GetString(9);
                bookDTO.BorrowTime = readedData.GetDateTime(10);
                bookDTO.ReturnTime = readedData.GetDateTime(11);

                borrowedBookInformationOfInputId.Add(bookDTO);//책 리스트에 추가
            }
            readedData.Close();
            return borrowedBookInformationOfInputId; //모든 책정보가 담겨 있는 리스트 전달
        }

        public void DeleteBookInBorrowedList(UserDTO loggedInUserInformation, string bookId)
        {
            string queryStatement = string.Format("DELETE FROM user_borrowed_book_list WHERE UserId = '{0}' AND BookId = '{1}';", loggedInUserInformation.UserNumber, bookId);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void SaveReturnedBookToData(BookDTO bookDTO, UserDTO loggedInUserInformation)
        {
            string queryStatement = string.Format("INSERT INTO user_returned_book_list (UserId, BookId, BookName, BookAuthor, BookPublisher, BookQuantity, BookPrice, BookPublicationDate, Isbn, BookDescription, BorrowTime, ReturnTime ) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}');", loggedInUserInformation.UserNumber, bookDTO.BookId, bookDTO.BookName, bookDTO.BookAuthor, bookDTO.BookPublisher, bookDTO.BookQuantity, bookDTO.BookPrice, bookDTO.BookPublicationDate, bookDTO.Isbn, bookDTO.BookDescription, bookDTO.BorrowTime.ToString("yyyy-MM-dd HH:mm:ss"), bookDTO.ReturnTime.ToString("yyyy-MM-dd HH:mm:ss"));
            connectionWithServer.CreateUpdateDelete(queryStatement);
        } 

        public void AddNewBookToData(BookDTO newBookDTO)
        {
            string queryStatement = string.Format("INSERT INTO book_data (BookName, BookAuthor, BookPublisher, BookQuantity, BookPrice, BookPublicationDate, Isbn, BookDescription) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}');", newBookDTO.BookName, newBookDTO.BookAuthor, newBookDTO.BookPublisher, newBookDTO.BookQuantity, newBookDTO.BookPrice, newBookDTO.BookPublicationDate, newBookDTO.Isbn, newBookDTO.BookDescription);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public List<BookDTO> SpecificUserBorrowedBook(int userNumber)
        {
            BookDTO bookDTO;
            List<BookDTO> borrowedBookInformationOfSpecificUser = new List<BookDTO>();  //반납할 책 정보를 담을 리스트 선언

            string queryStatement = string.Format("SELECT * FROM  user_borrowed_book_list WHERE UserId = '{0}';", userNumber);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                bookDTO.BookId = readedData.GetInt32(1);
                bookDTO.BookName = readedData.GetString(2);
                bookDTO.BookAuthor = readedData.GetString(3);
                bookDTO.BookPublisher = readedData.GetString(4);
                bookDTO.BookQuantity = readedData.GetInt32(5);
                bookDTO.BookPrice = readedData.GetInt32(6);
                bookDTO.BookPublicationDate = readedData.GetString(7);
                bookDTO.Isbn = readedData.GetString(8);
                bookDTO.BookDescription = readedData.GetString(9);
                bookDTO.BorrowTime = readedData.GetDateTime(10);
                bookDTO.ReturnTime = readedData.GetDateTime(11);

                borrowedBookInformationOfSpecificUser.Add(bookDTO);//책 리스트에 추가
            }
            readedData.Close();
            return borrowedBookInformationOfSpecificUser; //모든 책정보가 담겨 있는 리스트 전달
        }

        public int FindBookInBorrowedList(string bookId) // 책 아이디가 대여 리스트에 있을때
        {
            int bookCount;
            string queryStatement = string.Format("SELECT COUNT(*) FROM  user_borrowed_book_list WHERE BooKId = '{0}';", bookId);
            bookCount = Convert.ToInt32(connectionWithServer.SelectUsedExecuteScalarMethod(queryStatement));
            return bookCount;
        }
        public void DeleteBook(string deletedBookId)
        {
            string queryStatement = string.Format("DELETE FROM book_data WHERE BookId = '{0}';", deletedBookId);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void EditBook(BookDTO editedBookDTO, string editedBookId)
        {
            string queryStatement = string.Format("UPDATE book_data SET BookName = '{0}', BookAuthor = '{1}', BookPublisher = '{2}', BookQuantity = '{3}', BookPrice = '{4}', BookPublicationDate = '{5}' WHERE BookId = '{6}';", editedBookDTO.BookName, editedBookDTO.BookAuthor, editedBookDTO.BookPublisher, editedBookDTO.BookQuantity, editedBookDTO.BookPrice, editedBookDTO.BookPublicationDate, editedBookId);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }
        public int FindUserNumberInBorrowedBookList(string userNumber)
        {
            int userCount;
            string queryStatement = string.Format("SELECT COUNT(*) FROM  user_borrowed_book_list WHERE UserId = '{0}';", userNumber);
            userCount = Convert.ToInt32(connectionWithServer.SelectUsedExecuteScalarMethod(queryStatement));
            return userCount;
        }
        public void DeleteUserInBorrowedList(string userNumber)
        {
            string queryStatement = string.Format("DELETE FROM user_borrowed_book_list WHERE UserId = '{0}';", userNumber);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void DeleteUserInReturnedList(string userNumber)
        {
            string queryStatement = string.Format("DELETE FROM user_returned_book_list WHERE UserId = '{0}';", userNumber);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void AddBookInUserApplyBookList(List<BookDTO> userApplyBooks) //user가 요청한 도서, 데이터베이스에 저장하는 함수
        {
            foreach (BookDTO book in userApplyBooks)
            {
                string queryStatement = string.Format("INSERT INTO user_apply_book_list (BookName, BookAuthor, BookPublisher, BookPrice, BookPublicationDate, BookIsbn, BookDescription ) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}');", book.BookName, book.BookAuthor, book.BookPublisher, book.BookPrice, book.BookPublicationDate, book.Isbn, book.BookDescription);
                connectionWithServer.CreateUpdateDelete(queryStatement);
            }
        }

        public int FindBookInApplyBookListWithIsbn(string isbn) // 책이 신청리스트에 이미 신청된 책일 경우
        {
            int bookCount;
            string queryStatement = string.Format("SELECT COUNT(*) FROM user_apply_book_list WHERE BookIsbn = '{0}';", isbn);
            bookCount = Convert.ToInt32(connectionWithServer.SelectUsedExecuteScalarMethod(queryStatement));   
            return bookCount;
        }

        public int FindBookInBookDataList(string isbn) // 책이 이미 도서관에 있을 경우
        {
            int bookCount;
            string queryStatement = string.Format("SELECT COUNT(*) FROM book_data WHERE Isbn = '{0}';", isbn);
            bookCount = Convert.ToInt32(connectionWithServer.SelectUsedExecuteScalarMethod(queryStatement));
            return bookCount;
        }

        public List<BookDTO> ReadApplyBookList()
        {
            BookDTO bookDTO;
            List<BookDTO> appliedBookInformation = new List<BookDTO>();  //모든 책 정보를 담을 리스트 선언

            string queryStatement = "SELECT * FROM  user_apply_book_list;";
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO(); //왜 와일문 밖에 작성하면 마지막 값으로 다 저장되는지 물어보기
                bookDTO.BookId = readedData.GetInt32(0);
                bookDTO.BookName = readedData.GetString(1);
                bookDTO.BookAuthor = readedData.GetString(2);
                bookDTO.BookPublisher = readedData.GetString(3);
                bookDTO.BookPrice = readedData.GetInt32(4);
                bookDTO.BookPublicationDate = readedData.GetString(5);
                bookDTO.Isbn = readedData.GetString(6);
                bookDTO.BookDescription = readedData.GetString(7);

                appliedBookInformation.Add(bookDTO);//책 리스트에 추가
            }
            readedData.Close();
            return appliedBookInformation; //모든 책정보가 담겨 있는 리스트 전달
        }
        public BookDTO FindBookInApplyBookListWithId(string bookId) // 책이 신청리스트에 있는지 확인
        {
            BookDTO bookDTO = new BookDTO();

            string queryStatement = string.Format("SELECT * FROM user_apply_book_list WHERE BookId = '{0}';", bookId);
            MySqlDataReader readedData = connectionWithServer.SelectUsedExecuteReader(queryStatement);

            while (readedData.Read())
            {
                bookDTO = new BookDTO();
                bookDTO.BookId = readedData.GetInt32(0);
                bookDTO.BookName = readedData.GetString(1);
                bookDTO.BookAuthor = readedData.GetString(2);
                bookDTO.BookPublisher = readedData.GetString(3);
                bookDTO.BookPrice = readedData.GetInt32(4);
                bookDTO.BookPublicationDate = readedData.GetString(5);
                bookDTO.Isbn = readedData.GetString(6);
                bookDTO.BookDescription = readedData.GetString(7);
            }
            readedData.Close();
            return bookDTO; //해당 id의 책 반환

        }

        public void AddNewBookInLibrary(BookDTO newBook)
        {
            string queryStatement = string.Format("INSERT INTO book_data (BookName, BookAuthor, BookPublisher, BookQuantity ,BookPrice, BookPublicationDate, Isbn, BookDescription ) VALUES ('{0}', '{1}','{2}','5', '{3}','{4}','{5}','{6}');", newBook.BookName, newBook.BookAuthor, newBook.BookPublisher, newBook.BookPrice, newBook.BookPublicationDate, newBook.Isbn, newBook.BookDescription);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }

        public void DeleteBookInAppliedList(string bookId)
        {
            string queryStatement = string.Format("DELETE FROM user_apply_book_list WHERE BookId = '{0}';", bookId);
            connectionWithServer.CreateUpdateDelete(queryStatement);
        }
    }
}
