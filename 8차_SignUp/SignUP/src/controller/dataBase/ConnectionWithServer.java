package controller.dataBase;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.Statement;
import java.sql.SQLException;

public class ConnectionWithServer {
    static Connection conn = null; //접속을 위한 객체
    static Statement st = null;    //쿼리문을 보내기 위한 객체
    static {
        try {
            /*DriverManager.getConnection(접속할주소,계정,비밀번호)*/
            conn = DriverManager.getConnection("","","");

            st = conn.createStatement();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
