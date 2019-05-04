using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy2 : MonoBehaviour
{

    [SerializeField]
    protected Transform bullet;

    [SerializeField]
    protected int type;
    [SerializeField]
    protected float speed;
    protected float fireRate;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int bonus = 1; // valor de bonus para o player

    public int Bonus { get { return bonus; } }

    protected abstract void Init();

    private void Start()
    {
        Init();
        StartCoroutine(FireRoutine());
        fireRate = Random.Range(1f, 2f);
    }

    protected virtual void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        // metodo no start para o inimigo mover para a possiçção do player. 
        // No update ele vai seguir o player..
        Vector3 mousePosition = GameManager.Instance.Player.transform.position;

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.Translate(Vector2.up * speed * Time.deltaTime);
        transform.up = direction;
    }

    // a cada 2 segundos ele atira, podemos colocar um rdm
    protected virtual IEnumerator FireRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.Damage(damage);
                //ShowExplosionAnim();
               
                Destroy(this.gameObject);
            }

        }
    }

}
