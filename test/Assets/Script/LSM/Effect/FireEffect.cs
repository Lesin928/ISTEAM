using UnityEngine;
using UnityEngine.Pool;
 
//불 이펙트의 충돌 처리
public class FireEffect : MonoBehaviour
{ 

    void FireDestroy()
    {
        Destroy(gameObject);
    }


}
