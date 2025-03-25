using UnityEngine;
using UnityEngine.Pool;

//돌 이펙트의 충돌 처리
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
