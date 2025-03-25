
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenes_Change : MonoBehaviour
{

    public Image image;
    private int flag = 0;
    private int fade_flag = 0;
    // 컴포넌트
    public Camera _camera;
    private float fade_out_time = 0.3f;
    private float fade_in_time = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (flag == 1)
        {
            Debug.Log("flag = 1");
            _camera.GetComponent<Camera_test>().ControllerZoomPlus();
            _camera.GetComponent<Camera_test>().UpdateZoom();
            if(fade_flag == 0 && _camera.orthographicSize > 1.3)
            {
                StartCoroutine(FadeIn(1, 0));
                //Debug.Log("1,0 코루틴 시작");
                fade_flag = 1;
            }
            
            if (_camera.orthographicSize > 5.0)
            {
                _camera.orthographicSize = 5.0f;
                flag = 0;
                fade_flag = 0;
            }
            
        }
    }
    private void FixedUpdate()
    {

    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{

    //    while (_camera.orthographicSize <= 5.0)
    //    {
    //        _camera.GetComponent<Camera_test>().ControllerZoomPlus();
    //        _camera.GetComponent<Camera_test>().UpdateZoom();

    //        if (_camera.orthographicSize >= 5.0)
    //        {
    //            Debug.Log("위치변경");
    //            // _camera.orthographicSize = 5.0f;
    //            break;
    //        }
    //    }

    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            StartCoroutine(FadeOut(0,1));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            _camera.GetComponent<Camera_test>().ControllerZoomMinus();
            _camera.GetComponent<Camera_test>().UpdateZoom();

            if (_camera.orthographicSize <= 0.7)
            {

                collision.gameObject.transform.position = new Vector3(0, 0, 0);
                flag = 1;
                
            }
        }
    }
    

    IEnumerator FadeOut(float start, float end)
    {
        float curruntTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
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
