package controller;

import view.CalculatorFrame;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;
import java.text.DecimalFormat;

public class ButtonEvent {

    public CalculatorFrame calculatorFrame;

    public ButtonEvent(CalculatorFrame calculatorFrame) {  //생성자에서 Frame 객체의 필드값 수정하기 위해 가져오기
        this.calculatorFrame = calculatorFrame;
    }

    //1. 숫자 버튼 클릭 시 처리하는 ActionListener
    public class NumberButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            JButton numberButton = (JButton) e.getSource(); //버튼 가져오기

            if(calculatorFrame.isLogExpression){ // 로그에서 데이터 받아와서 숫자 입력 했을때

            }
            if (calculatorFrame.isEqualExist) { // 이전에 equal이 입력 되었을 경우 > 새로운 연산이 시작되므로 모든 값 초기화 해준다
                calculatorFrame.savedNumber = "";
                calculatorFrame.firstNumber = "0";
                calculatorFrame.secondNumber = "";
                calculatorFrame.operator = ""; //입력 받은 연산자
                calculatorFrame.isEqualExist = false;
                calculatorFrame.preNumberLabel.setText("");
            }

            if (calculatorFrame.operator == "") { //연산자가 없을 때 숫자1 입력 받기
                //숫자 제한개수 초과시 입력 안되게 막기 (쉼표 제거 후 숫자 개수 세기)
                if(!judgeInputLengthLimit(calculatorFrame.firstNumber)) return;
                //숫자 입력 전 숫자1이 0이면 지워주기
                if (calculatorFrame.firstNumber == "0") {
                    calculatorFrame.firstNumber = "";
                    clearInputNumber();
                }
                printNumberAndErrorMessage(calculatorFrame.numberInputLabel.getText() + numberButton.getText());
                calculatorFrame.firstNumber = calculatorFrame.numberInputLabel.getText();
            }

            else { // 연산자가 있을 때 숫자2 입력받기
                //숫자 제한개수 초과시 입력 안되게 막기 (쉼표 제거 후 숫자 개수 세기)
                if(!judgeInputLengthLimit(calculatorFrame.secondNumber)) return;
                //숫자 입력 전 숫자2가 0이거나 입력 받은 값이 없다면 지워주기
                if ((calculatorFrame.secondNumber == "0") || (calculatorFrame.secondNumber == "")) {
                    calculatorFrame.secondNumber = "";
                    clearInputNumber();
                }
                printNumberAndErrorMessage(calculatorFrame.numberInputLabel.getText() + numberButton.getText());
                calculatorFrame.secondNumber = calculatorFrame.numberInputLabel.getText();
            }

