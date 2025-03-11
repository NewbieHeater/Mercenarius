using UnityEngine;

public enum AttackShape
{
    Spherical,
    Hemisphere,
    Box
}

[RequireComponent(typeof(LineRenderer))]
public class AttackRangeIndicator : MonoBehaviour
{
    [Header("공격 형태 및 설정")]
    public AttackShape shape = AttackShape.Spherical;

    [Tooltip("구형/반구형 공격일 경우 반지름")]
    public float radius = 5f;

    [Tooltip("박스 공격일 경우 XZ 평면의 절반 크기 (z값은 공격 거리)")]
    public Vector3 boxHalfExtents = new Vector3(2f, 0, 3f);

    [Header("LineRenderer 설정")]
    public int segments = 100;        // 원/반원 그릴 때 세그먼트 수
    public Color lineColor = Color.red;
    public float lineWidth = 0.1f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        // world 좌표를 사용하도록 변경 (이제 Indicator의 transform.position이 반영됨)
        lineRenderer.useWorldSpace = true;
    }

    private void Update()
    {
        switch (shape)
        {
            case AttackShape.Spherical:
                DrawCircle();
                break;
            case AttackShape.Hemisphere:
                DrawHemisphere();
                break;
            case AttackShape.Box:
                DrawBox();
                break;
        }
    }

    // world 좌표를 기준으로 원을 그림
    private void DrawCircle()
    {
        lineRenderer.positionCount = segments + 1;
        float angle = 0f;
        Vector3 center = transform.position; // Indicator의 현재 위치를 중심으로
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, center + new Vector3(x, 0, z));
            angle += 360f / segments;
        }
    }

    // world 좌표를 기준으로 반원을 그림
    private void DrawHemisphere()
    {
        int halfSegments = segments / 2;
        lineRenderer.positionCount = halfSegments + 1;
        Vector3 center = transform.position;
        // -90도부터 +90도까지 (전방 반원)
        float angle = -90f;
        for (int i = 0; i <= halfSegments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float z = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            lineRenderer.SetPosition(i, center + new Vector3(x, 0, z));
            angle += 180f / halfSegments;
        }
    }

    // world 좌표를 기준으로 박스를 그림
    private void DrawBox()
    {
        lineRenderer.positionCount = 5;
        Vector3 center = transform.position;
        Vector3[] corners = new Vector3[5];
        corners[0] = center + new Vector3(-boxHalfExtents.x, 0, -boxHalfExtents.z);
        corners[1] = center + new Vector3(-boxHalfExtents.x, 0, boxHalfExtents.z);
        corners[2] = center + new Vector3(boxHalfExtents.x, 0, boxHalfExtents.z);
        corners[3] = center + new Vector3(boxHalfExtents.x, 0, -boxHalfExtents.z);
        corners[4] = corners[0];

        lineRenderer.SetPositions(corners);
    }
}
