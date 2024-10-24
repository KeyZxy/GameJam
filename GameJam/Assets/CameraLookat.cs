using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookat : MonoBehaviour
{
    public Transform target;  // Ҫ�����Ŀ��  
    public Vector3 offset;    // �����Ŀ��֮���ƫ��  
    public float smoothSpeed = 0.125f; // ƽ��������ٶ�  

    public SpriteRenderer backgroundRenderer; // ������ SpriteRenderer

    private Camera cam;  // ������

    private void Start()
    {
        // ��ȡ������
        cam = Camera.main;
        // ��ȡ��Ҷ���
        target = GameObject.FindWithTag("Player").transform;
        // ���䵱ǰ����
        ResizeBackgroundToFillCamera();
    }

    private void Update()
    {
        // ���Ŀ�껹�����ڣ������ҵ����
        if (target == null)
        {
            target = GameObject.FindWithTag("Player").transform;
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            // ����Ŀ��λ�ü���ƫ��  
            Vector3 desiredPosition = target.position + offset;

            // ƽ���ƶ������Ŀ��λ��  
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            // ȷ�����û����ת��������2D��ͼ��  
            transform.rotation = Quaternion.identity;

            // �ñ����������
            FollowCameraWithBackground();
        }
    }

    // �ñ�����������ƶ�
    private void FollowCameraWithBackground()
    {
        
            // ���ñ�����λ��Ϊ�����λ�ã���ȷ�� z �ᱣ�ֱ����Ĳ㼶λ��
            backgroundRenderer.transform.position = new Vector3(transform.position.x, transform.position.y, backgroundRenderer.transform.position.z);
        
    }

    // ����������С��ʹ��������������Ұ
    public void ResizeBackgroundToFillCamera()
    {
       
            // ��ȡ�����ĵ�ǰ�ߴ�
            float bgWidth = backgroundRenderer.sprite.bounds.size.x;
            float bgHeight = backgroundRenderer.sprite.bounds.size.y;

            // ��ȡ����ĸ߶ȺͿ��
            float cameraHeight = 2f * cam.orthographicSize;
            float cameraWidth = cameraHeight * cam.aspect;

            // ������Ҫ�����ű���
            float scaleX = cameraWidth / bgWidth;
            float scaleY = cameraHeight / bgHeight;

            // ���ñ��������ű�����ʹ����������Ұ
            backgroundRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1);
        
    }

    // ���ڸ��ı������Զ��������
    public void ChangeBackground(SpriteRenderer newBackground)
    {
        // ���±�����Sprite
     
            backgroundRenderer = newBackground;

            // ���µ���������С����Ӧ���
            ResizeBackgroundToFillCamera();
        
    }
}
