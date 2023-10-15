package view;

import model.DriveVolumeDTO;
import utility.Constants;

public class CMDUI {
    public static void printInitialOutputOfCMD(String[] InitialOutputOfCMD){
        System.out.println(InitialOutputOfCMD[0] + "\n" + InitialOutputOfCMD[1]);
    }
    public static void printCommandLine(String path){ //커맨드라인 출력하는 함수
        System.out.print("\n"+ path + ">");
    }

    public static void printNotice(String notice){
        System.out.print(notice);
    }

    public static void printCommandResult(String result){
        System.out.println(result);
    }

    public static void printErrorMessage(String errorMessage){
        System.out.println(errorMessage);
    }
    public static void printVolumeInformation(DriveVolumeDTO driveVolumeDTO) {
        //볼륨 이름이 없다는 가정 하에 출력
        System.out.println(Constants.NO_VOLUME_NAME_NOTICE);
        System.out.println(Constants.VOLUME_NAME_VERSION_NOTICE + driveVolumeDTO.getvolumeSerialNumber() + "\n");
    }
    public static void printQuestion(String question) {
        System.out.print(question);
    }
    public static void printDirectory(String directory){
        System.out.println(" " + directory +" 디렉터리"+ "\n");
    }
    public static void printDirDirectoryInformation(String formattedDate,String directoryOrFileType,String directoryOrFileName){
        System.out.println(formattedDate + "   " + directoryOrFileType + "   " + directoryOrFileName);
    }

}
