package model;

public class PathDTO {
    //copy, move에서 사용
    private String firstFilePath;
    private String secondFilePath;

    //cd,dir에서 사용
    private String currentPath;
    private boolean isRightPath;

    //Getter 메소드
    public String getFirstFilePath() {
        return firstFilePath;
    }
    public String getSecondFilePath() {
        return secondFilePath;
    }
    public String getCurrentPath(){
        return currentPath;
    }
    public Boolean getIsRightPath(){
        return isRightPath;
    }

    // Setter 메서드
    public void setFirstFilePath(String firstFilePath) {
        this.firstFilePath = firstFilePath;
    }

    public void setSecondFilePath(String secondFilePath) {
        this.secondFilePath = secondFilePath;
    }
    public void setCurrentPath(String currentPath){
        this.currentPath =currentPath;
    }
    public void setIsRightPath(Boolean isRightPath){
        this.isRightPath = isRightPath;
    }

}
