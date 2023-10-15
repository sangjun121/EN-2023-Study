package model;

public class DriveVolumeDTO {
    private String volumeName;
    private String volumeSerialNumber;

    //Getter 메소드
    public String getvolumeName() {
        return volumeName;
    }
    public String getvolumeSerialNumber() {
        return volumeSerialNumber;
    }

    // Setter 메서드
    public void setVolumeName(String volumeName) {
        this.volumeName = volumeName;
    }

    public void setvolumeSerialNumber(String volumeSerialNumber) {
        this.volumeSerialNumber = volumeSerialNumber;
    }

}
