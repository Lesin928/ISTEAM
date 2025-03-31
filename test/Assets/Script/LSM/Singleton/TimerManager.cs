using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    private BossEvent bossEvent; 

    private float elapsedTime = 0f;
    private bool isRunning = false;
    public float firstBossTime = 120f;
    public bool firstBoss = false;
    public float secondBossTime = 300f;
    public bool secondBoss = false;
    public float thirdBossTime = 600f;
    public bool thirdBoss = false;
    public int killCount = 0;   


    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            killCount = 0;
            elapsedTime = 0f; 
        }
        isRunning = true;
        firstBossTime = 120f;
        secondBossTime = 300f;
        thirdBossTime = 600f;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        } 
        bossEvent = FindAnyObjectByType<BossEvent>();
    }

    private void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > firstBossTime && !firstBoss)
            {
                firstBoss = true;
                Debug.Log(elapsedTime);
                StopTimer();
                bossEvent.SetBoss();
            }
            else if (elapsedTime > secondBossTime && !secondBoss)
            {
                secondBoss = true;
                StopTimer();
                bossEvent.SetBoss();
            }
            else if (elapsedTime > thirdBossTime && !thirdBoss)
            {
                thirdBoss = true;
                StopTimer();
                bossEvent.SetBoss();
            }
        }
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
     
}
