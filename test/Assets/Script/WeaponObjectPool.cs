using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectPool : MonoBehaviour
{
    public GameObject[] effectPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject GetFromPool() //이펙트 가져오는 오브젝트 풀링
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return Instantiate(effectPrefab[0]); //나중에 이펙트별로 정리
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
