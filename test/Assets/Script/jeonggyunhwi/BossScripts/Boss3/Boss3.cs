using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Boss3 : MonoBehaviour
{

    public int Hp = 100000;
    public GameObject shield;
    public float current_time = 0;
    public float move_time = 0;
    public GameObject pos1;
    public GameObject pos2;

    public GameObject target;
    private Vector2 dir_V;
    private float random_dir_x;
    private float random_dir_y;
    [SerializeField]
    private GameObject MiniShip1_Prefab;
    [SerializeField]
    private GameObject MiniShip2_Prefab;




    private void Awake()
    {
        BossPoolManager.Instance.CreatePool(MiniShip1_Prefab, 10);
        BossPoolManager.Instance.CreatePool(MiniShip2_Prefab, 10);
    }

    IEnumerator SpawnMiniShip1()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject miniship1 = BossPoolManager.Instance.Get(MiniShip1_Prefab);
            miniship1.transform.position = pos1.transform.position;
        }
    }
    IEnumerator SpawnMiniShip2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameObject miniship2 = BossPoolManager.Instance.Get(MiniShip2_Prefab);
            miniship2.transform.position = pos2.transform.position;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnMiniShip1());
        StartCoroutine(SpawnMiniShip2());
    }
    
    // Update is called once per frame
    void Update()
    {
        move_time += Time.deltaTime;
        if(move_time >= 2)
        {
            random_dir_x = Random.Range(-100, 100);
            random_dir_y = Random.Range(-100, 100);
            dir_V = new Vector2(random_dir_x, random_dir_y);
            move_time = 0;
        }
        transform.Translate(dir_V.normalized * 1.5f * Time.deltaTime);
        if (shield.GetComponent<BossShield>().Shield_Point == 0)
        {
            current_time += Time.deltaTime;

            if (current_time >= 30)
            {
                Debug.Log("½¯µå Àç»ý¼º");
                shield.SetActive(true);
                shield.GetComponent<BossShield>().Shield_Point = 30;
                current_time = 0;
            }
        }
    }

    public void Damage(int att)
    {
        Hp -= att;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }




}
