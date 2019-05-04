using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform bullet;

    [SerializeField]
    private int lives = 3;
    private int score = 0;

    private float speed = 6f;

    private AudioSource shotSound;

    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }


    
    void Update()
    {
        MoveKey();
       


        // Atira com click do mouse ou com a barra de espaço
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    private void MoveMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePos.y += .2f;
        // mousePos.x -= 2f;
        transform.position = mousePos;
        //LookAtMouse();
    }


    private void MoveKey()
    {

        float horizotal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // if ()
        //{
        // Movimenta na horizontal
        transform.Translate(Vector2.right * horizotal * speed * Time.deltaTime);
        // movimenta na vertical
        transform.Translate(Vector2.up * vertical * speed * Time.deltaTime);
        
        //}

        CheckBoundaries();

    }


    //OKokok
    private void LookAtMouse()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        //direction.x -= 1f; 
        direction.y -= .8f;
        direction.x += .8f;

        if (direction.y != transform.position.y && direction.x != transform.position.x)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            transform.up = direction;
        }

    }


    private void Fire()
    {
        shotSound.Play();
        Instantiate(bullet, transform.position, transform.rotation);
    }


    public void AddScore(int value)
    {
        score += value;
        UIManager.Instance.UpdatePlayerScore(score);
    }


    public void Damage(int value)
    {
        this.lives -= value;
        UIManager.Instance.UpdatePlayerLives(lives);
        if (lives <= 0)
        {
            
            UIManager.Instance.GameOver();
        }
    }


    private void CheckBoundaries()
    {
        if (transform.position.x < -6)
        {
            transform.position = new Vector2(-6, transform.position.y);
        }
        else
             if (transform.position.x > 6)
        {
            transform.position = new Vector2(6, transform.position.y);
        }
        else
             if (transform.position.y < -4.4f)
        {
            transform.position = new Vector2(transform.position.x, -4.4f);
        }
        else
             if (transform.position.y > 4.4f)
        {
            transform.position = new Vector2(transform.position.x, 4.4f);
        }
    }


}
