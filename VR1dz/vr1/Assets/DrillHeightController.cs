using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrillHeightController : MonoBehaviour
{
    public Transform valve;    // Вентиль (перетащите UpperValve сюда)
    public Transform drill;    // Сверло (перетащите Drill сюда)
    public float maxHeight = 2f; // Максимальная высота
    public float minHeight = 0.5f; // Минимальная высота
    public float rotationToHeightRatio = 0.01f; // Чувствительность

    private float initialDrillY;

    void Start()
    {
        initialDrillY = drill.position.y;
    }

    void Update()
    {
        // Угол вентиля в диапазоне [-180, 180]
        float valveAngle = valve.localEulerAngles.z;
        if (valveAngle > 180f) valveAngle -= 360f;

        // Преобразуем угол в высоту с ограничениями
        float targetY = initialDrillY + (valveAngle * rotationToHeightRatio);
        targetY = Mathf.Clamp(targetY, minHeight, maxHeight);

        // Применяем новую высоту
        drill.position = new Vector3(drill.position.x, targetY, drill.position.z);
    }
}