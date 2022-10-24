using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{

    //----------Prototipo de inteligencia artificial simple para enemigos-------------
    //En la página web podés leer los artículos sobre este prototipo cuando estén listos
    //https://gamedevtraum.com
    //Visita el canal para ver más cosas sobre programación, diseño 3d y videojuegos :D 
    //https://youtube.com/c/GameDevTraum
    //
    //Suscríbete!

    [SerializeField]
    private Transform posicionInicial;

    [SerializeField]
    private Arma arma;
    [SerializeField]
    private int puntosVidaMaxima;
    [SerializeField]
    private int puntosVida;
    [SerializeField]
    private GameObject objetoMuerte;
    private bool puedeDisparar=true;

    [SerializeField]
    private bool muerte;

    [SerializeField]
    private UIManager uiManager;


    private Camera camera;

    void Start()
    {
        arma=GetComponent<Arma>();
        puntosVida = puntosVidaMaxima;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (muerte)
        {
            Muerte();
        }

        if (puedeDisparar)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                Disparar(new Vector3(ray.direction.x,0f,ray.direction.z));    
            }
        }
    
        if (Input.GetKeyDown(KeyCode.R))
        {
            arma.Recargar();
        }
    }

    private void Disparar(Vector3 d)
    {
        if(arma.Shoot(d)){
            //hay balas
           
            puedeDisparar = false;

            Invoke("HabilitarDisparo", 0.3f);

        }else{
            //no hay balas
           
        }


    }

    private void Muerte()
    {
        //morimos
        Instantiate(objetoMuerte, transform.position, Quaternion.identity);
        puntosVida = puntosVidaMaxima;
        gameObject.transform.position = posicionInicial.position;
        uiManager.JugadorMuere();
    }

    private void HabilitarDisparo()
    {
        //Para que no podamos disparar de manera continua
        puedeDisparar = true;
    }

    public void RecibeDisparo()
    {
        //alguien nos ha disparado
        puntosVida--;
        if (puntosVida <= 0)
        {
            
            Muerte();
           
        }

    }

}
