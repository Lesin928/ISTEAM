using UnityEngine;

public class Solar : Character
{
    public GameObject effect;  
    private void Awake()
    {
        SetHp(500); //최대체력  
    }

    //HP 관련
    //최대 체력을 설정하고 현재 체력을 그와 맞게 세팅
    public override void SetHp(float hp)
    {
        this.maxHp = hp;
        this.currentHp = hp;
    }

    public override float GetHp()
    {
        return currentHp;
    }

    //치명타 판정 이후 방어력만큼 피해를 경감하여 데미지 받음 
    
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
