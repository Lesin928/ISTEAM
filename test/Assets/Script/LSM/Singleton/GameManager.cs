using UnityEngine;
using UnityEngine.SceneManagement;

//������ �ٽ� ������ �����ϴ� Ŭ����

public class GameManager : MonoBehaviour
{
    //�̱������� ����
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
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;
             
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� GameManager�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� GameManager)�� �������ش�.
            Destroy(this.gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            if(!isPaused)
            {   //�������¾ƴϸ� ����
                isEscaped = true;
                PauseGame();
            }
            else if (isEscaped && isPaused)
            {   //esc�� ���� ������ �簳
                isEscaped = false;
                ContinueGame();
            }

            //�Ͻ����� UI ��� �ʿ� ����

        }
    }


    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. 
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

    //���� �ʱ� ����
    public void InitGame() 
    {    
        TimerManager.Instance.StartTimer(); 
    }

    //���� ����
    public void PauseGame()
    {
        Time.timeScale = 0;
        //������ �Ͻ������ϴ� �κ�
        isPaused = true;
        //TimerManager.Instance.StopTimer();
        //�Ҹ� ����
        isPaused = true;
        playerControll.Stop();

        //���� ����µ� esc������ ������ ī��� ���� ������ �ƴϸ� �簳
        if (!isEscaped && !UI_GameManager.instance.cardlistOn )
        {
            ContinueGame();
        }
    }

    //���� ����
    public void ContinueGame()
    {
        playerControll.StartMove();
        isPaused = false;
        //������ �ٽ� �����ϴ� �κ�
        //TimerManager.Instance.StartTimer();
        //�Ҹ� ���     
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
    //���� �����
    public void RestartGame()
    {
        //������ �ٽ� �����ϴ� �κ�
        //�� �ʱ�ȭ
        // ���� �� �̸��� ��� �ٽ� �ε� 
        SceneManager.LoadScene("LeeTestScene");
        Debug.Log("���� �����");
    } 

    public void OverGame()
    {
        if (!isOver)
        {
            isOver = true;
            UI_GameManager.instance.GameOver();
            TimerManager.Instance.StopTimer();  
            //���� ����
            //���� ���� UI ����  
        }
    }




}
