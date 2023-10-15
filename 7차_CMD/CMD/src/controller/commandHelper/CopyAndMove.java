package controller.commandHelper;

import model.PathDTO;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;
import view.CMDUI;

import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class CopyAndMove {
    protected DesktopInformation desktopInformation;
    protected ExceptionHandling exceptionHandling;

    public CopyAndMove(ExceptionHandling exceptionHandling){
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = new ExceptionHandling();
    }

    protected List<String> checkAndFindValidationPathAndFileInInputCommand(String pathRemovedHeadAndTailWhiteSpace, String currentPath){ //입력받은 명령어 문장 유효성 check하기
        String[] splittedInputCommand;
        List<String> validPathList = new ArrayList<String>();
        String potentiallyValidPath;
        int count = 0;

        //1. 공백을 기준으로 문자열 나누기
        pathRemovedHeadAndTailWhiteSpace = pathRemovedHeadAndTailWhiteSpace.replaceAll("\\s{2,}"," "); //공백이 2개이상 연속으로 나올때 공백하나로 바꾸기
        splittedInputCommand = pathRemovedHeadAndTailWhiteSpace.split(" ");
        for(int i=0; i<splittedInputCommand.length; i++){
            validPathList.add(splittedInputCommand[i]);
        }

        //2. 나눠진 문자열들 앞뒤 합쳐서 유효한 주소이면 합치기 (디렉토리명이나 파일명에 공백이 포함되어 있을 경우를 찾기 위해 시행)
        while(count < validPathList.size() - 1){
            potentiallyValidPath = validPathList.get(count) + " " + validPathList.get(count + 1);
            if(desktopInformation.checkPathExists(potentiallyValidPath)){
                validPathList.set(count,potentiallyValidPath);
                validPathList.remove(count + 1);
                count = 0;
            }
            else if(!desktopInformation.checkPathExists(potentiallyValidPath) && (count >= 1)){ //2번째 경로 이상인 경우
                if(desktopInformation.checkPathExists(desktopInformation.getParentPath(potentiallyValidPath))){ //부모 경로가 있는 경우
                    validPathList.set(count,potentiallyValidPath);
                    validPathList.remove(count + 1);
                    count = 0;
                }
            }
            else count++;
        }

        //3. 상대경로인 경우(즉 , c:\으로 시작하지 않는 경우 상대경로 이므로 현재 경로랑 합쳐주기)
        for(int i=0; i<validPathList.size(); i++){
            if(!validPathList.get(i).startsWith("c:\\")){ //상대경로인 경우 확인하기
                validPathList.set(i, currentPath + "\\" +validPathList.get(i));
            }
        }

        //4.리스트가 1개, 2개가 아닌 경우 처리 올바르지 않은 입력
        if(validPathList.size() < 1 || validPathList.size() > 2){
            //하나라도 올바르지 않은 경우 모든 원소 지우기
            validPathList.clear();
            return validPathList;
        }

        //5. 리스트에 저장된 0번째 디렉토리의 경로가 올바른지 확인하기
        if (!desktopInformation.checkPathExists(validPathList.get(0))) {
            validPathList.clear();
            return validPathList;
        }

        return validPathList;
    }

    protected PathDTO distinguishCopyAndMoveCase(List<String> vaildPaths, String currentPath){
        PathDTO PathDTO = new PathDTO();

        //리스트 원소가 1개인 경우 > 해당 경로의 파일을 현재 디렉토리에 붙여넣기 혹은 이동하기(예 :copy c:\\users\\사용자명\\desktop\\abc.txt)
        if(vaildPaths.size() == 1){
            //1. 파일명인 경우
            if(!vaildPaths.get(0).contains("\\") && !vaildPaths.get(0).contains("/")){
                PathDTO.setFirstFilePath(currentPath + "\\" + vaildPaths.get(0));
                PathDTO.setSecondFilePath(currentPath + "\\" + Paths.get(PathDTO.getFirstFilePath()).getFileName().toString()); //두번째 경로에 파일경로 추가
                return PathDTO;
            }
            //2. 파일경로인 경우
            PathDTO.setFirstFilePath(vaildPaths.get(0));
            PathDTO.setSecondFilePath(currentPath + "\\" + Paths.get(PathDTO.getFirstFilePath()).getFileName().toString()); //두번째 경로에 파일경로 추가
            return PathDTO;
        }

        //리스트 원소가 2개인 경우
        else if(vaildPaths.size() == 2){
            // 원소 두개가 각각 경로로 표현된 파일인지, 그냥 파일명인지로 총 4가지로 구분
            // 1. 둘다 파일명만 있는 경우 > 현재경로에 있는 파일을 현재경로로 붙여넣기 혹은 이동하기
            if(!vaildPaths.get(0).contains("\\") && !vaildPaths.get(0).contains("/") && !vaildPaths.get(1).contains("\\") && !vaildPaths.get(1).contains("/")){
                PathDTO.setFirstFilePath(currentPath + "\\" + vaildPaths.get(0));
                PathDTO.setSecondFilePath(currentPath + "\\" + vaildPaths.get(1));
                return PathDTO;
            }
            // 2. 둘다 파일 경로인 경우  > 앞의 경로에 있는 파일을 뒤의 경로에 있는 파일에 붙여넣기 혹은 이동하기
            else if((vaildPaths.get(0).contains("\\") || vaildPaths.get(0).contains("/")) && (vaildPaths.get(1).contains("\\") || vaildPaths.get(1).contains("/"))){
                PathDTO.setFirstFilePath(vaildPaths.get(0));
                PathDTO.setSecondFilePath(vaildPaths.get(1));
                return PathDTO;
            }
            // 3. 앞의 원소는 파일 경로이고 뒤의 원소는 파일명인 경우
            else if((vaildPaths.get(0).contains("\\") || vaildPaths.get(0).contains("/")) && (!vaildPaths.get(1).contains("\\") && !vaildPaths.get(1).contains("/"))){
                PathDTO.setFirstFilePath(vaildPaths.get(0));
                PathDTO.setSecondFilePath(currentPath + "\\" + vaildPaths.get(1));
                return PathDTO;
            }
            // 4. 앞의 원소는 파일명이고 뒤의 원소는 파일 경로인 경우
            else if((!vaildPaths.get(0).contains("\\") && !vaildPaths.get(0).contains("/")) && (vaildPaths.get(1).contains("\\") || vaildPaths.get(1).contains("/"))){
                PathDTO.setFirstFilePath(currentPath + "\\" + vaildPaths.get(0));
                PathDTO.setSecondFilePath(vaildPaths.get(1));
                return PathDTO;
            }
        }
        return PathDTO;
    }

    protected boolean handleFileOverWrite(Path sourcePath, Path targePath) {
        Boolean isCorrectAnswer = false;
        String fileName;

        //질문 출력을 위해 파일 이름 받기
        if (Files.isRegularFile(targePath)) {
            fileName = targePath.getFileName().toString();
        } else { //경로 하나만 입력 받았을 시 > 그 경로에서 파일 이름 가져오기
            fileName = sourcePath.getFileName().toString();
        }

        while (!isCorrectAnswer) {
            CMDUI.printQuestion(fileName + Constants.ASK_FILE_OVER_WRITE);
            //응답 입력 받기
            Scanner scan = new Scanner(System.in);
            String answer = scan.nextLine();

            //소문자로 바꾸기
            answer = answer.toLowerCase();
            answer.trim();
            if (answer.equals("yes")) {
                return true;
            } else if (answer.equals("no")) {
                return false;
            }
        }
        return true;
    }
}
