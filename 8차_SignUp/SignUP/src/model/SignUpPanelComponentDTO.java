package model;

import javax.swing.*;

public class SignUpPanelComponentDTO {
    private JTextField newUserName;
    private JTextField newUserID;
    private JPasswordField newUserPassword;
    private JPasswordField newUserPasswordCheck;
    private JTextField newUserBirthDay;
    private JTextField newUserEmail;
    private JTextField newUserPhoneNumber;
    private JTextField newUserAddress;
    private JTextField newUserZipCode;
    private JButton addressSearchingButton;
    private JButton signUpButton;
    //getter
    public JTextField getNewUserName(){
        return newUserName;
    }
    public JTextField getNewUserID(){
        return newUserID;
    }
    public JPasswordField getNewUserPassword(){
        return newUserPassword;
    }
    public JPasswordField getNewUserPasswordCheck(){
        return newUserPasswordCheck;
    }
    public JTextField getNewUserBirthDay(){
        return newUserBirthDay;
    }
    public JTextField getNewUserEmail(){
        return newUserEmail;
    }
    public JTextField getNewUserPhoneNumber(){
        return newUserPhoneNumber;
    }
    public JTextField getNewUserAddress(){
        return newUserAddress;
    }
    public JTextField getNewUserZipCode(){
        return newUserZipCode;
    }
    public JButton getAddressSearchingButton(){
        return addressSearchingButton;
    }
    public JButton getSignUpButton(){
        return signUpButton;
    }

    //setter
    public void setNewUserName(JTextField newUserName){
        this.newUserName = newUserName;
    }
    public void setNewUserID(JTextField newUserID){
        this.newUserID = newUserID;
    }
    public void setNewUserPassword(JPasswordField newUserPassword){
        this.newUserPassword = newUserPassword;
    }
    public void setNewUserPasswordCheck(JPasswordField newUserPasswordCheck){
        this.newUserPasswordCheck = newUserPasswordCheck;
    }
    public void setNewUserBirthDay(JTextField newUserBirthDay){
        this.newUserBirthDay = newUserBirthDay;
    }
    public void setNewUserEmail(JTextField newUserEmail){
        this.newUserEmail = newUserEmail;
    }
    public void setNewUserPhoneNumber(JTextField newUserPhoneNumber){
        this.newUserPhoneNumber = newUserPhoneNumber;
    }
    public void setNewUserAddress(JTextField newUserAddress){
        this.newUserAddress = newUserAddress;
    }
    public void setNewUserZipCode(JTextField newUserZipCode){
        this.newUserZipCode = newUserZipCode;
    }
    public void setAddressSearchingButton(JButton addressSearchingButton){
        this.addressSearchingButton = addressSearchingButton;
    }
    public void setSignUpButton(JButton signUpButton){
        this.signUpButton = signUpButton;
    }
}
