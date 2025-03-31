using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine; 

//플레이어의 자동 공격을 구현한 클래스 
public class AutoAttack : MonoBehaviour
{
    private HashSet<Weapon> weapons = new HashSet<Weapon>(); //무기 
    private Coroutine attackCoroutine;
    private int lastWeaponCount = 0;

    private void Update()
    {
        if (weapons.Count == 0) //무기가 없다
        {
            if (attackCoroutine != null) //공격중이면 코루틴 중지
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
            else //공격중 아니면 무기 등록
            {
                Weapon[] weapons = GetComponentsInChildren<Weapon>();
                foreach (Weapon weapon in weapons)
                {
                    AddWeapon(weapon);
                }
            }
        }
        else //무기가 있다
        {
            Debug.Log("Attack");
            if (attackCoroutine == null || weapons.Count != lastWeaponCount) //공격하고 있지 않거나 무기 수가 변하면 코루틴 시작
            {
                Debug.Log("Attack12123");
                if (attackCoroutine != null)
                {
                    Debug.Log("Attack121231231231");
                    StopCoroutine(attackCoroutine); 
                }
                attackCoroutine = StartCoroutine(AttackRoutine());
            }
        }
        lastWeaponCount = weapons.Count;
    }

    IEnumerator AttackRoutine()
    {
        while (weapons.Count > 0)
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.WeaponAttack();// 무기별 공격 실행 
            }
            yield return new WaitForSeconds(0.1f);
            //0.1초마다 공격 실행
        }
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
