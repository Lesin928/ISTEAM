using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // Animator 가져오기
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("isHover", true); // Hover 애니메이션 실행
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("isHover", false); // Idle 애니메이션으로 복귀
    }
}

