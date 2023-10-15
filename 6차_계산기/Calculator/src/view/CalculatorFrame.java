package view;

import controller.ButtonEvent;
import controller.KeyPressEvent;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.util.*;
import java.util.List;

public class CalculatorFrame extends JFrame {

    //프레임 위에 올릴 입력창 패널과 버튼패널 그리고 로그 패널 생성
    public JPanel basePanel;
    public JPanel logButtonPanel;
    public JPanel inputPanel;
    public JPanel buttonPanel;
    public JPanel logPanel;

    //logButtonPanel의 Components
    public JButton logButton;

    //inputPanel의 Components
    public JLabel preNumberLabel;
    public JLabel numberInputLabel;

    //buttonPanel의 Components
    public String[] buttonTitle = {"CE", "C", "←", "÷", "7", "8", "9", "x", "4", "5", "6", "-", "1", "2", "3", "+", "±", "0", ".", "="};
    public JButton[] calculatebuttons;

    //logPanel의 Components
    public JScrollPane logScrollPane; //로그 패널을 올릴 스크롤 팬
    public JButton logTextButton;

    //계산시 사용하는 변수들 선언
    public String savedNumber;
    public String firstNumber;
    public String secondNumber;
    public String operator;
    public Boolean isEqualExist;

    //로그 데이터 정보 불러왔을 때 로직 처리 확인할 변수 선언
    public Boolean isLogExpression;

    //로그 데이터 정보 저장할 변수들 선언
    public List<String> logList;


    //버튼 클릭에 대한 ActionListener 클래스 인스턴스 생성
    ButtonEvent buttonEvent;
    ButtonEvent.NumberButtonEventListenerClass numberButtonEventListenerClass;
    ButtonEvent.OperatorButtonEventListenerClass operatorButtonEventListenerClass;
    ButtonEvent.EqualButtonEventListenerClass equalButtonEventListenerClass;
    ButtonEvent.ClearButtonEventListenerClass clearButtonEventListenerClass;
    ButtonEvent.DecimalPointButtonEventListenerClass decimalPointButtonEventListenerClass;
    ButtonEvent.BackSpaceButtonEventListenerClass backSpaceButtonEventListenerClass;
    ButtonEvent.PlusAndMinusButtonEventListenerClass plusAndMinusButtonEventListenerClass;
    ButtonEvent.ClearEntryButtonEventListenerClass clearEntryButtonEventListenerClass;

    //키 입력에 대한 ActionListener 클래스 인스턴스 생성
    KeyPressEvent keyPressEventEvent;
    KeyPressEvent.KeyInputListener keyInputListener;

    //로그 눌렀을때 해당 기록 불러오는 버튼 이벤트
    ButtonEvent.LogHistoryButtonEventListenerClass logHistoryButtonEventListenerClass;

