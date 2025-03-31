using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Pool;

public class BossPoolManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    private static BossPoolManager instance;
    public static BossPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("BossPoolManager");
                instance = go.AddComponent<BossPoolManager>();
                //DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    // ������ �̸��� Ű�� ����ϴ� Ǯ ��ųʸ�
    private Dictionary<string, BossObjectPool> pools = new Dictionary<string, BossObjectPool>();

    // ���ο� ������Ʈ Ǯ�� �����ϴ� �޼���
    // prefab: Ǯ���� ������, initialSize: �ʱ� Ǯ ũ��
    public void CreatePool(GameObject prefab, int initialSize)
    {
        string key = prefab.name;
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new BossObjectPool(prefab, initialSize, transform));
        }
    }

    // Ǯ���� ������Ʈ�� �������� �޼���
    // ��û�� �������� Ǯ�� ���ٸ� ���� ����
    public GameObject Get(GameObject prefab)
    {
        string key = prefab.name;
        if (!pools.ContainsKey(key))
        {
            CreatePool(prefab, 10);
        }
        return pools[key].Get();
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
    public void Return(GameObject obj)
    {
        string key = obj.name.Replace("(Clone)", "");
        if (pools.ContainsKey(key))
        {
            pools[key].Return(obj);
        }
    }  
}
