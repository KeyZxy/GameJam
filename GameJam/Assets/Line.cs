using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Line : MonoBehaviour
{


    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    // public Rigidbody2D rigidBody;
    public ParticleSystem fireParticleSystem; // ��������ϵͳ  
    private List<ParticleSystem> fireParticlesList = new List<ParticleSystem>(); // ����ϵͳʵ���б�  

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointCount = 0;

    private float pointsMinDistance = 0.1f;
    private float circleColliderRadius;

    // �����ɫ�ֶ�  
    public Color lineColor; // ���浱ǰ��������ɫ  

    public GameObject waterParticlePrefab; // ˮ����Ԥ��  
    public float dropSpeed = 2f; // ˮ���������ٶ�  
    //private bool isDropping = false;

    public GameObject AirParticlePrefab; // ��������Ԥ��  
    public float floatSpeed = 2f; // �������������ٶ�  
                                  // private bool isfloatping = false;

    public void StartDroppingWaterParticles()
    {
        // ��������ˮ����  
        foreach (Vector2 point in points)
        {
            GameObject waterParticle = Instantiate(waterParticlePrefab, point, Quaternion.identity);
            // ���� Rigidbody ��ʹ��������  
            Rigidbody2D rb = waterParticle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1; // ��������  
            }
        }

        // ���� Line ����  
        Destroy(gameObject);
    }

    public void StartFloatingAirParticles()
    {
        // ����������������  
        foreach (Vector2 point in points)
        {
            GameObject AirParticle = Instantiate(AirParticlePrefab, point, Quaternion.identity);
            // ���� Rigidbody ��ʹ��������  
            Rigidbody2D rb = AirParticle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = -1; // ��������  
                rb.velocity = Vector2.zero; // ��ʼ���ٶ�Ϊ�� 
            }
        }

        // ���� Line ����  
        Destroy(gameObject);
    }
    private void Update()
    {
        // �ҵ������������Ӳ�ʩ�����ϵ�����  
        foreach (GameObject airParticle in GameObject.FindGameObjectsWithTag("Air")) // ��������������һ����ǩ  
        {
            Rigidbody2D rb = airParticle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 upwardForce = new Vector2(0, 20f); // ������Ҫ����������С  
                rb.AddForce(upwardForce, ForceMode2D.Force);
            }
        }
    }
    //������Ч
    public void CreateFireParticle(Vector2 position)
    {
        ParticleSystem fireInstance = Instantiate(fireParticleSystem, position, Quaternion.identity);
        fireInstance.transform.position = new Vector3(position.x, position.y, 0);
        fireParticlesList.Add(fireInstance);
        fireInstance.Play();
    }


    public void StopFireParticles()
    {
        StartCoroutine(StopAndDestroyParticlesAfterDelay(2.9f)); // �ȴ�3���ֹͣ����������
    }

    private IEnumerator StopAndDestroyParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // �ȴ�3��

        foreach (var fireParticle in fireParticlesList)
        {
            if (fireParticle != null)
            {
                fireParticle.Stop(); // ֹͣ���Ӳ���
                Destroy(fireParticle.gameObject, 0.1f); // �ӳ�1�����٣���ȷ������Ч����ȫֹͣ
            }
        }

        fireParticlesList.Clear(); // ����б�
    }
    private void UpdateLineRenderer()
    {
        lineRenderer.positionCount = points.Count;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
    }


    public void AddPoint(Vector2 newPoint)
    {
        if (pointCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        ++pointCount;

        var circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;

        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);

        if (pointCount > 1)
            edgeCollider.points = points.ToArray();
    }

    public Vector2 GetLastPoint()
    {
        return lineRenderer.GetPosition(pointCount - 1);
    }

    //public void UsePhysics(bool usePhysics)
    //{
    //    rigidBody.isKinematic = !usePhysics;
    //}

    public void SetLineColor(Color color)
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineColor = color; // ����lineColor�ֶ�  
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;
        edgeCollider.edgeRadius = circleColliderRadius;
    }
}