package utility;

public class ExceptionHandling {
    public String optimizeStringForJudge(String inputSentence){
        String patternOptimizedString; //명령어 판단하기 위해 문자열 조정하기

        patternOptimizedString = inputSentence.toLowerCase(); // 소문자로 변환하기
        patternOptimizedString = patternOptimizedString.trim(); //명령어 앞,뒤쪽 공백제거

        //최적화된 문자열 반환
        return patternOptimizedString;
    }
    public String optimizeStringRemoveCommand(String inputCommandLine, int commandLength, boolean isWhitespaceRemove){
        inputCommandLine = inputCommandLine.substring(commandLength); //  명령어 지우기
        if(isWhitespaceRemove) inputCommandLine = inputCommandLine.trim(); //참인경우에만 앞뒤 공백 제거
        return inputCommandLine;
    }

    //공백이 포함된 경로인 경우 이 경로가 올바른지 판단
    public Boolean judgeWhiteSpaceContainedPathValidation(String inputPath){
        inputPath = inputPath.replaceAll("\\s{2,}"," "); //공백이 2개이상 연속으로 나올때 공백하나로 바꾸기
        String[] directoryArray = inputPath.split(" ");

        for (String directory : directoryArray) {
            // 경로에 디렉토리가 한묶음씩 슬래시와 함께 묶여있으면 인식 (예  /users  /junch)
            if( !(directory.startsWith("\\")) && !(directory.startsWith("/")) ){
                return !(Constants.IS_Valid_Path);
            }
        }
        return Constants.IS_Valid_Path;
    }


}
