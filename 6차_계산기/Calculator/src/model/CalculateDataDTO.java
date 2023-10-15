package model;

public class CalculateDataDTO {
    private String savedNumber= "";
    private String firstNumber = "0";
    private String secondNumber = "";
    private String operator = "";
    private Boolean isEqualExist = false;

    public String getSavedNumber(){
         return savedNumber;
    }
    public void setSavedNumber(String value){
        savedNumber = value;
    }

    public String getFirstNumber(){
        return firstNumber;
    }
    public void setFirstNumber(String value){
        firstNumber = value;
    }

    public String getSecondNumber(){
        return secondNumber;
    }
    public void setSecondNumber(String value){
        secondNumber = value;
    }

    public String getOperator(){
        return operator;
    }
    public void setOperator(String value){
        operator = value;
    }

    public Boolean getIsEqualExist(){
        return isEqualExist;
    }
    public void setIsEqualExist(boolean value){
        isEqualExist = value;
    }

}
