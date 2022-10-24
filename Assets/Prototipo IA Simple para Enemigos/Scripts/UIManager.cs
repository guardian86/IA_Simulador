using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //----------Prototipo de inteligencia artificial simple para enemigos-------------
    //En la página web podés leer los artículos sobre este prototipo cuando estén listos
    //https://gamedevtraum.com
    //Visita el canal para ver más cosas sobre programación, diseño 3d y videojuegos :D 
    //https://youtube.com/c/GameDevTraum
    //
    //Suscríbete!

    [SerializeField]
    private Text indicadorEnemigos;
    [SerializeField]
    private Text indicadorMuertes;
    [SerializeField]
    private Text indicadorBalas;
    [SerializeField]
    private GameObject cartelVictoria;

    private int contadorMuertes;
    

    private GameObject player;
    private Arma armaJugador;
    [SerializeField]
    private IntelligentEnemy[] enemies;

    void Start()
    {
        cartelVictoria.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        armaJugador = player.GetComponent<Arma>();
        contadorMuertes = 0;
    }

    // Update is called once per frame
    void Update()
    {

        CheckReset();

        enemies = FindObjectsOfType<IntelligentEnemy>();
        indicadorEnemigos.text = enemies.Length.ToString();
        if (enemies.Length == 0)
        {
            cartelVictoria.SetActive(true);
        }
        indicadorBalas.text = armaJugador.GetBalasRestantes().ToString();
        indicadorMuertes.text = contadorMuertes.ToString();
    }

    private void CheckReset()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void JugadorMuere()
    {
        contadorMuertes++;
    }

}
