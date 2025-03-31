using System;
using System.Collections;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    public float time = 0f;
    public float fadeDuration = 0.35f; // ���̵� �� ���� �ð�
    //public float Shield_Point { get; set; }

    public SpriteRenderer spriteRenderer;
    private Color initial_color;
    private Color current_color;
    Player player;
    Monster Boss3_Shied;
    private void Awake()
    {

        player = FindFirstObjectByType<Player>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Boss3_Shied = GetComponent<Monster>();
        //Shield_Point = 30;
        initial_color = new Color(255f, 255f, 255f, 0.35f);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = initial_color;
    }

    // Update is called once per frame
    void Update()
    {
        current_color = spriteRenderer.color;
        current_color.a = (float)((Boss3_Shied.GetComponent<Monster>().currentHp)*0.0035f); // 0.0035 -> �⺻ ���İ��� 0.35�̾
                                                                                            // ü�� 100�� �������� 0.35�� ���߱� ���� �ִ� ��ġ
        spriteRenderer.color = current_color;

        if (Boss3_Shied.GetComponent<Monster>().currentHp <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    public void ReActivate_Shield()
    {
        Boss3_Shied.GetComponent<Monster>().currentHp = Boss3_Shied.GetComponent<Monster>().maxHp; // ��ġ ����
        current_color = spriteRenderer.color;
        StartCoroutine(Shields());
        if (current_color.a <= 0.35f)
        {
            StopCoroutine(Shields());
        }

    }
    IEnumerator Shields()
    {
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;
        color.a = 0f;
        spriteRenderer.color = color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime * 0.1f; //�ǵ� �������� �ð� ����
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            if (color.a > 0.34f) // ����90 (90/255 �ٻ�ġ)
                break;
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
