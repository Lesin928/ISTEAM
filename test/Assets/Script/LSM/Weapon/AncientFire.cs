using UnityEngine;
using UnityEngine.Pool;

//고대무기 불 공격방식 구현한 코드
//오브젝트풀링을 통해 불 이펙트 발사
public class AncientFire : Weapon
{
    // public WeaponObjectPool effectPool; // 이펙트 풀링 시스템 

    public override void WeaponAttack() //무기별 발사방법 구현
    {
        //GameObject effect = effectPool.GetFromPool(weapon);
        //생성한 effcet를 발사
        //
    }
}