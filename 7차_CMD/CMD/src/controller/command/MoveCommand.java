package controller.command;

import controller.commandHelper.CopyAndMove;
import model.PathDTO;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;
import view.CMDUI;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;
import java.util.Scanner;

public class MoveCommand extends CopyAndMove {
    public DesktopInformation desktopInformation;
    public ExceptionHandling exceptionHandling;

    public MoveCommand(ExceptionHandling exceptionHandling) {
        super(exceptionHandling);
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = exceptionHandling;
    }

    public void differentiateMoveFunction(String inputSentence, String currentPath) {
        String OptimizedString = exceptionHandling.optimizeStringForJudge(inputSentence); //검사하기 알맞게 문자열 최적화
        String pathRemainedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 4, !Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 미제거
        String pathRemovedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 4, Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 제거
        List<String> vaildPaths = new ArrayList<String>();
        PathDTO movePathDTO;

        if (pathRemainedHeadAndTailWhiteSpace.charAt(0) == ' ') {
            vaildPaths = checkAndFindValidationPathAndFileInInputCommand(pathRemovedHeadAndTailWhiteSpace, currentPath);
            if (vaildPaths.size() == 0) { //입력받은 경로나 파일이 올바르지 않은 경우
                CMDUI.printErrorMessage(Constants.CANNOT_FIND_FILE);
                return;
            }
            //올바른 경우 파일 이동하기
            movePathDTO = distinguishCopyAndMoveCase(vaildPaths, currentPath);
            moveFile(movePathDTO);
        }

        else { //move명령어 뒤에 공백이 아닌 경우 > 올바르지 않은 명령어 처리
            //입력받은 m,o,v,e가 대문자인지 소문자인지 알아내기 위해 맨처음 입력받은 문자열에서 move부분 가져오기
            String cdInput = inputSentence.trim().substring(0, 4);
            //입력받은 값 중 띄어쓰기 나오기 전 문자열만 추출하기
            String stringsArray[] = pathRemovedHeadAndTailWhiteSpace.split(" ");
            CMDUI.printErrorMessage("\'" + cdInput + stringsArray[0] + "\'" + Constants.NOT_FIND_COMMAND);
            return;
        }
        return;
    }

    private void moveFile(PathDTO movePathDTO) {
        Path sourcePath = Paths.get(movePathDTO.getFirstFilePath());
        Path targetPath = Paths.get(movePathDTO.getSecondFilePath());

        //파일 이동
        try { //try catch 문 사용 해야만 함?
            //파일이 존재 하면 덮어씌우기 처리
            if (Files.exists(targetPath) && Files.isRegularFile(targetPath)) {
                if (handleFileOverWrite(sourcePath, targetPath)) {
                    Files.delete(targetPath);
                    Files.move(sourcePath, targetPath);
                    CMDUI.printCommandResult(Constants.MOVE_SUCCESS_MESSAGE);
                } else {
                    CMDUI.printCommandResult(Constants.MOVE_FAIL_MESSAGE);
                }
                return;
            } else {
                Files.move(sourcePath, targetPath);
                CMDUI.printCommandResult(Constants.MOVE_SUCCESS_MESSAGE);
                return;
            }
        } catch (IOException e) {
            CMDUI.printErrorMessage(Constants.CANNOT_FIND_FILE); //파일 경로가 올바르지 않은 경우
        }
    }

}

