using UnityEngine;

public class ValveRotationZ : MonoBehaviour
{
    [Header("��������� ��������")]
    public float rotationSpeed = 1f;
    public float minAngle = -180f;
    public float maxAngle = 180f;

    [Header("����� � ������")]
    public Transform drill;
    public float minDrillHeight = 0.5f;
    public float maxDrillHeight = 2f;

    // ��������� ����������
    private float _currentZAngle;
    private float _initialYRotation; // ��� ���������� ���������� Y=-90�
    private Vector3 _initialDrillPosition;
    private bool _isDragging;

    void Start()
    {
        _currentZAngle = transform.localEulerAngles.z;
        _initialYRotation = transform.localEulerAngles.y; // ��������� Y=-90�
        if (drill != null)
            _initialDrillPosition = drill.position;
    }

    void Update()
    {
        HandleMouseInput();
        UpdateDrillPosition();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickOnValve())
                _isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        if (_isDragging)
        {
            RotateValve();
        }
    }

    bool IsClickOnValve()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) && hit.transform == transform;
    }

    void RotateValve()
    {
        float deltaX = Input.GetAxis("Mouse X") * rotationSpeed * 50f;
        _currentZAngle = Mathf.Clamp(_currentZAngle + deltaX, minAngle, maxAngle);

        // ��������� �������� ������ �� Z, �������� Y=-90�
        transform.localEulerAngles = new Vector3(0, _initialYRotation, _currentZAngle);
    }

    void UpdateDrillPosition()
    {
        if (drill == null) return;

        // ������� ��������� ������ � ����������� �������
        float t = Mathf.InverseLerp(minAngle, maxAngle, _currentZAngle);
        float targetHeight = Mathf.Lerp(minDrillHeight, maxDrillHeight, t);

        // ��������� X � Z ������� ������, ������ ������ Y
        drill.position = new Vector3(
            _initialDrillPosition.x,
            targetHeight,
            _initialDrillPosition.z
        );
    }
}