using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    // UI �̹��� ������Ʈ
    public Image imageToChange;

    // ������ �̹����� (��������Ʈ �迭)
    public Sprite[] sprites;

    // Ŭ�� �� �̹��� ���� �Լ�
    public void ChangeImage(int index)
    {
        // ��������Ʈ �迭���� �ε����� ����Ͽ� �̹��� ����
        if (index >= 0 && index < sprites.Length)
        {
            imageToChange.sprite = sprites[index];
        }
    }
}
