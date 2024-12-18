using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Line : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip drop;
    public float maxLineLength = 10f;//长度
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
    // 碰撞检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (lineColor == Color.yellow && collision.gameObject.CompareTag("Water"))
        {
            Debug.Log("Dirt线条与Water物体碰撞，Dirt线条将被销毁");
            Destroy(gameObject); // 销毁 Dirt 线条
        }
        if(lineColor == Color.yellow && collision.gameObject.CompareTag("Stage"))
        {
            audioSource.PlayOneShot(drop);
        }
    }

    public void AddPoint(Vector2 newPoint)
    {
        // If this is the first point, add it immediately
        if (pointCount == 0)
        {
            points.Add(newPoint);
            ++pointCount;

            var circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
            circleCollider.offset = newPoint;
            circleCollider.radius = circleColliderRadius;

            lineRenderer.positionCount = pointCount;
            lineRenderer.SetPosition(pointCount - 1, newPoint);

            return;
        }

        // Check if the new point is far enough from the last point
        float distanceToLastPoint = Vector2.Distance(newPoint, GetLastPoint());

        if (distanceToLastPoint < pointsMinDistance)
            return;

        // Calculate the current total length of the line
        float currentLineLength = 0f;
        for (int i = 1; i < points.Count; i++)
        {
            currentLineLength += Vector2.Distance(points[i - 1], points[i]);
        }

        // If adding the new segment exceeds max length, adjust the point position
        if (currentLineLength + distanceToLastPoint > maxLineLength)
        {
            float remainingLength = maxLineLength - currentLineLength;

            // Calculate the point at which the new segment should stop
            Vector2 direction = (newPoint - GetLastPoint()).normalized;
            newPoint = GetLastPoint() + direction * remainingLength;
            distanceToLastPoint = remainingLength; // This will be used for the next steps
        }

        // Now add the point and update line and colliders
        points.Add(newPoint);
        ++pointCount;

        var circleCollider2D = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider2D.offset = newPoint;
        circleCollider2D.radius = circleColliderRadius;

        lineRenderer.positionCount = pointCount;
        lineRenderer.SetPosition(pointCount - 1, newPoint);

        if (pointCount > 1)
            edgeCollider.points = points.ToArray();

        // If max length is reached, stop adding points
        if (currentLineLength + distanceToLastPoint >= maxLineLength)
        {
            Debug.Log("Max line length reached.");
        }
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