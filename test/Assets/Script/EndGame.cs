using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text timeText;   // 플레이 시간을 표시할 UI 텍스트
    public Text killText;   // 처치한 몬스터 수를 표시할 UI 텍스트 
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
        return count.ToString("D4"); // 4자리 0000 포맷
    }
}