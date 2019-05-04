using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy2
{

    [SerializeField]
    private Transform multShot;

    [SerializeField]
    private Transform explosion;

    private float targetPosition;
    private float direction = 1;

    protected override void Init()
    {

    }

    protected override void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position.y > 3.5f)
        {
            transform.Translate(Vector2.down * 2f * Time.deltaTime);
        }
        else
        {
            targetPosition += Time.deltaTime * direction; 
            if (targetPosition >= 5f)
            {
                direction *= -1;
                targetPosition = 5f;
            }
            else if (targetPosition <= -5f)
            {
                direction *= -1;
                targetPosition = -5f;
            }
            transform.position = new Vector3(targetPosition, transform.position.y, 0);
        }
    }



    private void Fire()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            Instantiate(multShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    
    protected override IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Fire();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Damage(damage);
                

               
                Instantiate(explosion, other.transform.position, other.transform.rotation);
            }

        }else
        if (other.CompareTag("BulletPlayer"))
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            UIManager.Instance.UpdateEnemyBossLife();
        }
    }

}
