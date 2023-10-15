package controller.command;

import controller.commandHelper.CdAndDIR;
import model.PathDTO;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;
import view.CMDUI;

public class ChangeDirectoryCommand extends CdAndDIR{
    public DesktopInformation desktopInformation;
    public ExceptionHandling exceptionHandling;

    public ChangeDirectoryCommand(ExceptionHandling exceptionHandling){
        super(exceptionHandling);
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = exceptionHandling;
    }

    public String differentiateChangeDirectoryFunction(String inputSentence, String currentPath) {
        PathDTO currentPathDTO = new PathDTO();
        String OptimizedString = exceptionHandling.optimizeStringForJudge(inputSentence); //검사하기 알맞게 문자열 최적화
        String pathRemainedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 2, !Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 미제거
        String pathRemovedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 2, Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 제거

        //경로 가져오기
        currentPathDTO = getTotalPath(inputSentence,currentPath,2);
        //경로 존재하는 경우
        if(currentPathDTO.getIsRightPath()) return currentPathDTO.getCurrentPath();

        //경로 존재하지 않는 경우 예외처리
        //1."cd/?" 명령어는 공백에 상관없이 그리고 뒤에 어떤 문자가 같이와도 실행된다.
        if (pathRemovedHeadAndTailWhiteSpace.replaceAll(" ", "").contains("/?")) {
            CMDUI.printNotice(Constants.CHANGE_DIRECTORY_NOTICE);
            return currentPath;
        }

        //2. cd뒤에 문자가 붙어서 나오는 경우
        if(!pathRemainedHeadAndTailWhiteSpace.startsWith(".") && !pathRemainedHeadAndTailWhiteSpace.startsWith("/") && !pathRemainedHeadAndTailWhiteSpace.startsWith("\\")) {
            //입력받은 c와 d가 대문자인지 소문자인지 알아내기 위해 맨처음 입력받은 문자열에서 cd부분 가져오기
            String cdInput = inputSentence.trim().substring(0, 2);
            //입력받은 값 중 띄어쓰기 나오기 전 문자열만 추출하기
            String stringsArray[] = pathRemovedHeadAndTailWhiteSpace.split(" ");
            CMDUI.printErrorMessage("\'" + cdInput + stringsArray[0] + "\'" + Constants.NOT_FIND_COMMAND);
            return currentPath;
        }
        //3. 나머지 > 지정된 경로를 찾을 수 없습니다.
        CMDUI.printErrorMessage(Constants.CANNOT_FIND_PATH);
        return currentPathDTO.getCurrentPath();
    }

}
