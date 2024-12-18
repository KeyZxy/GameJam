using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>  
/// 画线控制器  
/// </summary>  
public class Draw : MonoBehaviour
{
    public GameObject linePrefab; // 线条预设  

   // public LayerMask cantDrawOverLayer; // 不能绘制在此层上  
    //private int cantDrawOverLayerIndex; // 不能绘制层的索引  

    [Space(30)]
    public Color lineColor; // 线条颜色  
    public float linePointsMinDistance; // 点之间的最小距离  
    public float lineWidth; // 线条宽度  

    private Line currentLine; // 当前绘制的线条  
    private Camera cam; // 主摄像机  

    //音效
    public AudioSource audioSource;
    public AudioClip burn;
    public AudioClip flow;
    public AudioClip wind;
    public AudioClip dirt;
    private void Start()
    {
        cam = Camera.main; // 获取主摄像机  
       audioSource=gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        // 检测鼠标输入  
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            BeginDraw(); // 开始绘制  
        if (currentLine != null)
            Drawing(); // 进行绘制  
        if (Input.GetMouseButtonUp(0))
            EndDraw(); // 结束绘制  
    }

    // 画线逻辑-----------------------------------------------------------------------  

    // 开始画线  
    void BeginDraw()
    {
        // 检查颜色是否为允许的颜色
        if (lineColor != Color.red && lineColor != Color.yellow && lineColor != Color.blue && lineColor != Color.green)
        {
            Debug.LogWarning("当前颜色不允许绘制");
            return;
        }
        // 实例化线預設  
        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        // 设置线条参数  
        //currentLine.UsePhysics(false); // 关闭物理模拟  
        currentLine.SetLineColor(lineColor); // 设置线条颜色  
        if (lineColor == Color.green)
        {
            audioSource.PlayOneShot(wind);
          
        }
        if (lineColor == Color.blue)
        {
            audioSource.PlayOneShot(flow);

        }
        currentLine.SetPointsMinDistance(linePointsMinDistance); // 设置点之间的最小距离  
        currentLine.SetLineWidth(lineWidth); // 设置线条宽度  
    }

    // 画线进行中  
    void Drawing()
    {
        if (currentLine == null) return;
        // 获取当前鼠标位置并转换为世界坐标  
        Vector3 screenPos = Input.mousePosition; // 鼠标屏幕坐标  
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos);
        worldPos.z = 0; // 确保z轴为0  

        // 将世界坐标转换为相对于LinesDrawer的局部坐标  
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        // 添加新点  
        currentLine.AddPoint(localPos);
        //火焰特效
        if (lineColor == Color.red)
        {
            currentLine.CreateFireParticle(localPos);
        }
    }

    // 画线结束  

    void EndDraw()
    {
        if (currentLine == null) return;

        currentLine.SetLineColor(lineColor);

        if (lineColor == Color.red)
        {
            currentLine.tag = "Fire";
            Rigidbody2D currentRigidbody = currentLine.gameObject.AddComponent<Rigidbody2D>();
            currentRigidbody.gravityScale=0;
            // 在3秒后销毁当前线条
            audioSource.PlayOneShot(burn);
            // 停止所有火焰粒子效果
            currentLine.StopFireParticles();

            // 销毁线条
            Destroy(currentLine.gameObject, 3f);

        }

        else if (lineColor == Color.blue)
        {
            currentLine.tag = "Water";
          
            currentLine.StartDroppingWaterParticles(); // 开始将水线拆分为粒子  
        }
        else if (lineColor == Color.green)
        {
            currentLine.tag = "Air";
            
            currentLine.StartFloatingAirParticles();
        }
        else if (lineColor == Color.yellow)
        {
            currentLine.tag = "Dirt";
            audioSource.PlayOneShot(dirt);
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