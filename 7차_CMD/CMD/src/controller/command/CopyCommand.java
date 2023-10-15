package controller.command;

import model.PathDTO;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;
import view.CMDUI;
import controller.commandHelper.CopyAndMove;

import java.util.ArrayList;
import java.util.List;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.Scanner;

public class CopyCommand extends CopyAndMove{
    public DesktopInformation desktopInformation;
    public ExceptionHandling exceptionHandling;
    public CopyCommand(ExceptionHandling exceptionHandling){
        super(exceptionHandling);
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = exceptionHandling;
    }
    public void differentiateCopyFunction(String inputSentence, String currentPath){
        String OptimizedString = exceptionHandling.optimizeStringForJudge(inputSentence); //검사하기 알맞게 문자열 최적화
        String pathRemainedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 4, !Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 미제거
        String pathRemovedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 4, Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 제거
        List<String> vaildPaths = new ArrayList<String>();
        PathDTO copyPathDTO;

        if(pathRemainedHeadAndTailWhiteSpace.charAt(0) == ' '){
            vaildPaths = checkAndFindValidationPathAndFileInInputCommand(pathRemovedHeadAndTailWhiteSpace,currentPath);
            if(vaildPaths.size() == 0){ //입력받은 경로나 파일이 올바르지 않은 경우
                CMDUI.printErrorMessage(Constants.CANNOT_FIND_FILE);
                return;
            }
            //올바른 경우 파일 복사하기
            copyPathDTO = distinguishCopyAndMoveCase(vaildPaths, currentPath);
            copyFile(copyPathDTO);
        }

        else{ //copy명령어 뒤에 공백이 아닌 경우 > 올바르지 않은 명령어 처리
            //입력받은 c,o,p,y가 대문자인지 소문자인지 알아내기 위해 맨처음 입력받은 문자열에서 copy부분 가져오기
            String cdInput = inputSentence.trim().substring(0, 4);
            //입력받은 값 중 띄어쓰기 나오기 전 문자열만 추출하기
            String stringsArray[] = pathRemovedHeadAndTailWhiteSpace.split(" ");
            CMDUI.printErrorMessage("\'" + cdInput + stringsArray[0] + "\'" + Constants.NOT_FIND_COMMAND);
            return;
        }

        return;
    }

    private void copyFile(PathDTO copyPathDTO) {
        Path sourcePath = Paths.get(copyPathDTO.getFirstFilePath());
        Path targetPath = Paths.get(copyPathDTO.getSecondFilePath());

        //파일이 서로 같은 경우 > 같은 파일로 복사할 수 없습니다. 0개의 파일이 복사 되었습니다.
        if(sourcePath.equals(targetPath)){
            CMDUI.printErrorMessage(Constants.CANNOT_COPY_ON_SAME_FILE);
            CMDUI.printErrorMessage(Constants.COPY_FAIL_MESSAGE);
            return;
        }
        //정상적으로 파일 복사
        try { //try catch 문 사용 해야만 함?
            //파일이 존재 하면 덮어씌우기 처리
            if (Files.exists(targetPath) && Files.isRegularFile(targetPath)) {
                if (handleFileOverWrite(sourcePath,targetPath)) {
                    Files.delete(targetPath);
                    Files.copy(sourcePath, targetPath);
                    CMDUI.printCommandResult(Constants.COPY_SUCCESS_MESSAGE);
                } else {
                    CMDUI.printCommandResult(Constants.COPY_FAIL_MESSAGE);
                }
                return;
            }
            else {
                Files.copy(sourcePath, targetPath);
                CMDUI.printCommandResult(Constants.COPY_SUCCESS_MESSAGE);
                return;
            }

        } catch (IOException e) {
            CMDUI.printErrorMessage(Constants.CANNOT_FIND_FILE); //파일 경로가 올바르지 않은 경우
        }
    }
}
