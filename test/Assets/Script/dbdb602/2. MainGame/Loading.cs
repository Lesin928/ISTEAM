using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour
{
    public Canvas loadingCanvas; // �ε� ȭ�� ĵ����
    public GameObject ancientStartCard; // Ancient Start Card ������Ʈ
    private Animator ancientAnimator; // Animator ������Ʈ

    void Start()
    {
        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(true); // �ε� ĵ���� Ȱ��ȭ
        }

        if (ancientStartCard != null)
        {
            ancientAnimator = ancientStartCard.GetComponent<Animator>();
            ancientStartCard.SetActive(false); // Ancient Start Card ��Ȱ��ȭ
        }

        StartCoroutine(ShowLoadingScreen(3f)); // 3�� �� �ε� ����
    }

    IEnumerator ShowLoadingScreen(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (loadingCanvas != null)
        {
            loadingCanvas.gameObject.SetActive(false); // �ε� ĵ���� ��Ȱ��ȭ
        }

        if (ancientStartCard != null)
        {
            ancientStartCard.SetActive(true); // Ancient Start Card Ȱ��ȭ

            if (ancientAnimator != null)
            {
                ancientAnimator.Play("�ִϸ��̼�_�̸�"); // �ִϸ��̼� ����
            }
        }
    }
}