    public CalculatorFrame() { //생성자
        //버튼에 대한 리스너 객체 생성하기 (이렇게 생성해서 쓰면 왜 안되는지)
        this.buttonEvent = new ButtonEvent(this);
        this.numberButtonEventListenerClass = buttonEvent.new NumberButtonEventListenerClass();
        this.operatorButtonEventListenerClass = buttonEvent.new OperatorButtonEventListenerClass();
        this.equalButtonEventListenerClass = buttonEvent.new EqualButtonEventListenerClass();
        this.clearButtonEventListenerClass = buttonEvent.new ClearButtonEventListenerClass();
        this.decimalPointButtonEventListenerClass = buttonEvent.new DecimalPointButtonEventListenerClass();
        this.backSpaceButtonEventListenerClass = buttonEvent.new BackSpaceButtonEventListenerClass();
        this.plusAndMinusButtonEventListenerClass =buttonEvent.new PlusAndMinusButtonEventListenerClass();
        this.clearEntryButtonEventListenerClass = buttonEvent.new ClearEntryButtonEventListenerClass();

        this.logHistoryButtonEventListenerClass = buttonEvent.new LogHistoryButtonEventListenerClass();

        this.basePanel = new JPanel(new BorderLayout());
        this.logButtonPanel = new JPanel(new FlowLayout(FlowLayout.RIGHT));
        this.inputPanel = new JPanel(new GridLayout(2, 1, 0, 0));
        this.buttonPanel = new JPanel(new GridLayout(5, 4, 0, 0));
        this.logPanel = new JPanel(new GridLayout(0, 1));
        this.logScrollPane = new JScrollPane();
        this.logScrollPane.setViewportView(logPanel);

        //logButtonPanel의 Components
        this.logButton = new JButton();

        //inputPanel의 Components
        this.numberInputLabel = new JLabel("0");
        this.preNumberLabel = new JLabel("");

        //buttonPanel의 Components
        this.calculatebuttons = new JButton[20];

        //계산시 사용하는 변수들 선언
        this.savedNumber= "";
        this.firstNumber = "0";
        this.secondNumber = "";
        this.operator = "";
        this.isEqualExist = false;

        //로그 기록 불러왔을 때 계산 수행하기 위한 변수
        this.isLogExpression = false;

        //로그 기록 저장 리스트
        this.logList = new ArrayList<>();

        setTitle("계산기");
        setSize(324, 534);
        setMinimumSize(new Dimension(324,534)); //최소 크기 지정
        setLayout(new BorderLayout());
        this.addComponentListener(new ComponentAdapter() { //프레임 사이즈 변화에 대한 이벤트 리스너 설정
            @Override
            public void componentResized(ComponentEvent e) {
                if(e.getComponent().getWidth() >= 648 ){
                    //로그버튼 무효화
                    logButton.setEnabled(false);
                    //로그창이 위에 떠있을때
                    showLogPanel();
                    //입력창 늘렸을때 패널 옆에 붙이기
                    resizePanelsWithLogPanel();
                    logScrollPane.setPreferredSize(new Dimension((e.getComponent().getWidth()) /2, e.getComponent().getHeight()));
                    add(logScrollPane, BorderLayout.EAST);
                }
                else{
                    //로그버튼 유효화
                    logButton.setEnabled(true);
                    resizePanelsWithOutLogPanel(); //사이즈 변경에 따라 패널 비율 맞추는 함수
                }
            }
        });

        setLocationRelativeTo(null);
        setDefaultCloseOperation(EXIT_ON_CLOSE);

        composeBasePanel(); //base패널 구성
        add(basePanel,BorderLayout.WEST); //base 패널 Frame 위에 올리기
        this.addKeyListener((new KeyPressEvent(this)).new KeyInputListener());
        this.setFocusable(true);
        setVisible(true);
    }

