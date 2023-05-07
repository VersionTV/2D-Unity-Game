using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using Unity.Mathematics;

public class Character : MonoBehaviour
{
    int currentLevel;
    public static int health=6;
    float speed = 10f;
    bool kosma = false;
    public static bool isStart = false;
    static int fruits = 0;
    [SerializeField]
    AudioSource fruitCollect,died,reload;

    [SerializeField]
    Rigidbody2D rig;
    [SerializeField]
    Animator ani,aniE,aniF;
    [SerializeField]
    Text fruitText,fruitscoreText,BestScoreText,bestwelcome,endbest;
    [SerializeField]
    GameObject restartPanel,welcomePanel,gun,endpanel;
    [SerializeField]
    Image healthImage;

    BoxCollider2D Box2d;

    void Start()
    {
        if (GameManager.isRestart)//oyun baþlama durumu kontrolü
        {
            welcomePanel.SetActive(false);
        }
        fruitText.text = fruits.ToString();
        BestScoreText.text ="BEST SCORE: "+ PlayerPrefs.GetInt("bestscore").ToString();//en iyi skoru getir
        bestwelcome.text = BestScoreText.text;
        currentLevel = PlayerPrefs.GetInt("Level", 0);//kayýtlý leveli bul
        health =PlayerPrefs.GetInt("can");//Can kaydýný getir
        if (!PlayerPrefs.HasKey("can"))//oyuna ilk defa mý giriyor? Evetse can full
        {
            health = 6;
        }
        HealthGUI();



    }

    private void Awake()
    {
        Box2d=GetComponentInChildren<BoxCollider2D>();
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (!isStart)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");//hareketleri al
        Move(h);
        Animation(h);
        playerTurn(h);//hareket ve animasyon methodlarýný cagir

        
    }
    void Move(float h) { 
        rig.velocity=new Vector2(h*speed,rig.velocity.y);//hareket

    } 
    void Animation(float h)
    {
        if(h==0) {
             kosma = false;

        }
        else if(h!=0) { 
            kosma =true;

        }
        ani.SetBool("kosma", kosma);//kosuyor mu animasyonu
    }
    void playerTurn(float h)
    {
        if (h > 0)//x eksenine göre karekterin yone donmesi
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f );
        }
        if (h < 0)
        {
            this.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }
    private void Death(GameObject go)
    {
        //die islemleri
        
        if (go.CompareTag("Enemy") ||go.CompareTag("DieBorder"))
        {
           StartCoroutine(DieDelayed());
            ani.SetTrigger("die");
            died.Play();
           
        }
       


            IEnumerator DieDelayed()//gecikmeli olme
        {
            yield return new WaitForSeconds(0.7f);
            Destroy(gameObject);
            HealthSystem();
            fruitscoreText.text = "Score : " + fruitText.text;
           
            

        }


        }
          void Kill(GameObject go)//karekterin kafasýna vurup oldurme
        {
        if (go.CompareTag("EnemyHead"))
        {
            aniE.SetTrigger("killed");
            Destroy(go.transform.parent.gameObject, 0.3f);
            fruitCollect.Play();
            
            fruits += 5;
            fruitText.text = fruits.ToString();
        }
        }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//yeniden baslatma
    }
    public void Quitgame()
    {
        Application.Quit(); //cikis islemi
    }
    public void PlayGame()
    {
        isStart = true; //baslatma
        
        welcomePanel.SetActive(false);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death(collision.gameObject); //collidera dokunma
        Kill(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fruit")) //istrigger la taglara gore obje temaslarý
        {
            fruitCollect.Play();
            Destroy(collision.gameObject);  //meyve toplama
            fruits += 5;
            fruitText.text = fruits.ToString();
            
        }
        if (collision.CompareTag("GunCard"))
        {
            reload.Play(); //silah alma
            gun.SetActive(true);
            Destroy(collision.gameObject); ;
        }
        if (collision.CompareTag("finish")) //level gecme
        {
            currentLevel++; 
            PlayerPrefs.SetInt("Level", currentLevel);
            aniF.SetTrigger("triggered");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
        if(collision.CompareTag("fullfinish"))
        {
            endpanel.SetActive(true); //oyunu bitirme
            PlayerPrefs.SetInt("Level", 0);
            endbest.text = "Best Score: " + fruits.ToString();
        }
    }
    void HealthSystem()
    {
        //can sistemi
        if (health > 0)
        {
            GameManager.isRestart = true;
            health--;
            PlayerPrefs.SetInt("can", health);
            RestartGame();

        }
        else
        {
            if (fruits > PlayerPrefs.GetInt("bestscore"))
            {
                PlayerPrefs.SetInt("bestscore", fruits);
                BestScoreText.text ="Best Score: "+ fruits.ToString();


            }
            restartPanel.SetActive(true);
            health = 6;
            PlayerPrefs.SetInt("can", health);
            fruits = 0;
        }
    }
    void HealthGUI()
    {//can sistemi arayuzu
        switch (health)
        {
            case 6:
                healthImage.fillAmount = 1F;
                break;
            case 5:
                healthImage.fillAmount = 0.85f;
                break;
            case 4:
                healthImage.fillAmount = 0.70f;
                break;
            case 3:
                healthImage.fillAmount = 0.50f;
                break;
            case 2:
               
                healthImage.fillAmount = 0.30f;
                break;
            case 1:
                
                healthImage.fillAmount = 0.15f;
                break;
            case 0:
                
                healthImage.fillAmount = 0f;
                break;
            default:
                healthImage.fillAmount = 1f;
                
                break;

        }
        
           
        
    }
    public void ContinueLevel()
    {
        //leveli yukleme
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + currentLevel);
    }

}//class

