using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Line : MonoBehaviour
{


    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    // public Rigidbody2D rigidBody;
    public ParticleSystem fireParticleSystem; // 火焰粒子系统  
    private List<ParticleSystem> fireParticlesList = new List<ParticleSystem>(); // 粒子系统实例列表  

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointCount = 0;

    private float pointsMinDistance = 0.1f;
    private float circleColliderRadius;

    // 添加颜色字段  
    public Color lineColor; // 保存当前线条的颜色  

    public GameObject waterParticlePrefab; // 水粒子预设  
    public float dropSpeed = 2f; // 水粒子下落速度  
    //private bool isDropping = false;

    public GameObject AirParticlePrefab; // 气体粒子预设  
    public float floatSpeed = 2f; // 气体粒子下落速度  
                                  // private bool isfloatping = false;

    public void StartDroppingWaterParticles()
    {
        // 创建所有水粒子  
        foreach (Vector2 point in points)
        {
            GameObject waterParticle = Instantiate(waterParticlePrefab, point, Quaternion.identity);
            // 设置 Rigidbody 以使粒子下落  
            Rigidbody2D rb = waterParticle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1; // 启用重力  
            }
        }

        // 销毁 Line 对象  
        Destroy(gameObject);
    }

    public void StartFloatingAirParticles()
    {
        // 创建所有气体粒子  
        foreach (Vector2 point in points)
        {
            GameObject AirParticle = Instantiate(AirParticlePrefab, point, Quaternion.identity);
            // 设置 Rigidbody 以使粒子下落  
            Rigidbody2D rb = AirParticle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = -1; // 启用重力  
                rb.velocity = Vector2.zero; // 初始化速度为零 
            }
        }

        // 销毁 Line 对象  
        Destroy(gameObject);
    }
    private void Update()
    {
        // 找到所有气体粒子并施加向上的推力  
        foreach (GameObject airParticle in GameObject.FindGameObjectsWithTag("Air")) // 假设气体粒子有一个标签  
        {
            Rigidbody2D rb = airParticle.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 upwardForce = new Vector2(0, 20f); // 根据需要调整推力大小  
                rb.AddForce(upwardForce, ForceMode2D.Force);
            }
        }
    }
    //火焰特效
    public void CreateFireParticle(Vector2 position)
    {
        ParticleSystem fireInstance = Instantiate(fireParticleSystem, position, Quaternion.identity);
        fireInstance.transform.position = new Vector3(position.x, position.y, 0);
        fireParticlesList.Add(fireInstance);
        fireInstance.Play();
    }


    public void StopFireParticles()
    {
        StartCoroutine(StopAndDestroyParticlesAfterDelay(2.9f)); // 等待3秒后停止并销毁粒子
    }

    private IEnumerator StopAndDestroyParticlesAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待3秒

        foreach (var fireParticle in fireParticlesList)
        {
            if (fireParticle != null)
            {
                fireParticle.Stop(); // 停止粒子播放
                Destroy(fireParticle.gameObject, 0.1f); // 延迟1秒销毁，以确保粒子效果完全停止
            }
        }

        fireParticlesList.Clear(); // 清空列表
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
        lineColor = color; // 更新lineColor字段  
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