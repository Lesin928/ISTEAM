using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.Pool;

public class BossPoolManager : MonoBehaviour
{
    // 싱글톤 인스턴스
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

    // 프리팹 이름을 키로 사용하는 풀 딕셔너리
    private Dictionary<string, BossObjectPool> pools = new Dictionary<string, BossObjectPool>();

    // 새로운 오브젝트 풀을 생성하는 메서드
    // prefab: 풀링할 프리팹, initialSize: 초기 풀 크기
    public void CreatePool(GameObject prefab, int initialSize)
    {
        string key = prefab.name;
        if (!pools.ContainsKey(key))
        {
            pools.Add(key, new BossObjectPool(prefab, initialSize, transform));
        }
    }

    // 풀에서 오브젝트를 가져오는 메서드
    // 요청한 프리팹의 풀이 없다면 새로 생성
    public GameObject Get(GameObject prefab)
    {
        string key = prefab.name;
        if (!pools.ContainsKey(key))
        {
            CreatePool(prefab, 10);
        }
        return pools[key].Get();
    }

    // 사용이 끝난 오브젝트를 풀로 반환하는 메서드
    public void Return(GameObject obj)
    {
        string key = obj.name.Replace("(Clone)", "");
        if (pools.ContainsKey(key))
        {
            pools[key].Return(obj);
        }
    }  
}
