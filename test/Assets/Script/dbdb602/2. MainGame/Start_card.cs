using UnityEngine;
using UnityEngine.UI;

public class Start_card : MonoBehaviour
{
    public GameObject Canvas; // ��Ȱ��ȭ or Ȱ��ȭ �� ĵ����

    void Start()
    {
        if(Canvas != null)
        {
            // ���� �ÿ� ĵ���� ���̰�
            Canvas.SetActive(true);
        }
    }

    // Ŭ�� �� ȣ��� �޼���
    public void OnCardClick(int index)
    {
        Debug.Log("ī��: " + index);
        if (Canvas != null)
        {
            Debug.Log("ĵ���� ��Ȱ��ȭ �õ� ��...");
            Canvas.SetActive(false);
            Debug.Log("ĵ���� ����: " + Canvas.activeSelf);
        }
        else
        {
            Debug.LogWarning("ĵ������ null�Դϴ�.");
        }

    }
}
