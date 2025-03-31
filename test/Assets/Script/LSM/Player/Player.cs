using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    

    public int playerLevel = 1; //플레이어 레벨
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            //스탯 초기화
            armor = 5; //방어력
            attack = 10; //공격력
            attackSpeed = 0.5f; //공격속도
            moveSpeed = 5; //이동속도  
            critical = 0.2f; //치명타 확률
            criticalDamage = 1.5f; //치명타 피해 
        }
        maxHp = 1000000; //최대체력
        currentHp = maxHp; //현재 체력
    }
    public void LevelUp()
    {
        playerLevel++;
        SetAttack(1);
        SetArmor(1);
        HealHp(maxHp / 4f);
    }
    
    public override float GetHp()
    {
        return currentHp;
    }

    //방어력만큼 피해를 경감하여 데미지 받음
    //오버플로우 방지를 위해 double 명시적 변환 후 계산
    public override void TakeDamage(float damage)
    {
        AudioManager.Instance.PlaySFX("Player", "Player_Hit");
        if (damage == 0)
        {
            damage = 1;
        }

        currentHp -= (float)((double)damage * (double)damage / ((double)armor + (double)damage));

        if (currentHp <= 0)
        {
            currentHp = 0;
            //플레이어 사망
            AudioManager.Instance.PlaySFX("Player", "Player_Death");
            Destroy(gameObject);
            //게임오버 처리
            GameManager.Instance.OverGame();
        }
    }

    public void SetMaxHp(float point)
    {
        maxHp += point;
    }
    //체력을 회복함
    public void HealHp(float Heal)
    {
        if (maxHp < currentHp + Heal)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp += Heal;
        }
    }
    public void SetArmor(float point)
    {
        armor += point;
    }    
    public void SetAttack(float point)
    {
        attack += point;
    }
    public void SetAttackSpeed(float point)
    {
        //기본 공격 빈도: 0.5 공격/초
        //point % 증가 후 공격 빈도: 0.5 × 1.2 = 0.6 공격/초    
        //새로운 공격 간격: 1 / 0.6 ≈ 1.67초
        attackSpeed = attackSpeed / (1 + (point/100)); 
        attackSpeed = (float)Math.Floor(attackSpeed * 1000) / 1000; //소수점 3자리 까지만
    }
    public void SetMoveSpeed(float point)
    {
        moveSpeed += point;
    }
    public void SetCritical(float point)
    {
        critical *= point / 100;
        if (critical > 1f)
        {
            critical = 1f;
        }        
    }
    public void SetCriticalDamage(float point)
    {
        criticalDamage *= point / 100f;
    }


}

