using System.Collections;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    [SerializeField]
    private Transform ms;
    [SerializeField]
    private GameObject bullet;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(CreateBullet());
    }
    IEnumerator CreateBullet()
    {
        while (true)
        {
            Instantiate(bullet, ms.position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
