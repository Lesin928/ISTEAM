using System.Collections.Generic;
using UnityEngine;

public class BossObjectPool : MonoBehaviour
{
    private GameObject prefab;

    //비활성화된 오프젝트를 보관하는 큐
    private Queue<GameObject> pool;

    // 풀링된 오브젝트들의 부모 트랜스폼
    private Transform parent;

    // 생성자: 프리팹과 초기 크기를 받아 풀 초기화
    public BossObjectPool(GameObject prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<GameObject>();

        for (int i = 0; i < initialSize; i++)
        {
            CreateNewObject();
        }
    }

    // 새로운 오브젝트를 생성하여 풀에 추가하는 private 메서드
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // 풀에서 사용 가능한 오브젝트를 가져오는 메서드
    // 풀이 비어있으면 새로 생성
    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            CreateNewObject();
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        return obj;
    }

    // 사용이 끝난 오브젝트를 풀로 반환하는 메서드
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
