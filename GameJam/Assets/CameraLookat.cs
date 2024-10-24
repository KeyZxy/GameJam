using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookat : MonoBehaviour
{
    public Transform target;  // 要跟随的目标  
    public Vector3 offset;    // 相机与目标之间的偏移  
    public float smoothSpeed = 0.125f; // 平滑跟随的速度  

    public SpriteRenderer backgroundRenderer; // 背景的 SpriteRenderer

    private Camera cam;  // 相机组件

    private void Start()
    {
        // 获取相机组件
        cam = Camera.main;
        // 获取玩家对象
        target = GameObject.FindWithTag("Player").transform;
        // 适配当前背景
        ResizeBackgroundToFillCamera();
    }

    private void Update()
    {
        // 如果目标还不存在，尝试找到玩家
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // 计算目标位置加上偏移  
            Vector3 desiredPosition = target.position + offset;

            // 平滑移动相机到目标位置  
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // 确保相机没有旋转（保持在2D视图）  
            transform.rotation = Quaternion.identity;

            // 让背景跟随相机
            FollowCameraWithBackground();
        }
    }

    // 让背景跟随相机移动
    private void FollowCameraWithBackground()
    {
        
            // 设置背景的位置为相机的位置，并确保 z 轴保持背景的层级位置
            backgroundRenderer.transform.position = new Vector3(transform.position.x, transform.position.y, backgroundRenderer.transform.position.z);
        
    }

    // 调整背景大小，使其填充整个相机视野
    public void ResizeBackgroundToFillCamera()
    {
       
            // 获取背景的当前尺寸
            float bgWidth = backgroundRenderer.sprite.bounds.size.x;
            float bgHeight = backgroundRenderer.sprite.bounds.size.y;

            // 获取相机的高度和宽度
            float cameraHeight = 2f * cam.orthographicSize;
            float cameraWidth = cameraHeight * cam.aspect;

            // 计算需要的缩放比例
            float scaleX = cameraWidth / bgWidth;
            float scaleY = cameraHeight / bgHeight;

            // 设置背景的缩放比例，使其填充相机视野
            backgroundRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1);
        
    }

    // 用于更改背景并自动适配相机
    public void ChangeBackground(SpriteRenderer newBackground)
    {
        // 更新背景的Sprite
     
            backgroundRenderer = newBackground;

            // 重新调整背景大小以适应相机
            ResizeBackgroundToFillCamera();
        
    }
}
