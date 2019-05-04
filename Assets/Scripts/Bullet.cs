using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private Transform explosion;

    [SerializeField]
    private bool fromPlayer = false;

    [SerializeField]
    private bool mutiple = false;

    [SerializeField]
    private float speed = 5f; // velocidade da bala

    private Vector2 moveDirection;

    private void Start()
    {
        Destroy(this.gameObject, 3f);
        if (fromPlayer)
        {
            moveDirection = Vector2.up;
        }
        else
        {

            //transform.GetComponent<SpriteRenderer>().flipY = true;
            if (mutiple == false)
            {
                LookToPlayer();
                moveDirection = Vector2.up;
                
            }
            else
            {
                moveDirection = Vector2.down    ;
            }

        }
    }

    void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime * speed);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {


        if (fromPlayer)
        { 
            HitTheEnemy(other);
        }
        else
        {
            HitThePlayer(other);
        }

    }


    private void HitThePlayer(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Damage(1);
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                Destroy(this.gameObject);
            }

        }
    }

    private void HitTheEnemy(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                GameManager.Instance.Player.AddScore(enemy.Bonus);
            }
            // explosão quando inimigo é morto
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }



    private void HitTheBoss(Collider2D other)
    {
        if (other.CompareTag("Boss"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("BOSSSS");
                UIManager.Instance.UpdateEnemyBossLife();
            }
            // explosão quando o inimigo é morto
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }



    private void LookToPlayer()
    {
        // esse metodo no start serve para o inimigo mover para a possiçção do player. 
        // No update ele vai seguir o player..
        Vector3 mousePosition = GameManager.Instance.Player.transform.position;

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.Translate(Vector2.up * speed * Time.deltaTime);
        transform.up = direction;
    }

}
