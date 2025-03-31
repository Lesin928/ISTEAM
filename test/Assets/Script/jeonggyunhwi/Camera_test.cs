using Unity.Cinemachine;
using UnityEngine;

public class Camera_test : MonoBehaviour
{
    private const float ZoomSpeed = 4f; // �� �ӵ� (���콺 �� ���)
    private const float MinZoomSize = 1f; // �ּ� �� �� (�� ������)
    private const float MaxZoomSize = 10f; // �ִ� �� �� (�� �ָ�)

    private CinemachineCamera _virtualCamera;
    private float _targetZoomSize; // ��ǥ �� ũ��

    
    public float Fov;
    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineCamera>();
    }

    void Start()
    {
        
        Fov = _virtualCamera.Lens.OrthographicSize;
        _targetZoomSize = _virtualCamera.Lens.OrthographicSize;
    }

    void Update()
    {
        
    }
    public void ControllerZoomMinus()
    {
        Debug.Log("����");
        // �� �ƿ�
        var newSize = _virtualCamera.Lens.OrthographicSize - ZoomSpeed;
        _targetZoomSize = Mathf.Clamp(newSize, MinZoomSize, MaxZoomSize);
    }

    public void ControllerZoomPlus()
    {
        // �� ��
        var newSize = _virtualCamera.Lens.OrthographicSize + ZoomSpeed;
        _targetZoomSize = Mathf.Clamp(newSize, MinZoomSize, MaxZoomSize);
    }

    public void UpdateZoom()
    {
        if (Mathf.Abs(_targetZoomSize - _virtualCamera.Lens.OrthographicSize) < Mathf.Epsilon)
        {
            return;
        }
        _virtualCamera.Lens.OrthographicSize = Mathf.Lerp(
                _virtualCamera.Lens.OrthographicSize, _targetZoomSize, Time.deltaTime * 5f);
        Fov = _virtualCamera.Lens.OrthographicSize;
    }
}