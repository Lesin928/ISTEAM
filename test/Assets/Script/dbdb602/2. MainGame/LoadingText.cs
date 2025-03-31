using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingText : MonoBehaviour
{
    public Text textUI; // UI �ؽ�Ʈ
    private int count = 1;

    void Start()
    {
        StartCoroutine(SpawnText());
    }

    IEnumerator SpawnText()
    {
        while (true) // ���� �ݺ�
        {
            textUI.text += $"."; // ���ο� �ؽ�Ʈ �߰�
            count++;
            yield return new WaitForSeconds(1f); // 1�� ���
        }
    }
}
