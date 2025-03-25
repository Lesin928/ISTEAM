using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera_test : MonoBehaviour
{
    private const float ZoomSpeed = 0.2f; // �ѹ��� �� �Է��� �� �Ǵ� ����
    private const float MinZoomSize = 0.5f; // �ּ� ī�޶� ������
    private const float MaxZoomSize = 10.0f; //  �ִ� ī�޶� ������
    private const float ZoomOutSpeed = 0.01f;
    private float _targetZoomSize; // ��ǥ ī�޶� ũ��
    private Camera _camera; // ī�޶� ������Ʈ

    public GameObject black_hole;
    Vector3 TargetPos;

    public GameObject Target;               // ī�޶� ����ٴ� Ÿ��
    public float CameraSpeed = 10.0f;
    
    //public float offsetX = 0.0f;            // ī�޶��� x��ǥ
    //public float offsetY = 10.0f;           // ī�޶��� y��ǥ
    //public float offsetZ = -10.0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = GetComponent<Camera>();
        _targetZoomSize = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        // Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
        TargetPos = new Vector3(
            Target.transform.position.x ,
            Target.transform.position.y,
            Target.transform.position.z - 10
            );

        // ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
    
    public void ControllerZoomMinus()
    {
        

        // ī�޶� ũ�⸦ ���콺 ��ũ�� �Է¿� ���� �����Ͽ� Ȯ��/���
        var newSize = _camera.orthographicSize - 1 * ZoomSpeed;

        // ī�޶� ũ�� ���� �ּҰ��� �ִ밪 ���̷� ����
        _targetZoomSize = Mathf.Clamp(newSize, MinZoomSize, MaxZoomSize);
    }

    public void ControllerZoomPlus()
    {
        

        // ī�޶� ũ�⸦ ���콺 ��ũ�� �Է¿� ���� �����Ͽ� Ȯ��/���
        var newSize = _camera.orthographicSize + 1 * ZoomOutSpeed;

        // ī�޶� ũ�� ���� �ּҰ��� �ִ밪 ���̷� ����
        _targetZoomSize = Mathf.Clamp(newSize, MinZoomSize, MaxZoomSize);
    }
    public void UpdateZoom()
    {
        if (Math.Abs(_targetZoomSize - _camera.orthographicSize) < Mathf.Epsilon)
        {
            return;
        }

        //var mouseWorldPos = _camera.ScreenToWorldPoint(black_hole.transform.position);
        //var cameraTransform = transform;
        //var currentCameraPosition = cameraTransform.position;
        //var offsetCamera = mouseWorldPos - currentCameraPosition - (mouseWorldPos - currentCameraPosition) / 
        //    (_camera.orthographicSize / _targetZoomSize);

        // ī�޶� ũ�� ����
        _camera.orthographicSize = _targetZoomSize;

        // �� ������ ���� ī�޶� ��ġ ����
        //currentCameraPosition += offsetCamera;
        //cameraTransform.position = currentCameraPosition;
    }
}
