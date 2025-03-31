using UnityEngine;
using UnityEngine.SceneManagement;

//게임의 핵심 진행을 관리하는 클래스

public class GameManager : MonoBehaviour
{
    //싱글톤으로 구현
    private static GameManager instance = null;
    private bool isPaused = false;
    private bool isEscaped = false;
    private bool isOver = false;
    private PlayerControll playerControll;

    void Awake()
    {
        playerControll = FindAnyObjectByType<PlayerControll>();
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;
             
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 GameManager이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 GameManager)을 삭제해준다.
            Destroy(this.gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            if(!isPaused)
            {   //정지상태아니면 정지
                isEscaped = true;
                PauseGame();
            }
            else if (isEscaped && isPaused)
            {   //esc로 인한 정지면 재개
                isEscaped = false;
                ContinueGame();
            }

            //일시정지 UI 띄울 필요 있음

        }
    }


    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. 
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    //게임 초기 세팅
    public void InitGame() 
    {    
        TimerManager.Instance.StartTimer(); 
    }

    //게임 정지
    public void PauseGame()
    {
        Time.timeScale = 0;
        //게임을 일시정지하는 부분
        isPaused = true;
        //TimerManager.Instance.StopTimer();
        //소리 정지
        isPaused = true;
        playerControll.Stop();

        //만약 멈췄는데 esc로인한 정지와 카드로 인한 정지가 아니면 재개
        if (!isEscaped && !UI_GameManager.instance.cardlistOn )
        {
            ContinueGame();
        }
    }

    //게임 진행
    public void ContinueGame()
    {
        playerControll.StartMove();
        isPaused = false;
        //게임을 다시 진행하는 부분
        //TimerManager.Instance.StartTimer();
        //소리 재생     
        Time.timeScale = 1;
    }
    public void PauseGame(float delay)
    {
        Invoke("PauseGame", delay);
    }
    public void ContinueGame(float delay)
    {
        Invoke("ContinueGame", delay);
    }
    //게임 재시작
    public void RestartGame()
    {
        //게임을 다시 시작하는 부분
        //씬 초기화
        // 현재 씬 이름을 얻고 다시 로드 
        SceneManager.LoadScene("LeeTestScene");
        Debug.Log("게임 재시작");
    } 

    public void OverGame()
    {
        if (!isOver)
        {
            isOver = true;
            UI_GameManager.instance.GameOver();
            TimerManager.Instance.StopTimer();  
            //게임 오버
            //게임 오버 UI 띄우기  
        }
    }




}
