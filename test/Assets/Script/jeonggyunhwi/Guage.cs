using UnityEngine;
using UnityEngine.UI;

public class Guage : MonoBehaviour
{
    public Transform boss;
    public GameObject gauge_bar;
    //���� �ʻ�� ������
    // gameObject.SetActive(true / false)
    // isPattern Ȯ�� �� ������ Ʈ���̸� (�������� ����) ������ Ȱ��ȭ
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
