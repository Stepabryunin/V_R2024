using UnityEngine;

public class ValveRotationZ : MonoBehaviour
{
    [Header("Настройки вращения")]
    public float rotationSpeed = 1f;
    public float minAngle = -180f;
    public float maxAngle = 180f;

    [Header("Связь с дрелью")]
    public Transform drill;
    public float minDrillHeight = 0.5f;
    public float maxDrillHeight = 2f;

    // Приватные переменные
    private float _currentZAngle;
    private float _initialYRotation; // Для сохранения начального Y=-90°
    private Vector3 _initialDrillPosition;
    private bool _isDragging;

    void Start()
    {
        _currentZAngle = transform.localEulerAngles.z;
        _initialYRotation = transform.localEulerAngles.y; // Сохраняем Y=-90°
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

        // Применяем вращение ТОЛЬКО по Z, сохраняя Y=-90°
        transform.localEulerAngles = new Vector3(0, _initialYRotation, _currentZAngle);
    }

    void UpdateDrillPosition()
    {
        if (drill == null) return;

        // Плавное изменение высоты с сохранением позиции
        float t = Mathf.InverseLerp(minAngle, maxAngle, _currentZAngle);
        float targetHeight = Mathf.Lerp(minDrillHeight, maxDrillHeight, t);

        // Сохраняем X и Z позиции сверла, меняем только Y
        drill.position = new Vector3(
            _initialDrillPosition.x,
            targetHeight,
            _initialDrillPosition.z
        );
    }
}