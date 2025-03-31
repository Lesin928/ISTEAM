using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    // UI 이미지 컴포넌트
    public Image imageToChange;

    // 변경할 이미지들 (스프라이트 배열)
    public Sprite[] sprites;

    // 클릭 시 이미지 변경 함수
    public void ChangeImage(int index)
    {
        // 스프라이트 배열에서 인덱스를 사용하여 이미지 변경
        if (index >= 0 && index < sprites.Length)
        {
            imageToChange.sprite = sprites[index];
        }
    }
}
