using System.Collections.Generic;
using UnityEngine;

public class WeaponObjectPool : MonoBehaviour
{
    public GameObject[] effectPrefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public GameObject GetFromPool() //����Ʈ �������� ������Ʈ Ǯ��
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return Instantiate(effectPrefab[0]); //���߿� ����Ʈ���� ����
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
