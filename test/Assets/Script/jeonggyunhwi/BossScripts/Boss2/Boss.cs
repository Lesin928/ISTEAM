using UnityEngine;

public class Boss : MonoBehaviour
{
    //���� ��ü, �༺ ������ ����
    public int Hp = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int att)
    {
        Hp -= att;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
