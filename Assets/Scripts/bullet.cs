using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Character c;
    
    void Start()
    {
        Destroy(gameObject,5f);
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //merminin etkilesimi ve yokolmasý
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }



}
