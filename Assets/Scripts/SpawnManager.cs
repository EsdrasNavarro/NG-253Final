using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform enemy;
    public Transform enemyBoss;

    void Start()
    {
        StartCoroutine(CreateNewEnemyRountine());
    }

    public void CreateNewEnemy()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 20, Screen.height));
        pos.x = Random.Range((pos.x * -1), pos.x);
       
            Instantiate(enemy, pos, Quaternion.identity);
          
    }

    IEnumerator CreateNewEnemyRountine()
    {

        while (true && GameManager.Instance.levelTime < GameManager.LEVEL_TIME)
        {
            CreateNewEnemy();
            yield return new WaitForSeconds(2f);
        }


        if (GameManager.Instance.isBoss)
        {
            yield return new WaitForSeconds(4f); // geração aleatória
            Instantiate(enemyBoss);
        }

    }

}
