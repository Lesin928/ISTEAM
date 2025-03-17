using UnityEngine;

public class PBullet : MonoBehaviour
{
    public float Speed = 4.0f; 
    public GameObject effect;

    void Update()
    {
        //�̻��� ���ʹ������� �����̱�
        //���� ���� * ���ǵ� * Ÿ��
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    //ȭ������� �������
    private void OnBecameInvisible()
    {
        //�ڱ� �ڽ� �����
        Destroy(gameObject);
    } 

}