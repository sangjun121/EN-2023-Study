package controller.command;

import controller.commandHelper.CdAndDIR;
import model.DriveVolumeDTO;
import model.PathDTO;
import utility.Constants;
import utility.DesktopInformation;
import utility.ExceptionHandling;

import org.apache.commons.io.FileUtils;
import view.CMDUI;

import java.io.File;
import java.util.Date;
import java.text.SimpleDateFormat;

public class DirectoryCommand extends CdAndDIR {
    public DesktopInformation desktopInformation;
    public ExceptionHandling exceptionHandling;

    public DirectoryCommand(ExceptionHandling exceptionHandling) {
        super(exceptionHandling);
        this.desktopInformation = DesktopInformation.getInstance();
        this.exceptionHandling = exceptionHandling;
    }

    public void differentiateChangeDirectoryFunction(String inputSentence, String currentPath) {
        PathDTO currentPathDTO = new PathDTO();
        String OptimizedString = exceptionHandling.optimizeStringForJudge(inputSentence); //검사하기 알맞게 문자열 최적화
        String pathRemainedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 3, !Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 미제거
        String pathRemovedHeadAndTailWhiteSpace = exceptionHandling.optimizeStringRemoveCommand(OptimizedString, 3, Constants.IS_REMOVE_WHITE_SPACE); //앞뒤 공백 제거

        //경로 가져오기
        currentPathDTO = getTotalPath(inputSentence, currentPath, 3);
        //경로 존재하는 경우 > 디렉토리 정보 출력
        if (currentPathDTO.getIsRightPath()) {
            findDirectoryWithPathFromRoot(currentPathDTO.getCurrentPath());
            return;
        }
        //경로 존재하지 않는 경우 예외처리
        //1. dir뒤에 문자가 붙어서 나오는 경우
        if (!pathRemainedHeadAndTailWhiteSpace.startsWith(".") && !pathRemainedHeadAndTailWhiteSpace.startsWith("/") && !pathRemainedHeadAndTailWhiteSpace.startsWith("\\")) {
            //입력받은 c와 d가 대문자인지 소문자인지 알아내기 위해 맨처음 입력받은 문자열에서 cd부분 가져오기
            String dirInput = inputSentence.trim().substring(0, 3);
            //입력받은 값 중 띄어쓰기 나오기 전 문자열만 추출하기
            String stringsArray[] = pathRemovedHeadAndTailWhiteSpace.split(" ");
            CMDUI.printErrorMessage("\'" + dirInput + stringsArray[0] + "\'" + Constants.NOT_FIND_COMMAND);
            return;
        }
        //2. 나머지 > 지정된 경로를 찾을 수 없습니다.
        CMDUI.printErrorMessage(Constants.CANNOT_FIND_PATH);
        return;
    }

    private void findDirectoryWithPathFromRoot(String path) {
        String directoryPath = path; // 디렉토리 경로
        String directoryOrFileName; //파일 혹은 디렉토리 이름
        Date directoryOrFileModifiedDate; //파일 속성에서 불러온 수정일자
        String formattedDate; //포맷팅된 디렉토리 수정일자
        String directoryOrFileType; // 파일인지 디렉토리인지 형식
        SimpleDateFormat directoryTimeFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm"); //포맷 양식
        int fileCount = 0;
        int directoryCount = 0;

        //1. 드라이브 정보 출력
        DriveVolumeDTO driveVolumeDTO = getVolumeInformation();
        CMDUI.printVolumeInformation(driveVolumeDTO);

        //2. 디렉토리 경로 출력
        CMDUI.printDirectory(directoryPath);

        //3. 경로 정보 출력
        File directory = new File(directoryPath);
        if (directory.exists() && directory.isDirectory()) {
            File[] files = directory.listFiles();
            if (files != null) {
                //현재 디렉토리
                getDirectoryInformation(directory,directoryPath,".");
                //부모 디렉토리
                getDirectoryInformation(directory,directoryPath,"..");

                for (File file : files) { //foreach 사용
                    if (!file.isHidden() && !FileUtils.isSymlink(file)) { // 히든파일 및 링크파일 출력하지 않기
                        if (file.isFile()) {
                            getFileInformation(file);
                            fileCount++;
                        }
                        else if (file.isDirectory()) {
                            getDirectoryInformation(file,directoryPath,"");
                            directoryCount++;
                        }
                    }
                }
            }
        }
    }

    private DriveVolumeDTO getVolumeInformation() {
        DriveVolumeDTO driveVolumeDTO = new DriveVolumeDTO();
        driveVolumeDTO.setVolumeName(desktopInformation.getDriveVolumeName("C:\\")); // 볼륨 이름 불러오기
        driveVolumeDTO.setvolumeSerialNumber(desktopInformation.getDriveVolumeSerialNumber("C:\\")); // 드라이브 시리얼 넘버 불러오기
        return driveVolumeDTO;
    }

    private void getFileInformation(File file) { //파일인 경우
        SimpleDateFormat directoryTimeFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm"); //포맷 양식
        String directoryOrFileName = file.getName();
        Date directoryOrFileModifiedDate = new Date(file.lastModified());
        String formattedDate = directoryTimeFormat.format(directoryOrFileModifiedDate);
        String directoryOrFileType = file.isDirectory() ? "<DIR>" : "     ";

        CMDUI.printDirDirectoryInformation(formattedDate, directoryOrFileType, directoryOrFileName);
    }

    private void getDirectoryInformation(File file, String currentDirectory, String directoryOrFileName) { //디렉토리인 경우
        SimpleDateFormat directoryTimeFormat;
        Date directoryOrFileModifiedDate;
        String formattedDate;
        String directoryOrFileType;

        File currentFileDirectory = new File(currentDirectory);
        directoryTimeFormat = new SimpleDateFormat("yyyy-MM-dd a hh:mm"); //포맷 양식
        directoryOrFileModifiedDate = new Date(file.lastModified());
        formattedDate = directoryTimeFormat.format(directoryOrFileModifiedDate);
        directoryOrFileType = file.isDirectory() ? "<DIR>" : "     ";

       if(directoryOrFileName.equals("")){
           directoryOrFileName = file.getName();
       }
       CMDUI.printDirDirectoryInformation(formattedDate, directoryOrFileType, directoryOrFileName);
    }
}
