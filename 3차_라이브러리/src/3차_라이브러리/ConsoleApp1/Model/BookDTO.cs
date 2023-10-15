using System;

public class BookDTO //프로퍼티 문법으로 보안된 변수 접근
{
    private int bookId;
    private string bookName;
    private string bookAuthor;
    private string bookPublisher;
    private int bookQuantity;
    private int bookPrice;
    private string bookPublicationDate;
    private string isbn;
    private string bookDescription;
    private DateTime borrowTime;
    private DateTime returnTime;

    public int BookId
    {
        get { return bookId; }
        set { bookId = value; }
    }

    public string BookName
    {
        get { return bookName; }
        set { bookName = value; }
    }

    public string BookAuthor
    {
        get { return bookAuthor; }
        set { bookAuthor = value; }
    }

    public string BookPublisher
    {
        get { return bookPublisher; }
        set { bookPublisher = value; }
    }

    public int BookQuantity
    {
        get { return bookQuantity; }
        set { bookQuantity = value; }
    }

    public int BookPrice
    {
        get { return bookPrice; }
        set { bookPrice = value; }
    }

    public string BookPublicationDate
    {
        get { return bookPublicationDate; }
        set { bookPublicationDate = value; }
    }

    public string Isbn
    {
        get { return isbn; }
        set { isbn = value; }
    }

    public string BookDescription
    {
        get { return bookDescription; }
        set { bookDescription = value; }
    }

    public DateTime BorrowTime
    {
        get { return borrowTime; }
        set { borrowTime = value; }
    }

    public DateTime ReturnTime
    {
        get { return returnTime; }
        set { returnTime = value; }
    }

}