            saveFirstNumberToSavedNumber(calculatorFrame.firstNumber);
            calculatorFrame.requestFocus();
        }
    }

    //2. 연산자 버튼 클릭 시 처리하는 ActionListener
    public class OperatorButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            JButton operatorButton = (JButton) e.getSource(); //버튼 가져오기
            String operator = operatorButton.getText();

            if (calculatorFrame.isEqualExist) { // 이전에 equal이 입력 되었을 경우 > equal이 입력되고 그 결과 값에 다시 연산을 하는 경우
                calculatorFrame.savedNumber = "";
                calculatorFrame.firstNumber = calculatorFrame.numberInputLabel.getText();
                calculatorFrame.secondNumber = "";
                calculatorFrame.operator = ""; //입력 받은 연산자
                calculatorFrame.isEqualExist = false;
                calculatorFrame.preNumberLabel.setText("");
            }

            //연산자가 들어오면 앞서 입력된 문자열형의 숫자 정리해주기 (예:0123 > 123)
            calculatorFrame.firstNumber = removeUnnecessaryZero(calculatorFrame.firstNumber);
            calculatorFrame.secondNumber = removeUnnecessaryZero(calculatorFrame.secondNumber);
            printNumberAndErrorMessage(removeUnnecessaryZero(calculatorFrame.numberInputLabel.getText())); // 입력한 값이 2.000과 같은 경우 불필요한 소수점 제거해 주기

            //앞서 연산자가 없는 경우 연산자 추가
            if (calculatorFrame.operator == "") {
                calculatorFrame.operator = operator;
                printExpression(calculatorFrame.firstNumber + calculatorFrame.operator);
            } else { // 앞서 연산자가 있는경우 또 연산자가 나왔을때
                if (calculatorFrame.secondNumber == "") { //숫자2가 안나왔을때 > 즉, 숫자1 나오고 연산자가 두번 이상 연속으로 나왔을 경우
                    //연산자 교체
                    calculatorFrame.operator = operator;
                    printExpression(calculatorFrame.firstNumber + calculatorFrame.operator);
                } else { //앞서 숫자1, 연산자, 숫자2 가 나왔고 현재 연산자 버튼을 눌렀을 경우 > 앞의 수식의 계산을 진행
                    calculateNumbers(calculatorFrame.operator); // 기존의 연산자로 계산을 진행
                    calculatorFrame.secondNumber = ""; //숫자2 초기화
                    calculatorFrame.operator = operator; //계산이 끝난 후 새로 입력받은 연산자를 입력
                    printNumberAndErrorMessage(calculatorFrame.firstNumber);

                    if (calculatorFrame.firstNumber.matches(".*[ㄱ-ㅎㅏ-ㅣ가-힣]+.*")) { //오류 메시지인 경우
                        printNumberAndErrorMessage(calculatorFrame.firstNumber); // 오류메시지 출력
                        //전부 초기화
                    }
                    else {
                        printExpression(calculatorFrame.firstNumber + calculatorFrame.operator);
                    }
                }
            }
            saveFirstNumberToSavedNumber(calculatorFrame.firstNumber);
            calculatorFrame.requestFocus();
        }
    }

    //3. Equal 버튼 클릭시 처리하는 ActionListener
    public class EqualButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            //equal값 들어왔는지 확인하는 변수
            calculatorFrame.isEqualExist = true;
            //Equal값이 들어오면 앞서 입력된 문자열형의 숫자 정리해주기 (예:0123 > 123)
            calculatorFrame.firstNumber = removeUnnecessaryZero(calculatorFrame.firstNumber);
            calculatorFrame.secondNumber = removeUnnecessaryZero(calculatorFrame.secondNumber);

            if (calculatorFrame.operator == "") { //앞서 연산자가 나오지 않은 경우 > 숫자2도 없다.
                printExpression(calculatorFrame.firstNumber + "=");
                addLogOnLogPanel(calculatorFrame.firstNumber + "=/" + calculatorFrame.firstNumber);
            }
            else { //앞서 연산자가 나온 경우

                if (calculatorFrame.secondNumber == "") { //숫자2가 나오지 않은 경우 (예: 1+=) > 자기 복제
                    calculatorFrame.secondNumber = calculatorFrame.savedNumber;
                    printExpression(calculatorFrame.firstNumber + calculatorFrame.operator + calculatorFrame.savedNumber + "=");

                    //계산
                    calculateNumbers(calculatorFrame.operator);
                    printNumberAndErrorMessage(calculatorFrame.firstNumber);
                    calculatorFrame.secondNumber = "";
                } else { // 숫자1, 연산자, 숫자2 그리고 현재 등호가 나온 경우 > 정상 계산
                    //히스토리 창 출력
                    printExpression(calculatorFrame.firstNumber + calculatorFrame.operator + calculatorFrame.secondNumber + "=");
                    //수식 계산
                    calculateNumbers(calculatorFrame.operator);
                    //입력 창 출력
                    printNumberAndErrorMessage(calculatorFrame.firstNumber);
                    saveFirstNumberToSavedNumber(calculatorFrame.firstNumber);
                }
            }
            calculatorFrame.requestFocus();
        }
    }

    //4. C버튼 눌렀을 때 이벤트 처리 > 모두 초기화
    public class ClearButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            //모든 변수 초기화
            calculatorFrame.savedNumber = "";
            calculatorFrame.firstNumber = "0";
            calculatorFrame.secondNumber = "";
            calculatorFrame.operator = ""; //입력 받은 연산자
            calculatorFrame.isEqualExist = false;

            //버튼 모두 활성화
            for(int i=0; i<20; i++) {
                calculatorFrame.calculatebuttons[i].setEnabled(true);
            }

            //출력 창 초기화
            calculatorFrame.preNumberLabel.setText("");
            printNumberAndErrorMessage("0");
            calculatorFrame.requestFocus();
        }
    }

    //5. 소수점 버튼 눌렀을 경우 이벤트 처리
    public class DecimalPointButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            if(calculatorFrame.firstNumber.equals("0") && calculatorFrame.operator.equals("")){ //첫번째 숫자 입력시 초기값이 0이면
                calculatorFrame.firstNumber = "0.";
            }

            if(!calculatorFrame.operator.equals("") && calculatorFrame.secondNumber.equals("")){ //연산자 입력 받고 두번째 숫자 입력 하나도 안받았을 때
                calculatorFrame.secondNumber = "0.";
                printNumberAndErrorMessage("0.");
            }

            if(calculatorFrame.isEqualExist){ //등호 입력 이후 연산 처리시 소수점이 제일 먼저 들어올 때
                calculatorFrame.firstNumber = "0.";
                calculatorFrame.secondNumber = "";
                calculatorFrame.operator = ""; //입력 받은 연산자
                calculatorFrame.isEqualExist = false;
                printExpression("");
                printNumberAndErrorMessage("0.");

            }

            if (!(calculatorFrame.numberInputLabel.getText()).contains(".")) {
                printNumberAndErrorMessage(calculatorFrame.numberInputLabel.getText() + ".");
            }

            calculatorFrame.requestFocus();
        }
    }

    //6. BackSpace버튼 인 경우
    public class BackSpaceButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            String inputNumbers;

            if (calculatorFrame.isEqualExist) { //등호가 들어왔을 경우
                //등호와 연산자가 있다는 의미는 정상적인 수식계산이 진행 되었다는 의미 > 이 경우 backspace를 누르면 history 창이 지워진다.
                if (calculatorFrame.operator != "") {
                    calculatorFrame.preNumberLabel.setText("");
                }
                // operator가 "" 인 경우 > 숫자 누르고 = 누른 후 backspace 눌렀을 경우 (연산자가 없고 =이 들와왔을 경우) 는 아무일도 일어나지 않는다.
            } else { //등호가 없는 경우
                if ((calculatorFrame.operator == "") && (calculatorFrame.firstNumber != "0")) { //숫자1 입력 도중
                    inputNumbers = calculatorFrame.numberInputLabel.getText();
                    calculatorFrame.firstNumber = judgeDeletedNumberLength(inputNumbers);
                    printNumberAndErrorMessage(calculatorFrame.firstNumber); //화면 출력
                } else if ((calculatorFrame.operator != "") && (calculatorFrame.secondNumber != "")) { //숫자2 입력도중
                    inputNumbers = calculatorFrame.numberInputLabel.getText();
                    calculatorFrame.secondNumber = judgeDeletedNumberLength(inputNumbers);
                    printNumberAndErrorMessage(calculatorFrame.secondNumber); //화면 출력
                }
            }
            saveFirstNumberToSavedNumber(calculatorFrame.firstNumber);
            calculatorFrame.requestFocus();
        }
    }

    //7. plusAndMinus버튼 인 경우
    public class PlusAndMinusButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            //연산자 없을 경우 > 즉 숫자1 입력 도중

            //숫자 1 입력 도중
            if (calculatorFrame.operator.equals("") && !calculatorFrame.isEqualExist) {
                if (!(calculatorFrame.firstNumber.equals("0"))) { // 0인 경우 - 붙으면 안됨
                    setMinusOnNumberInputLabel();
                    calculatorFrame.firstNumber = calculatorFrame.numberInputLabel.getText(); //바뀐값 변수에 저장
                }
                else{
                    printExpression("negate(" + calculatorFrame.preNumberLabel.getText() + ")");
                }
            }
            //숫자1과 연산자가 들어 왔을 경우
            else if (!calculatorFrame.operator.equals("") && calculatorFrame.secondNumber.equals("") && !calculatorFrame.isEqualExist) {
                setNegateOnPreNumberLabelAfterOperatorInput(); //preNumber라벨 창 출력 관리 함수
                setMinusOnNumberInputLabel(); // 입력창 출력 관리 함수
                calculatorFrame.firstNumber = calculatorFrame.numberInputLabel.getText();//바뀐값 변수에 저장
            }
            //두번째 숫자 입력시
            else if (!calculatorFrame.operator.equals("") && !calculatorFrame.secondNumber.equals("") && !calculatorFrame.isEqualExist) {
                setMinusOnNumberInputLabel();
                calculatorFrame.secondNumber = calculatorFrame.numberInputLabel.getText(); //바뀐값 변수에 저장
            }
            //연산자 다음 바로 등호 들어왔을 경우 > 자기복제 수식 계산 끝난 후
            else if (!calculatorFrame.operator.equals("") && calculatorFrame.secondNumber.equals("") && calculatorFrame.isEqualExist) {

            }
            //숫자1, 연산자, 숫자2, 등호 들어왔을 경우 > 수식계산 끝난 후
            else if (!calculatorFrame.operator.equals("") && !calculatorFrame.secondNumber.equals("") && calculatorFrame.isEqualExist) {
                setNegateOnPreNumberLabelAfterAllCalculationOver();
                setMinusOnNumberInputLabel();
                calculatorFrame.firstNumber = calculatorFrame.numberInputLabel.getText();//바뀐값 변수에 저장
            }
            saveFirstNumberToSavedNumber(calculatorFrame.firstNumber);
            calculatorFrame.requestFocus();
        }
    }

    //8. CE 버튼인 경우
    public class ClearEntryButtonEventListenerClass implements ActionListener {
        public void actionPerformed(ActionEvent e) {
            if (calculatorFrame.operator.equals("") && !calculatorFrame.isEqualExist) { //숫자1 입력도중
                printNumberAndErrorMessage("0");
                calculatorFrame.firstNumber = "0";
            }
            //숫자1과 연산자 까지 들어왔을때
            else if (!calculatorFrame.operator.equals("") && calculatorFrame.secondNumber.equals("") && !calculatorFrame.isEqualExist) {
                printNumberAndErrorMessage("0");
                calculatorFrame.secondNumber ="0";
            }
            //두번째 숫자 입력시
            else if (!calculatorFrame.operator.equals("") && !calculatorFrame.secondNumber.equals("") && !calculatorFrame.isEqualExist) {
                printNumberAndErrorMessage("0");
                calculatorFrame.secondNumber = "0";
            }
            //등호 입력시, 즉 계산 완료 후
            else if (calculatorFrame.isEqualExist) {
                calculatorFrame.savedNumber = "";
                calculatorFrame.firstNumber = "0";
                calculatorFrame.secondNumber = "";
                calculatorFrame.operator = ""; //입력 받은 연산자
                calculatorFrame.isEqualExist = false;

                //출력 창 초기화
                calculatorFrame.preNumberLabel.setText("");
                printNumberAndErrorMessage("0");
            }
            saveFirstNumberToSavedNumber(calculatorFrame.firstNumber);
            calculatorFrame.requestFocus();
        }
    }

    public class LogHistoryButtonEventListenerClass implements ActionListener{
        public void actionPerformed(ActionEvent e) {
            calculatorFrame.isLogExpression = true;
            JButton logHistory = (JButton) e.getSource();

            //버튼 택스트의 html 태그 제거 및 필요한 텍스트 추출하기
            String logHistoryData = logHistory.getText().replaceAll("<html><div style=\"text-align: right;\">","");
            logHistoryData = logHistoryData.replaceAll("<br><font size=\"5\"><b>","");
            logHistoryData = logHistoryData.replaceAll("</b></font></div></html>","");
            String[] logHistoryDataList = logHistoryData.split("=");

            //출력하기
            printExpression(logHistoryDataList[0] + "=");
            printNumberAndErrorMessage(logHistoryDataList[1]);

        }
    }


    //pulsAndMinus 버튼 눌렀을때 입력창 출력하는 함수
    public void setMinusOnNumberInputLabel() {
        String inputLabelText = calculatorFrame.numberInputLabel.getText();
        if (inputLabelText.contains("-")) {
            inputLabelText = inputLabelText.replace("-", "");
            printNumberAndErrorMessage(inputLabelText);
        } else {
            printNumberAndErrorMessage("-" + calculatorFrame.numberInputLabel.getText());
        }
    }

    //pulsAndMinus 버튼 눌렀을때 preNumberLabel에 negate 출력하는 함수
    public void setNegateOnPreNumberLabelAfterOperatorInput() {
        String preNumberLabelText = calculatorFrame.preNumberLabel.getText();
        int numberLastIndex = findNumbersIndexOnPreNumberLabel(preNumberLabelText); //입력한 숫자의 마지막 인덱스 찾기
        String targetNumber = preNumberLabelText.substring(0, numberLastIndex); //문자열에서 숫자 추출하기

        if (!preNumberLabelText.contains("negate")) { //히스토리창에 negate가 아직 출력되지 않았을 때 > 즉 plusAndMinus버튼이 처음 눌렸을 경우
            printExpression(preNumberLabelText + "negate( " + targetNumber + " )");
        } else { //plusAndMinus버튼이 두번 이상 눌렸을때
            //문자열 나누기
            String expression = preNumberLabelText.substring(0, numberLastIndex + 1);
            String preNegateText = preNumberLabelText.substring(numberLastIndex + 1);

            //preNumberLabel 출력
            printExpression(expression + " negate( " + preNegateText + " )");
        }
    }

    public void setNegateOnPreNumberLabelAfterAllCalculationOver() {
        String preNumberLabelText = calculatorFrame.preNumberLabel.getText();
        if (!preNumberLabelText.contains("negate")) { //히스토리창에 negate가 아직 출력되지 않았을 때 > 즉 plusAndMinus버튼이 처음 눌렸을 경우
            printExpression("negate( " + calculatorFrame.numberInputLabel.getText() + " )");
        } else {
            printExpression("negate( " + preNumberLabelText + " )");
        }
    }

    public int findNumbersIndexOnPreNumberLabel(String preNumberLabelText) {
        String[] operator = {"+", "-", "÷", "x"};
        int stringNumberLastIndex = -1;

        for (int i = 0; i < 4; i++) {
            stringNumberLastIndex = preNumberLabelText.indexOf(operator[i]);
            if (stringNumberLastIndex != -1) {
                return stringNumberLastIndex;
            }
        }
        return stringNumberLastIndex;
    }

    public void printNumberAndErrorMessage(String number){
        String withoutPeriodAndMinus;

        //입력창에 초기 출력이 0인 경우
        if(calculatorFrame.numberInputLabel.getText() == "0"){
            clearInputNumber();
        }

        if(number.matches( ".*[ㄱ-ㅎㅏ-ㅣ가-힣]+.*")){ //오류메시지인 경우
            setFontSize(number);
            calculatorFrame.numberInputLabel.setText(number);
            return;
        }

        if(number.contains("E")){
            number = number.replace("E", "e");
        }
        else {
            number = addCommasOnLabel(number);
            number = number.replace(",", "");
            withoutPeriodAndMinus = number.replaceAll("-","");
            withoutPeriodAndMinus = number.replaceAll(".","");

            if (withoutPeriodAndMinus.length() >= 17) {
                BigDecimal bigDecimal = new BigDecimal(number);
                number = bigDecimal.toString();
            }
            else {
                number = addCommasOnLabel(number);
            }
        }

        //출력전 사이즈 조절
        setFontSize(number);
        calculatorFrame.numberInputLabel.setText(number); //결과 출력
    }

    public void setFontSize(String numberOrText){
        //출력전 사이즈 조절
        int fontSize = calculatorFrame.numberInputLabel.getHeight();
        Font numberInputLabelFont = new Font("나눔고딕", Font.BOLD, fontSize); //초기 폰트 값 저장
        FontMetrics numberInputLabelFontMetrics = calculatorFrame.numberInputLabel.getFontMetrics(numberInputLabelFont);
        int numberWidth = numberInputLabelFontMetrics.stringWidth(numberOrText);

        if(calculatorFrame.numberInputLabel.getWidth() < numberWidth){
            while(calculatorFrame.numberInputLabel.getWidth() * 3.8 /4 < numberWidth) {
                fontSize = fontSize - 1;
                numberInputLabelFont = new Font("나눔고딕", Font.BOLD, fontSize); //초기 폰트 값 저장
                numberInputLabelFontMetrics = calculatorFrame.numberInputLabel.getFontMetrics(numberInputLabelFont);
                numberWidth = numberInputLabelFontMetrics.stringWidth(numberOrText);
                calculatorFrame.numberInputLabel.setFont(new Font("나눔고딕", Font.BOLD, fontSize));
            }
        }
        else{
            calculatorFrame.numberInputLabel.setFont(new Font("나눔고딕", Font.BOLD, calculatorFrame.numberInputLabel.getHeight()));
        }
    }

    //출력되는 숫자에 천단위로 , 추가하는 함수
    public String addCommasOnLabel(String number){
        //정수형 숫자의 경우 천 단위마다 쉼표 찍기
        if( !(number.contains(".")) ) { //정수인 경우에만 쉼표 찍기
            //앞서 출력된 모든 쉼표 제거
            number = number.replace(",", "");
            BigDecimal bigDecimalNumber = new BigDecimal(number);
            DecimalFormat decimalFormat = new DecimalFormat("###,###");
            String formattedNumber = decimalFormat.format(bigDecimalNumber);
            number = formattedNumber;
        }
        return number;
    }

    public void clearInputNumber(){
        calculatorFrame.numberInputLabel.setText("");
    }

    public String removeUnnecessaryZero(String number) { //string 값으로 저장된 숫자 중에 맨앞이나 맨뒤에 불필요한 0을 삭제해주기 위한 함수
        // string > Big Decimal > string 으로 바꿔준다
        String withoutPeriodAndMinus = number;
        BigDecimal bigDecimalNumber;

        //문자열에 저장된 , 제거
        number = removeCommasInNumber(number);

        //문자열이 빈 문자열이 아닐 경우에만 수행
        if(number != "") {
            bigDecimalNumber = new BigDecimal(number);
            //숫자가 16자리 넘어갈때만 지수표현식으로 표기
            withoutPeriodAndMinus = number.replace("-","");
            withoutPeriodAndMinus = number.replace(".","");

            if(withoutPeriodAndMinus.length() >= 17 || number.contains("E")) return bigDecimalNumber.stripTrailingZeros().toString(); //지수표현식으로 출력
            else return bigDecimalNumber.stripTrailingZeros().toPlainString(); //지수표현식 적용X
        }
        //빈 문자열일 경우 그대로 반환
        return number;
    }

    public String removeCommasInNumber(String number){
        //문자열에 저장된 , 제거
        number = number.replaceAll(",","");
        return number;
    }

    public void printExpression(String expression){
        if(expression.contains("E")){
            expression = expression.replaceAll("E","e");
        }
        calculatorFrame.preNumberLabel.setText(expression);
    }

    public void calculateNumbers(String operator){ //연산 함수
        String logText = "";
        String result = "";

        BigDecimal overFlowStandard = new BigDecimal("1.0E+10000");

        BigDecimal bigDecimalNumber1 = new BigDecimal(calculatorFrame.firstNumber);
        BigDecimal bigDecimalNumber2 = new BigDecimal(calculatorFrame.secondNumber);

        BigDecimal bigDecimalresult;


        switch (operator){
            case "+":
                result = (bigDecimalNumber1.add(bigDecimalNumber2, MathContext.DECIMAL64)).toString();
                break;
            case "-":
                result = (bigDecimalNumber1.subtract(bigDecimalNumber2, MathContext.DECIMAL64)).toString();
                break;
            case "x":
                result = (bigDecimalNumber1.multiply(bigDecimalNumber2, MathContext.DECIMAL64)).toString();
                break;
            case "÷":
                result = handleDivisionException(bigDecimalNumber1 ,bigDecimalNumber2);
                break;
        }
        //지수표현식
        if(result.contains("E")) {
            int indexOfE = result.indexOf("E");
            String exponent = result.substring(indexOfE + 2);
            if (exponent.length() >= 5) {
                printExpression(""); // history 창 삭제
                result = "오버플로우";
                calculatorFrame.firstNumber = result;

                //오버플로우 이후 버튼 비활성화
                for(int i=0; i<20; i++) {
                    if(i != 1) { //C버튼 제외하고 비활성화
                        calculatorFrame.calculatebuttons[i].setEnabled(false);
                    }
                }
                return;
            }
        }
        logText = calculatorFrame.firstNumber + calculatorFrame.operator + calculatorFrame.secondNumber + "=/" + result;
        addLogOnLogPanel(logText);
        calculatorFrame.firstNumber = result;

    }

    public String handleDivisionException(BigDecimal bigDecimalNumber1, BigDecimal bigDecimalNumber2){
        String result;

        //분모가 0인 경우 계산이 되지 않음
        if(bigDecimalNumber2.compareTo(BigDecimal.ZERO) == 0){
            if(bigDecimalNumber1.compareTo(BigDecimal.ZERO) == 0){
                result = "정의되지 않은 결과입니다";
            }
            else{
                result = "0으로 나눌 수 없습니다";
            }
        }
        //정상적인 계산인 경우
        else {
            result = (bigDecimalNumber1.divide(bigDecimalNumber2, 15, RoundingMode.HALF_UP)).toString();
        }
        return result;
    }

    public void saveFirstNumberToSavedNumber(String firstNumber){
        //모든 버튼 이벤트가 끝날때마다 현재 calculatorFrame.number1를 저장하기
        calculatorFrame.savedNumber = firstNumber;
    }

    //BackSpace 사용하여 입력 문자열을 지울때 길이 판단하고 길이에 따라 조건 나누기
    public String judgeDeletedNumberLength(String inputNumbers){
        String removedNumber; //backSpace 적용된 숫자

        if ( inputNumbers.length() <= 1 ){ //입력된 모든 숫자를 다 지웠을 경우 0 출력
            removedNumber = "0";
        }
        else{
            removedNumber = inputNumbers.substring(0, inputNumbers.length() - 1); //맨 뒤 숫자 제거
        }
        return removedNumber;
    }

    //숫자 제한개수 초과시 입력 안되게 막기 (쉼표 제거 후 숫자 개수 세기)
    public Boolean judgeInputLengthLimit(String number){
        Boolean isInputPossible = true;
        //소수점인지 판단
        if(number.contains(".")){
            //정수부가 0일 경우 > 소수점 제외하고 17자리 입력가능
            if(String.valueOf(number.charAt(0)) == "0"){
                if(number.length() > 17) isInputPossible = false;
            }
            else{
                if(number.length() > 16) isInputPossible = false;
            }
        }
        else{
            if ((removeCommasInNumber(number)).length() > 15) isInputPossible = false;
        }

        return isInputPossible;
    }

    public void addLogOnLogPanel(String logText){
        //로그 기록 리스트에 저장
        //로그 기록이 20가 넘어가면 맨 처음 로그 삭제
        if(calculatorFrame.logList.size() >= 20){
            calculatorFrame.logList.remove(0);
        }
        calculatorFrame.logList.add(logText);
        calculatorFrame.composeLogBasePanel(); // 로그 패널 위에 logBase패널 올리기
    }
}
