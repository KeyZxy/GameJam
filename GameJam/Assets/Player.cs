using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float move;
    bool jump;
    public float speed = 5f;
    public float jumpForce = 400f;                          // 弹跳力
    public bool canAirControl = false;                      // 在空中时，是否能控制
    public bool isDead = false;                             // 判断是否死亡,加载存档
    public LayerMask groundMask;                            // 定义哪一个Layer是地面
    public Transform m_GroundCheck;                         // 用于判定地面的空物体

    const float k_GroundedRadius = .1f; // 用于检测地面的小圆形的半径
    private bool m_Grounded;            // 当前是否在地面上
    private bool m_FacingRight = true;  // 玩家是否面朝右边
    private Vector3 m_Velocity = Vector3.zero;

    const float m_NextGroundCheckLag = 0.1f;    // 起跳后的一小段时间，不能再次起跳。防止连跳的一种解决方案
    float m_NextGroundCheckTime;            // 过了这个时间才可能落地、才能再次起跳

    // 这个角色控制器，是依靠刚体驱动的
    private Rigidbody2D m_Rigidbody2D;
    public float climbSpeed = 5f; // 攀爬速度  
    public bool isClimbing = false; // 是否正在攀爬  
    //public Animator animator;
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnAirEvent;

    public bool isGrounded;

    public List<Color> color;//玩家获得颜色
    public Button[] button;

    public Transform spawnPoint;  // 玩家出生点
    private Vector3 initialPosition;  // 玩家初始位置
    public float fallTimeLimit = 2.0f;  // 玩家下落时间限制（超过该时间判定死亡）

    private float fallTimer = 0f;  // 记录玩家下落的时间
    private bool isFalling = false;  // 是否正在下落
    private float fallThreshold = -0.1f;  // 下落的速度阈值（控制何时算作下落）
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnAirEvent == null)
            OnAirEvent = new UnityEvent();
       //color.Add(Color.red);
    }
    private void Start()
    {
        // 将玩家的位置设置为出生点位置
        if (spawnPoint != null)
        {
            initialPosition = spawnPoint.position;
            Respawn();
        }
    }
    // 复活玩家
    public void Respawn()
    {
        // 设置玩家位置为出生点
        transform.position = initialPosition;
        fallTimer = 0f;  // 重置下落计时器
        isFalling = false;

    }
    public void OnDeath()
    {
        // 重新加载当前关卡
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        move = Input.GetAxis("Horizontal");
        move *= speed;
        float temp = move;
        isGrounded = m_Grounded;

        CheckFall();


        jump = Input.GetButton("Jump");
        // 检测是否在攀爬状态  
        if (isClimbing)
        {
            HandleClimbing();
        }
        ColorSelect();
    }
    // 检查玩家是否下落
    void CheckFall()
    {
        // 检查玩家是否在下落
        if (m_Rigidbody2D.velocity.y < fallThreshold)
        {
            if (!isFalling)
            {
                isFalling = true;  // 玩家开始下落
                fallTimer = 0f;  // 重置计时器
            }
            fallTimer += Time.deltaTime;  // 记录下落的时间// 如果下落时间超过了限制，判定玩家死亡
            if (fallTimer >= fallTimeLimit)
            {
                OnDeath();
            }
        }
        else
        {
            // 玩家不在下落时重置状态
            isFalling = false;
            fallTimer = 0f;
        }
    }
    private void FixedUpdate()
    {
        Move(move,jump);
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // 检测与地面的碰撞
        if (Time.time > m_NextGroundCheckTime)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, groundMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                    if (!wasGrounded)
                        OnLandEvent.Invoke();
                }
            }
        }

        if (wasGrounded && !m_Grounded)
        {
            OnAirEvent.Invoke();
        }
    }
    public void ColorSelect()
    {
        for (int i = 0; i < button.Length; i++)
        {
            button[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < color.Count; i++) {

            if (color[i] == Color.green)
            {
                button[0].gameObject.SetActive(true);
            }
            if (color[i] == Color.red)
            {
                button[1].gameObject.SetActive(true);
            }
            if (color[i] == Color.yellow)
            {
                button[2].gameObject.SetActive(true);
            }
            if (color[i] == Color.blue)
            {
                button[3].gameObject.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true; // 进入梯子状态
            //color.Add(Color.green);
            m_Rigidbody2D.gravityScale = 0; // 清除重力影响  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false; // 离开梯子状态  
            
            m_Rigidbody2D.gravityScale = 1; // 恢复重力影响  
        }

    }

    private void HandleClimbing()
    {
        float verticalInput = Input.GetAxis("Vertical"); // 获取垂直输入  
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, verticalInput * climbSpeed); // 更新角色的垂直速度  
    }
    public void Move(float move, bool jump)
    {
        // 玩家在地面时，或者可以空中控制时，才能移动
        if (m_Grounded || canAirControl)
        {
            // 输入变量move决定横向速度
            m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);

            // 面朝右时按左键，或面朝左时按右键，都会让角色水平翻转
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        // 在地面时按下跳跃键，就会跳跃
        if (m_Grounded && jump)
        {
            OnAirEvent.Invoke();
            m_Grounded = false;
            // 施加弹跳力
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            m_NextGroundCheckTime = Time.time + m_NextGroundCheckLag;
        }
    }


    private void Flip()
    {
        // true变false，false变true
        m_FacingRight = !m_FacingRight;

        // 缩放的x轴乘以-1，图片就水平翻转了
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
}