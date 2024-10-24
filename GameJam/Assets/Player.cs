using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    float move;
    bool jump;
    public float speed = 5f;
    public float jumpForce = 400f;                          // ������
    public bool canAirControl = false;                      // �ڿ���ʱ���Ƿ��ܿ���
    public bool isDead = false;                             // �ж��Ƿ�����,���ش浵
    public LayerMask groundMask;                            // ������һ��Layer�ǵ���
    public Transform m_GroundCheck;                         // �����ж�����Ŀ�����

    const float k_GroundedRadius = .1f; // ���ڼ������СԲ�εİ뾶
    private bool m_Grounded;            // ��ǰ�Ƿ��ڵ�����
    private bool m_FacingRight = true;  // ����Ƿ��泯�ұ�
    private Vector3 m_Velocity = Vector3.zero;

    const float m_NextGroundCheckLag = 0.1f;    // �������һС��ʱ�䣬�����ٴ���������ֹ������һ�ֽ������
    float m_NextGroundCheckTime;            // �������ʱ��ſ�����ء������ٴ�����

    // �����ɫ������������������������
    private Rigidbody2D m_Rigidbody2D;
    public float climbSpeed = 5f; // �����ٶ�  
    public bool isClimbing = false; // �Ƿ���������  
    public Animator animator;
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    public UnityEvent OnAirEvent;

    public bool isGrounded;
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (OnAirEvent == null)
            OnAirEvent = new UnityEvent();
    }
    private void Update()
    {
        move = Input.GetAxis("Horizontal");
        move *= speed;
        float temp = move;
        isGrounded = m_Grounded;
        //����
        if (isGrounded)
        {
            animator.SetFloat("Speed", Mathf.Abs(temp));
            animator.SetBool("JumpUp", false);
            animator.SetBool("JumpDown", false);

        }
        else 
        {
            Vector3 vel=m_Rigidbody2D.velocity;
            if (vel.y > 0) {
                animator.SetBool("JumpUp", true);
                animator.SetBool("JumpDown", false);
            }
            else
            {
                animator.SetBool("JumpUp", false);
                animator.SetBool("JumpDown", true);
            }
        }

        
        jump = Input.GetButton("Jump");
        // ����Ƿ�������״̬  
        if (isClimbing)
        {
            HandleClimbing();
        }
    }
    private void FixedUpdate()
    {
        Move(move,jump);
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // �����������ײ
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true; // ��������״̬  
            m_Rigidbody2D.gravityScale = 0; // �������Ӱ��  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false; // �뿪����״̬  
            m_Rigidbody2D.gravityScale = 1; // �ָ�����Ӱ��  
        }
    }

    private void HandleClimbing()
    {
        float verticalInput = Input.GetAxis("Vertical"); // ��ȡ��ֱ����  
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, verticalInput * climbSpeed); // ���½�ɫ�Ĵ�ֱ�ٶ�  
    }
    public void Move(float move, bool jump)
    {
        // ����ڵ���ʱ�����߿��Կ��п���ʱ�������ƶ�
        if (m_Grounded || canAirControl)
        {
            // �������move���������ٶ�
            m_Rigidbody2D.velocity = new Vector2(move, m_Rigidbody2D.velocity.y);

            // �泯��ʱ����������泯��ʱ���Ҽ��������ý�ɫˮƽ��ת
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        // �ڵ���ʱ������Ծ�����ͻ���Ծ
        if (m_Grounded && jump)
        {
            OnAirEvent.Invoke();
            m_Grounded = false;
            // ʩ�ӵ�����
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            m_NextGroundCheckTime = Time.time + m_NextGroundCheckLag;
        }
    }


    private void Flip()
    {
        // true��false��false��true
        m_FacingRight = !m_FacingRight;

        // ���ŵ�x�����-1��ͼƬ��ˮƽ��ת��
        transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
    }
}