using UnityEngine;

public class WeaponEvent : MonoBehaviour
{ 
    public void OnClickBt(int index)
    {
        WeaponManager.Instance.RegisterWeaponNumber(index); 
    }

}
