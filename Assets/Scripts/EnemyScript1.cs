using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyScript1 : MonoBehaviour
{
   
    
    float speed=2f;
    [SerializeField]
    Transform startPoint, endPoint;//baslama durma noktalarý
    private bool movingRight = true;//saga gitme kontrolu
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
        if (transform.position == startPoint.position)
        {
            movingRight = true;
        }
        else if (transform.position == endPoint.position)
        {
            movingRight = false;
        }

        if (movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
        }
    }
        //dusmanin harekti
    
    private void Awake()
    {
    

    }

}
