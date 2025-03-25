using UnityEngine;
using UnityEngine.UI;

public class Guage : MonoBehaviour
{
    public Transform boss;
    public GameObject gauge_bar;
    //보스 필살기 게이지
    // gameObject.SetActive(true / false)
    // isPattern 확인 후 패턴이 트루이면 (패턴으로 들어가면) 게이지 활성화
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Slider>().value += Time.deltaTime; 
        transform.position = boss.transform.position + new Vector3(0, 1.3f, 0);
        if(gameObject.GetComponent<Slider>().value == 5)
        {
            gameObject.GetComponent<Slider>().value = 0;
        }
    }
}
