using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    Animator anim1;
    [SerializeField]
    LayerMask layer;
    [SerializeField]
    bool onGround = true;
    [SerializeField]
    Rigidbody2D rib;
    float jumpSpeed = 7f;
    

    void Start()
    {

        
        anim1 =GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Character.isStart)
        {
            return;
        }

        RaycastHit2D isHit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f,layer);
        if (isHit.collider != null)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
        if (onGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            rib.velocity = new Vector2(rib.velocity.x, jumpSpeed);
           
        }
        anim1.SetBool("jump", !onGround);
       
    }
}
