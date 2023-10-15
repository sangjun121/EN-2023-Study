package controller.commandHelper;

import model.PathDTO;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;
import view.CMDUI;

public class CdAndDIR {
    public DesktopInformation desktopInformation;
    public ExceptionHandling exceptionHandling;
    public CdAndDIR(ExceptionHandling exceptionHandling){
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = exceptionHandling;
    }
    protected PathDTO getTotalPath(String inputSentence, String currentPath, int removeStringLength){
        String OptimizedString = exceptionHandling.optimizeStringForJudge(inputSentence); //검사하기 알맞게 문자열 최적화
        String pathRemainedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, removeStringLength, !Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 미제거
        String pathRemovedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, removeStringLength, Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 제거

        //1. 마침표로 이동하는 경우
        if (pathRemovedHeadAndTailWhiteSpace.equals("")) { //현재경로
            CMDUI.printCommandResult(currentPath);
            return setvaildPath(currentPath);
        } else if (pathRemovedHeadAndTailWhiteSpace.equals(".")) {
            return setvaildPath(currentPath);
        } else if (pathRemovedHeadAndTailWhiteSpace.equals("..") || pathRemovedHeadAndTailWhiteSpace.equals("..\\") || pathRemovedHeadAndTailWhiteSpace.equals("../")) { //이전 경로인 경우
            currentPath = desktopInformation.getParentPath(currentPath);
            return setvaildPath(currentPath);
        } else if (pathRemovedHeadAndTailWhiteSpace.equals("\\") || pathRemovedHeadAndTailWhiteSpace.equals("/")) { // 최상위 폴더로 이동하는 경우
            currentPath = desktopInformation.getRootPath(currentPath);
            return setvaildPath(currentPath);
        }

        //2.상대 경로를 입력 받는 경우
        if (pathRemovedHeadAndTailWhiteSpace.startsWith(".")) {
            if (desktopInformation.checkPathExists(currentPath + "/" + pathRemovedHeadAndTailWhiteSpace)) //상대경로 이므로 현재 디렉토리에 입력한 경로 붙이기
            {
                currentPath = desktopInformation.getPath(currentPath + "/" + pathRemovedHeadAndTailWhiteSpace);
                return setvaildPath(currentPath);
            }
            return setInvaildPath(currentPath);
        }

        //3. 절대 경로를 입력 받는 경우
        if (pathRemainedHeadAndTailWhiteSpace.charAt(0) == ' ') {
            //명령어와 뒤의 문자열이 띄어쓰기 되어있으면 앞뒤 띄어쓰기 제거하기 위해 pathRemovedHeadAndTailWhiteSpace 사용
            if (pathRemovedHeadAndTailWhiteSpace.startsWith("c:")) { //root부터 전체 경로를 입력 했을 경우
                if (desktopInformation.checkPathExists(pathRemovedHeadAndTailWhiteSpace)) { // 파일경로가 있을때
                    return setvaildPath(pathRemovedHeadAndTailWhiteSpace);
                }
                return setInvaildPath(currentPath);
            }

            // "\Users\junch" 와 같이 절대경로 중 일부만 입력 시
            else if (pathRemovedHeadAndTailWhiteSpace.startsWith("\\") || pathRemovedHeadAndTailWhiteSpace.startsWith("/")) {
                if (desktopInformation.checkPathExists(pathRemovedHeadAndTailWhiteSpace)) {
                    return setvaildPath(desktopInformation.getPath(pathRemovedHeadAndTailWhiteSpace));
                }
                return setInvaildPath(currentPath);
            }

            //디렉토리명만 입력 받았을 시
            else if (!pathRemovedHeadAndTailWhiteSpace.contains("\\") && !pathRemovedHeadAndTailWhiteSpace.contains("/")) {
                if (desktopInformation.checkPathExists(currentPath + "\\" + pathRemovedHeadAndTailWhiteSpace)) {
                    return setvaildPath(desktopInformation.getPath(currentPath + "\\" + pathRemovedHeadAndTailWhiteSpace));
                }
                return setInvaildPath(currentPath);
            }

            //이외 이상한 값 입력 받았을 시
            return setInvaildPath(currentPath);
        }
        else{
            return setInvaildPath(currentPath);
        }
    }
    private PathDTO setInvaildPath(String currentPath){
        PathDTO currentPathDTO = new PathDTO();
        currentPathDTO.setCurrentPath(currentPath);
        currentPathDTO.setIsRightPath(false);
        return currentPathDTO;
    }
    private PathDTO setvaildPath(String currentPath){
        PathDTO currentPathDTO = new PathDTO();
        currentPathDTO.setCurrentPath(currentPath);
        currentPathDTO.setIsRightPath(true);
        return currentPathDTO;
    }
}
