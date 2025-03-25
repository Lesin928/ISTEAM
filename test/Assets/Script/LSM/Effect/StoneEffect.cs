using UnityEngine;
using UnityEngine.Pool;

//�� ����Ʈ�� �浹 ó��
public class StoneEffect : MonoBehaviour
{
    private Collider2D collider2D;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>(); 
    }

    private void EnableCollider()
    {
        if (collider2D != null)
        { 
            collider2D.enabled = true; 
        }
    }
    private void DisableCollider()
    {
        if (collider2D != null)
        {
            collider2D.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
