
using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes_Change : MonoBehaviour
{

    public Image image;
    private int flag = 0;
    private int fade_flag = 0;
    // ÄÄÆ÷³ÍÆ®
    public GameObject _camera;
    private float fade_out_time = 0.3f;
    private float fade_in_time = 2f;
    private GameObject blackhole;

    Player p;

    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GameObject.Find("CinemachineCamera");
        blackhole = GameObject.FindWithTag("BlackHole");
    }

    //Update is called once per frame
    void Update()
    {
        if (flag == 1)
        {
            _camera.GetComponent<Camera_test>().ControllerZoomPlus();
            _camera.GetComponent<Camera_test>().UpdateZoom();
            if (fade_flag == 0 && _camera.GetComponent<Camera_test>().Fov >= 1.5f)
            {
                StartCoroutine(FadeIn(1, 0));
                fade_flag = 1;
            }

            if (_camera.GetComponent<Camera_test>().Fov >= 9.5f)
            {
                _camera.GetComponent<Camera_test>().Fov = 10f;
                flag = 0;
                fade_flag = 0;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = gameObject.transform.position;
            collision.gameObject.GetComponent<PlayerControll>().Stop();


            _camera.GetComponent<Camera_test>().ControllerZoomMinus();
            _camera.GetComponent<Camera_test>().UpdateZoom();
            
            StartCoroutine(FadeOut(0, 1));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            _camera.GetComponent<Camera_test>().ControllerZoomMinus();
            _camera.GetComponent<Camera_test>().UpdateZoom();
            collision.gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0);

            if (_camera.GetComponent<Camera_test>().Fov <= 1.5f)
            {
                collision.gameObject.transform.position = GameObject.FindWithTag("WhiteHole").transform.position;
                collision.gameObject.GetComponent<PlayerControll>().StartMove();
                flag = 1;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.localScale = new Vector3(1f, 1f, 0);
        }
    }


    IEnumerator FadeOut(float start, float end)
    {
        float curruntTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            curruntTime += Time.deltaTime;
            percent = curruntTime / fade_out_time;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }

    }
    IEnumerator FadeIn(float start, float end)
    {
        float curruntTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            curruntTime += Time.deltaTime;
            percent = curruntTime / fade_in_time;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
    }

}