    public void composeBasePanel() { //패널 구성 후 base 패널에 최종적으로 올리기
        //패널 사이즈 지정
        //base패널 내 3개의 패널 비율 설정
        Dimension frameSize = new Dimension(this.getWidth(), this.getHeight()); // frame 사이즈 불러오기
        basePanel.setPreferredSize(new Dimension(frameSize.width, frameSize.height)); //base프레임은 frame 사이즈와 동일하게 설정
        logButtonPanel.setPreferredSize(new Dimension(frameSize.width, (int)(frameSize.height * (1.0/12))));
        inputPanel.setPreferredSize(new Dimension(frameSize.width, (int)(frameSize.height * (3.0/12))));
        buttonPanel.setPreferredSize(new Dimension(frameSize.width, (int)(frameSize.height * (8.0/12))));
        logScrollPane.setPreferredSize(new Dimension(frameSize.width,(int)(frameSize.height * (8.0/12))));

        //0. 로그버튼 패널 입력 구성
        logButton = setImageOnButton("utility/image/free-icon-clock-1827463.png");
        logButton.setPreferredSize( new Dimension (40,40) );
        logButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                panelTransition();
                requestFocus();
            }
        });

        logButtonPanel.add(logButton);
        logButtonPanel.add(logButton);

        basePanel.add(logButtonPanel,BorderLayout.NORTH);

        //1. 상단부 숫자 입력 패널 구성
        //패널에 올라갈 JLabel 텍스트 정렬 설정
        preNumberLabel.setHorizontalAlignment(JLabel.RIGHT);
        numberInputLabel.setHorizontalAlignment(JLabel.RIGHT);

        //Frame을 키움에 따라 입력받은 숫자 JLabel의 사이즈도 함께 증가
        numberInputLabel.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {
                int fontSize = numberInputLabel.getHeight();
                numberInputLabel.setFont(new Font("나눔고딕", Font.BOLD, fontSize));

                Font numberInputLabelFont = new Font("나눔고딕", Font.BOLD, numberInputLabel.getHeight()); //초기 폰트 값 저장
                FontMetrics numberInputLabelFontMetrics = numberInputLabel.getFontMetrics(numberInputLabelFont);
                int numberWidth = numberInputLabelFontMetrics.stringWidth(numberInputLabel.getText());

                if(numberInputLabel.getWidth() < numberWidth){
                    while(numberInputLabel.getWidth() * 3.8 / 4 < numberWidth) {
                        fontSize = fontSize - 1;
                        numberInputLabelFont = new Font("나눔고딕", Font.BOLD, fontSize); //초기 폰트 값 저장
                        numberInputLabelFontMetrics = numberInputLabel.getFontMetrics(numberInputLabelFont);
                        numberWidth = numberInputLabelFontMetrics.stringWidth(numberInputLabel.getText());
                        numberInputLabel.setFont(new Font("나눔고딕", Font.BOLD, fontSize));
                    }
                }
                else{
                    numberInputLabel.setFont(new Font("나눔고딕", Font.BOLD, numberInputLabel.getHeight()));
                }
            }
        });

        // inputPanel 위에 컴포넌트 올리기
        inputPanel.add(preNumberLabel);
        inputPanel.add(numberInputLabel);
        basePanel.add(inputPanel,BorderLayout.CENTER);

        //2. 하단부 버튼 패널 구성 //매직넘버 처리하기
        for (int i = 0; i < 20; i++) {
            calculatebuttons[i] = new JButton(buttonTitle[i]);

            //버튼 UI
            calculatebuttons[i].setContentAreaFilled(false);
            calculatebuttons[i].setOpaque(true); // 투명도 해제
            calculatebuttons[i].setBorderPainted(false);

            //버튼에 ActionListener 할당
            //1. 숫자버튼인 경우
            if((i == 4) || (i == 5) ||(i == 6) ||(i == 8) ||(i == 9) ||(i == 10) ||(i == 12) ||(i == 13) ||(i == 14) ||(i == 17)){
                calculatebuttons[i].setBackground(new Color(255,255,255));
                calculatebuttons[i].addActionListener(numberButtonEventListenerClass);
            }
            //2. 연산자버튼인 경우
            else if((i == 3) ||(i == 7) ||(i == 11) ||(i == 15) ){
                calculatebuttons[i].setBackground(new Color(226,226,226));
                calculatebuttons[i].addActionListener(operatorButtonEventListenerClass);
            }
            //3. Equal버튼인 경우
            else if(i == 19){
                //버튼에 색깔 추가
                calculatebuttons[i].setBackground(new Color(140,231,237)); //색 설정
                calculatebuttons[i].addActionListener(equalButtonEventListenerClass);
            }
            //4. CLEAR 버튼인 경우
            else if(i == 1){
                calculatebuttons[i].setBackground(new Color(226,226,226));
                calculatebuttons[i].addActionListener(clearButtonEventListenerClass);
            }
            //5. 소수점 버튼인 경우
            else if (i == 18){
                calculatebuttons[i].setBackground(new Color(226,226,226));
                calculatebuttons[i].addActionListener(decimalPointButtonEventListenerClass);
            }
            //6.BackSpace 버튼인 경우
            else if (i == 2){
                calculatebuttons[i].setBackground(new Color(226,226,226));
                calculatebuttons[i].addActionListener(backSpaceButtonEventListenerClass);
            }
            //7.ClearEntry 버튼인 경우
            else if (i == 0){
                calculatebuttons[i].setBackground(new Color(226,226,226));
                calculatebuttons[i].addActionListener(clearEntryButtonEventListenerClass);
            }
            //8.PlusAndMinus 버튼인 경우
            else if (i == 16){
                calculatebuttons[i].setBackground(new Color(226,226,226));
                calculatebuttons[i].addActionListener(plusAndMinusButtonEventListenerClass);
            }
            //버튼패널 위에 버튼 올리기
            buttonPanel.add(calculatebuttons[i]);
        }
        basePanel.add(buttonPanel,BorderLayout.SOUTH);

        //로그패널 구성
        logScrollPane.setHorizontalScrollBarPolicy(JScrollPane.HORIZONTAL_SCROLLBAR_NEVER); //가로 스크롤바 무효화
        logScrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED); //세로 스크롤바 항상 보이기

    }

    private void resizePanelsWithOutLogPanel() { //프레임 사이즈 변화시 모든 패널 같은 비율에 맞게 사이즈 변경
        int frameHeight = this.getHeight();
        int basePanelHeight = frameHeight;
        int logButtonPanelHeight = (int)(frameHeight * (1.0 / 12));
        int inputPanelHeight = (int)(frameHeight * (3.0 / 12));
        int buttonPanelHeight = (int)(frameHeight * (8.0 / 12));
        int logScrollPaneHeight = (int) (frameHeight * (8.0 / 12));
        int logPanelHeight = (int)(frameHeight * (8.0 / 12));

        basePanel.setPreferredSize(new Dimension(this.getWidth(),basePanelHeight));
        logButtonPanel.setPreferredSize(new Dimension(this.getWidth(), logButtonPanelHeight));
        inputPanel.setPreferredSize(new Dimension(this.getWidth(), inputPanelHeight));
        buttonPanel.setPreferredSize(new Dimension(this.getWidth(), buttonPanelHeight));
        logScrollPane.setPreferredSize(new Dimension(this.getWidth(),logScrollPaneHeight));

        revalidate();
    }
    private void resizePanelsWithLogPanel() { //프레임 사이즈 변화시 모든 패널 같은 비율에 맞게 사이즈 변경
        int frameHeight = this.getHeight();
        int basePanelHeight = frameHeight;
        int logButtonPanelHeight = (int)(frameHeight * (1.0 / 12));
        int inputPanelHeight = (int)(frameHeight * (3.0 / 12));
        int buttonPanelHeight = (int)(frameHeight * (8.0 / 12));

        basePanel.setPreferredSize(new Dimension(this.getWidth() /2,basePanelHeight));
        logButtonPanel.setPreferredSize(new Dimension(this.getWidth()/2, logButtonPanelHeight));
        inputPanel.setPreferredSize(new Dimension(this.getWidth()/2, inputPanelHeight));
        buttonPanel.setPreferredSize(new Dimension(this.getWidth()/2, buttonPanelHeight));
        revalidate();
    }


    public JButton setImageOnButton(String imagePath){
        ImageIcon logIcon = new ImageIcon(imagePath);
        Image buttonImage = logIcon.getImage();

        Image resizedButtonImage = buttonImage.getScaledInstance(30, 30, Image.SCALE_SMOOTH);
        ImageIcon resizedImageIcon = new ImageIcon(resizedButtonImage);

        JButton imageButton = new JButton(resizedImageIcon);

        return imageButton;
    }

    public void panelTransition(){
        Boolean isButtonPanelOnBasePanel = false;

        Component[] basePanelComponents = basePanel.getComponents();
        for(Component component : basePanelComponents){
            if(component == buttonPanel){
                isButtonPanelOnBasePanel = true;
                break;
            }
        }

        if(isButtonPanelOnBasePanel){
            basePanel.remove(buttonPanel);
            basePanel.add(logScrollPane,BorderLayout.SOUTH);
            revalidate();
        }
        else{
            basePanel.remove(logScrollPane);
            basePanel.add(buttonPanel,BorderLayout.SOUTH);
            revalidate();
        }
        repaint();
    }

    public void showLogPanel(){
        Boolean isLogPanelOnBasePanel = false;
        //로그 패널이 떠 있는지 확인
        Component[] basePanelComponents = basePanel.getComponents();
        for(Component component : basePanelComponents){
            if(component == logScrollPane){
                isLogPanelOnBasePanel = true;
                break;
            }
        }
        if(isLogPanelOnBasePanel){
            basePanel.remove(logScrollPane);
            basePanel.add(buttonPanel,BorderLayout.SOUTH);
            revalidate();
        }
        repaint();
    }

    public void composeLogBasePanel(){
        String logTotalText;
        String[] logTextArray;

        //기존 출력 초기화
        logPanel.removeAll();

        //로그 개수에 맞게 그리드 셀 할당
        logPanel.setLayout(new GridLayout(logList.size() + 3, 1, 0, 0));

        for(int i = logList.size() - 1; i>=0; i--){ //로그 역순으로 출력

            logTotalText = logList.get(i);
            logTextArray = logTotalText.split("/");

            logTextButton = new JButton("<html><div style=\"text-align: right;\">" +logTextArray[0] +"<br><font size=\"5\"><b>" + logTextArray[1] + "</b></font></div></html>");
            logTextButton.setHorizontalAlignment(JLabel.RIGHT);
            logTextButton.addActionListener(logHistoryButtonEventListenerClass);
            logPanel.add(logTextButton);
        }
        logPanel.revalidate();
        logPanel.repaint();
    }
}


