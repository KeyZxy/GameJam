using UnityEngine;
using System;
using System.Collections;
/// <summary>  
/// ���߿�����  
/// </summary>  
public class LinesDrawer : MonoBehaviour
{
    public GameObject linePrefab; // ����Ԥ��  

    public LayerMask cantDrawOverLayer; // ���ܻ����ڴ˲���  
    private int cantDrawOverLayerIndex; // ���ܻ��Ʋ������  

    [Space(30)]
    private Color lineColor = Color.green; // ������ɫ  
    public float linePointsMinDistance; // ��֮�����С����  
    public float lineWidth; // �������  

    private Line currentLine; // ��ǰ���Ƶ�����  
    private Camera cam; // �������  

    private void Start()
    {
        cam = Camera.main; // ��ȡ�������  
       
    }

    private void Update()
    {
        // ����������  
        if (Input.GetMouseButtonDown(0))
            BeginDraw(); // ��ʼ����  
        if (currentLine != null)
            Draw(); // ���л���  
        if (Input.GetMouseButtonUp(0))
            EndDraw(); // ��������  
    }

    // �����߼�-----------------------------------------------------------------------  

    // ��ʼ����  
    void BeginDraw()
    {
        // ʵ�������A�O  
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        // ������������  
        //currentLine.UsePhysics(false); // �ر�����ģ��  
        currentLine.SetLineColor(lineColor); // ����������ɫ  
        currentLine.SetPointsMinDistance(linePointsMinDistance); // ���õ�֮�����С����  
        currentLine.SetLineWidth(lineWidth); // �����������  
    }

    // ���߽�����  
    void Draw()
    {
        // ��ȡ��ǰ���λ�ò�ת��Ϊ��������  
        Vector3 screenPos = Input.mousePosition; // �����Ļ����  
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0; // ȷ��z��Ϊ0  

        // ����������ת��Ϊ�����LinesDrawer�ľֲ�����  
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        // ����µ�  
        currentLine.AddPoint(localPos);
        //������Ч
        if (lineColor == Color.red)
        {
            currentLine.CreateFireParticle(localPos);
        }
    }

    // ���߽���  

    void EndDraw()
    {
        if (currentLine == null) return;

        currentLine.SetLineColor(lineColor);

        if (lineColor == Color.red)
        {
            currentLine.tag = "Fire";
            Rigidbody2D currentRigidbody = currentLine.gameObject.AddComponent<Rigidbody2D>();
            currentRigidbody.bodyType = RigidbodyType2D.Static;
            // ��3������ٵ�ǰ����

            // ֹͣ���л�������Ч��
            currentLine.StopFireParticles();

            // ��������
            Destroy(currentLine.gameObject, 3f);

        }

        else if (lineColor == Color.blue)
        {
            currentLine.tag = "Water";
            currentLine.StartDroppingWaterParticles(); // ��ʼ��ˮ�߲��Ϊ����  
        }
        else if (lineColor == Color.green)
        {
            currentLine.tag = "Air";
            currentLine.StartFloatingAirParticles();
        }
        else if (lineColor == Color.yellow)
        {
            currentLine.tag = "Dirt";
            Rigidbody2D currentRigidbody = currentLine.gameObject.AddComponent<Rigidbody2D>();
            if (currentRigidbody != null)
            {
                currentRigidbody.gravityScale = 1;
            }
        }


        if (currentLine.pointCount < 2)
        {
            Destroy(currentLine.gameObject);
        }

        currentLine = null;
    }

}