using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text timeText;   // �÷��� �ð��� ǥ���� UI �ؽ�Ʈ
    public Text killText;   // óġ�� ���� ���� ǥ���� UI �ؽ�Ʈ 
    public float elapsedTime;
    public int killCount;

    private void Start()
    { 
        elapsedTime = TimerManager.Instance.GetElapsedTime();
        killCount = TimerManager.Instance.killCount;
        ShowGameOverUI();
    }

    public void ShowGameOverUI()
    {
        if (TimerManager.Instance != null)
        {  
            timeText.text = FormatTime(elapsedTime);
            killText.text = FormatKillCount(killCount);
        } 
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private string FormatKillCount(int count)
    {
        return count.ToString("D4"); // 4�ڸ� 0000 ����
    }
}