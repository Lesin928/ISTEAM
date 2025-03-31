using UnityEngine;

public class Solar : Character
{
    public GameObject effect;  
    private void Awake()
    {
        SetHp(500); //�ִ�ü��  
    }

    //HP ����
    //�ִ� ü���� �����ϰ� ���� ü���� �׿� �°� ����
    public override void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }

    public override float GetHp()
    {
        return currentHp;
    }

    //ġ��Ÿ ���� ���� ���¸�ŭ ���ظ� �氨�Ͽ� ������ ���� 
    
    public override void TakeDamage(float damage)
    {
        currentHp -= (float)((double)damage * (double)damage / ((double)armor + (double)damage));
        UI_GameManager.instance.UpdateSunHP(currentHp, maxHp);
        if (currentHp <= 0)
        {
            GameObject effec1t = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            GameManager.Instance.OverGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            TakeDamage(50); 
            monster.TakeDamage(10000f);
            if (currentHp <= 0)
            {
                GameManager.Instance.OverGame();
            }
        }
    }

}
