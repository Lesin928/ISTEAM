using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 

//플레이어의 자동 공격을 구현한 클래스 
public class AutoAttack : MonoBehaviour
{
    private HashSet<Weapon> weapons = new HashSet<Weapon>(); //무기 
    private Coroutine attackCoroutine;

    private void Start()
    {
        AddWeapon(GetComponentInChildren<Weapon>());
    }

    private void Update()
    {   
        if (weapons.Count == 0) //무기가 없다
        {
            if (attackCoroutine != null) //공격중이면 코루틴 중지
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
        else //무기가 있다
        {
            if (attackCoroutine == null) //공격하고 있지 않으면 코루틴 시작
            { 
                attackCoroutine = StartCoroutine(AttackRoutine());
            }
            
        }
    }


    IEnumerator AttackRoutine()
    { 
        while (weapons.Count > 0)
        { 
            foreach (Weapon weapon in weapons)
            { 
                weapon.WeaponAttack();// 무기별 공격 실행
            }
            // 가장 빠른 무기의 attackSpeed 만큼 대기 후 다시 실행
            float minAttackSpeed = Mathf.Min(weapons.Select(w => w.attackSpeed).ToArray());
            yield return new WaitForSeconds(minAttackSpeed);

        }
        attackCoroutine = null; // 무기 리스트가 비어지면 코루틴 종료
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (weapons.Contains(weapon))
        {
            weapons.Remove(weapon);
            if (weapons.Count == 0 && attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }


}
