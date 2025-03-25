using UnityEngine;

public class PushableObject : MonoBehaviour, IPushable
{
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Push(Vector2 force)
    {
        if (rb != null)
        { 
            rb.AddForce(force, ForceMode2D.Impulse); // 밀려나는 힘 적용 
        }
    }
     
}