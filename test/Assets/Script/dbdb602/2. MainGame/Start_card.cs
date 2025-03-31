using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ô밡 ���۵� ���� ī�� ���� ��ũ��Ʈ
/// </summary>
public class Start_card : MonoBehaviour
{
    public GameObject Canvas; // ��Ȱ��ȭ or Ȱ��ȭ �� ĵ����

    public Image ProfileImage; //���� UI ��ܹ� ������
    public Image BossPlayerImage; //������ �÷��̾� ������
    public Sprite[] ProfileSprites; //������ �̹����� �迭

    void Start()
    {/*
        if(Canvas != null)
        {
            // ���� �ÿ� ĵ���� ���̰�
            Canvas.SetActive(true);
        }*/
    }

    // Ŭ�� �� ȣ��� �޼���
    public void OnCardClick(int index)
    {
        Debug.Log("ī�� �ε��� : " + index);
        // ī�� �ε����� �ش��ϴ� �̹����� ���� => ���ζ��� �÷��̾� ������
        if (ProfileImage != null && index >= 0 && index < ProfileSprites.Length)
        {
            ProfileImage.sprite = ProfileSprites[index];
        }

        // ī�� �ε����� �ش��ϴ� �̹����� ���� => �������� �÷��̾� ������
        if (BossPlayerImage != null && index >= 0 && index < ProfileSprites.Length)
        {
            BossPlayerImage.sprite = ProfileSprites[index];
        }

        if (Canvas != null)
        {
            Canvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ĵ������ null�Դϴ�.");
        }  
    }
}
