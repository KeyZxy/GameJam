using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    Player player;
    Animator anima;
    Rigidbody2D rb;
    void Start()
    {
        player= GetComponent<Player>();
        anima= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float temp = player.move;
        
        if (player.isGrounded)
        {
            anima.SetFloat("Speed", Mathf.Abs(temp));
            anima.SetBool("JumpUp", false);

            anima.SetBool("JumpDown", false);

        }
        else
        {
            Vector3 vel = rb.velocity;
            if (vel.y > 0)
            {
                anima.SetBool("JumpUp", true);
                anima.SetBool("JumpDown", false);
            }
            else
            {
                anima.SetBool("JumpUp", false);
                anima.SetBool("JumpDown", true);
            }
        }
    }
}
