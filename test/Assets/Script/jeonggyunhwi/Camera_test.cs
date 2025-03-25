using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera_test : MonoBehaviour
{
    private const float ZoomSpeed = 0.2f; // 한번의 줌 입력의 줌 되는 정도
    private const float MinZoomSize = 0.5f; // 최소 카메라 사이즈
    private const float MaxZoomSize = 10.0f; //  최대 카메라 사이즈
    private const float ZoomOutSpeed = 0.01f;
    private float _targetZoomSize; // 목표 카메라 크기
    private Camera _camera; // 카메라 컴포넌트

    public GameObject black_hole;
    Vector3 TargetPos;

    public GameObject Target;               // 카메라가 따라다닐 타겟
    public float CameraSpeed = 10.0f;
    
    //public float offsetX = 0.0f;            // 카메라의 x좌표
    //public float offsetY = 10.0f;           // 카메라의 y좌표
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
        // 타겟의 x, y, z 좌표에 카메라의 좌표를 더하여 카메라의 위치를 결정
        TargetPos = new Vector3(
            Target.transform.position.x ,
            Target.transform.position.y,
            Target.transform.position.z - 10
            );

        // 카메라의 움직임을 부드럽게 하는 함수(Lerp)
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
    }
    
    public void ControllerZoomMinus()
    {
        

        // 카메라 크기를 마우스 스크롤 입력에 따라 변경하여 확대/축소
        var newSize = _camera.orthographicSize - 1 * ZoomSpeed;

        // 카메라 크기 값을 최소값과 최대값 사이로 유지
        _targetZoomSize = Mathf.Clamp(newSize, MinZoomSize, MaxZoomSize);
    }

    public void ControllerZoomPlus()
    {
        

        // 카메라 크기를 마우스 스크롤 입력에 따라 변경하여 확대/축소
        var newSize = _camera.orthographicSize + 1 * ZoomOutSpeed;

        // 카메라 크기 값을 최소값과 최대값 사이로 유지
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

        // 카메라 크기 갱신
        _camera.orthographicSize = _targetZoomSize;

        // 줌 비율에 의한 카메라 위치 조정
        //currentCameraPosition += offsetCamera;
        //cameraTransform.position = currentCameraPosition;
    }
}
