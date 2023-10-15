package controller;

import controller.command.*;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;
import view.CMDUI;

import java.util.Scanner;

public class CMDStart {
    public DesktopInformation desktopInformation;
    public ExceptionHandling exceptionHandling;

    //명령어 처리 클래스
    public ChangeDirectoryCommand changeDirectoryCommand;
    public DirectoryCommand directoryCommand;
    public HelpCommand helpCommand;
    public ClearScreenCommand commonLanguageSpecificationCommand;
    public CopyCommand copyCommand;
    public MoveCommand moveCommand;

    public CMDStart(){
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = new ExceptionHandling();

        //명령어 처리 클래스
        this.changeDirectoryCommand = new ChangeDirectoryCommand(exceptionHandling);
        this.directoryCommand = new DirectoryCommand(exceptionHandling);
        this.helpCommand = new HelpCommand();
        this.commonLanguageSpecificationCommand =new ClearScreenCommand();
        this.copyCommand =new CopyCommand(exceptionHandling);
        this.moveCommand = new MoveCommand(exceptionHandling);
    }

    public void startCMD(){
        String searchWord = "user.home"; // 초기 출력 경로
        String currentPath;
        String inputSentence;

        setCMDbasicUI(); //기본 출력창 출력하기
        currentPath = desktopInformation.getDirectoryPath(searchWord);
        CMDUI.printCommandLine(currentPath); // 맨처음 커맨드 라인 출력

        while (true) {
            inputSentence = receiveInputCommandLine(); //명령어 입력 받기
            currentPath = classifyCommandFunction(inputSentence, currentPath); //명령어 구분하고 해당 명령어 클래스로 들어가기
            CMDUI.printCommandLine(currentPath); //다음입력 받기
        }
    }

    private void setCMDbasicUI(){ //CMD 실행시 기본으로 출력되는 UI설정
        String[] InitialOutputOfCMD = new String[2]; //CMD실행시 출력되는 2줄 저장하는 배열

        InitialOutputOfCMD = desktopInformation.getInitialOutputOfCMD(); //cmd 버전정보 불러오기
        CMDUI.printInitialOutputOfCMD(InitialOutputOfCMD); //cmd 버전정보 출력하기
    }

    private String receiveInputCommandLine(){
        Scanner scan = new Scanner(System.in);
        String inputSentence = scan.nextLine(); //명령어 입력받기
        return inputSentence;
    }

    private String classifyCommandFunction(String inputSentence, String currentPath) { // 어떤 명령어가 들어왔는지 확인하는 함수
        String OptimizedString = exceptionHandling.optimizeStringForJudge(inputSentence); //검사하기 알맞게 문자열 최적화

        if (OptimizedString.startsWith("cd")) { //cd 명령어
            currentPath = changeDirectoryCommand.differentiateChangeDirectoryFunction(inputSentence, currentPath);
        }
        else if (OptimizedString.startsWith("dir")) { //dir 명령어
            directoryCommand.differentiateChangeDirectoryFunction(inputSentence, currentPath);
        }
        else if (OptimizedString.startsWith("cls")) {//cls 명령어
            commonLanguageSpecificationCommand.clearAll();
        }
        else if (OptimizedString.startsWith("help")) { //help 명령어
            helpCommand.printHelpPhrase();
        }
        else if (OptimizedString.startsWith("copy")) { //copy 명령어
            copyCommand.differentiateCopyFunction(inputSentence,currentPath);
        }
        else if (OptimizedString.startsWith("move")) { //move 명령어
            moveCommand.differentiateMoveFunction(inputSentence,currentPath);
        }
        else { //해당하는 명령어가 없을 때
            // 배치파일이 아닙니다 오류 문 출력
            //입력받은 값 중 띄어쓰기 나오기 전 문자열만 추출하기
            String stringsArray[] = OptimizedString.split(" ");
            CMDUI.printErrorMessage("\'"+ stringsArray[0] +"\'" + Constants.NOT_FIND_COMMAND);

        }
        return currentPath;
    }
}
