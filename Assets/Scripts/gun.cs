using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    [SerializeField]
    Transform Player;
    Transform SpawnPoint;
    public GameObject bulletPrefab;
    float speed = 15f;
    public float fireRate = 0.5f;
    private float fireDelay = 0.0f;
    void Start()
    {
        SpawnPoint = GetComponentInChildren<Transform>();
        


    }

    // Update is called once per frame
    void Update()
    {
        if(!Character.isStart)
        { return; }

        if (Input.GetKeyDown(KeyCode.Mouse0)&&fireDelay<Time.time)//ard arda atesi engelleme icin gecikme
        {
            fireDelay = Time.time + fireRate;
            GameObject bullet = Instantiate(bulletPrefab, SpawnPoint.position, SpawnPoint.rotation);//mermi olusturma
            if (Player.localScale.x > 0) 
                {
                
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, SpawnPoint.rotation.x, 0);//mermi yollama yone göre
            }
            else
            {
                

                bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, SpawnPoint.rotation.x, 0);
            }
            

        }
    }
}
