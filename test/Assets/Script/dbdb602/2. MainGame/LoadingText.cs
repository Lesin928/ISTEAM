using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingText : MonoBehaviour
{
    public Text textUI; // UI 텍스트
    private int count = 1;

    void Start()
    {
        StartCoroutine(SpawnText());
    }

    IEnumerator SpawnText()
    {
        while (true) // 무한 반복
        {
            textUI.text += $"."; // 새로운 텍스트 추가
            count++;
            yield return new WaitForSeconds(1f); // 1초 대기
        }
    }
}
