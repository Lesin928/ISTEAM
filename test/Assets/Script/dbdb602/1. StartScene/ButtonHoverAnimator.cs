using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // Animator ��������
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        anim.SetBool("isHover", true); // Hover �ִϸ��̼� ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("isHover", false); // Idle �ִϸ��̼����� ����
    }
}

