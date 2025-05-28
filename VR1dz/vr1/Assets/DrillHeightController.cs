using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrillHeightController : MonoBehaviour
{
    public Transform valve;    // ������� (���������� UpperValve ����)
    public Transform drill;    // ������ (���������� Drill ����)
    public float maxHeight = 2f; // ������������ ������
    public float minHeight = 0.5f; // ����������� ������
    public float rotationToHeightRatio = 0.01f; // ����������������

    private float initialDrillY;

    void Start()
    {
        initialDrillY = drill.position.y;
    }

    void Update()
    {
        // ���� ������� � ��������� [-180, 180]
        float valveAngle = valve.localEulerAngles.z;
        if (valveAngle > 180f) valveAngle -= 360f;

        // ����������� ���� � ������ � �������������
        float targetY = initialDrillY + (valveAngle * rotationToHeightRatio);
        targetY = Mathf.Clamp(targetY, minHeight, maxHeight);

        // ��������� ����� ������
        drill.position = new Vector3(drill.position.x, targetY, drill.position.z);
    }
}