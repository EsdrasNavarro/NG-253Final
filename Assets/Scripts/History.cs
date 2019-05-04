using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class History : MonoBehaviour
{

    private float time;

    void Start()
    {
    }

    private void Update()
    {
        time = Time.timeSinceLevelLoad;
        if(time >= 6f) // 5 segundos abre a tela o jogo
        {
           SceneManager.LoadScene("Fase1", LoadSceneMode.Single);
        }
    }


}
