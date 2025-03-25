using System.Collections.Generic;
using UnityEngine;

public class BossObjectPool : MonoBehaviour
{
    private GameObject prefab;

    //��Ȱ��ȭ�� ������Ʈ�� �����ϴ� ť
    private Queue<GameObject> pool;

    // Ǯ���� ������Ʈ���� �θ� Ʈ������
    private Transform parent;

    // ������: �����հ� �ʱ� ũ�⸦ �޾� Ǯ �ʱ�ȭ
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

    // ���ο� ������Ʈ�� �����Ͽ� Ǯ�� �߰��ϴ� private �޼���
    private void CreateNewObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parent);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    // Ǯ���� ��� ������ ������Ʈ�� �������� �޼���
    // Ǯ�� ��������� ���� ����
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

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ�ϴ� �޼���
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
