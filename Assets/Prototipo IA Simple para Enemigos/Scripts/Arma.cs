using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    //----------Prototipo de inteligencia artificial simple para enemigos-------------
    //En la página web podés leer los artículos sobre este prototipo cuando estén listos
    //https://gamedevtraum.com
    //Visita el canal para ver más cosas sobre programación, diseño 3d y videojuegos :D 
    //https://youtube.com/c/GameDevTraum
    //
    //Suscríbete!

    [SerializeField]
    private int cargador;

    [SerializeField]
    private int tiempoRecarga;

    [SerializeField]
    private GameObject origenDisparo;

    [SerializeField]
    private float alcanceRuido;

    [SerializeField]
    private GameObject proyectil;
    [SerializeField]
    private float velocidadProyectil;

    private bool recargando=false;

    [SerializeField]
    private int remainingBullets;

    private bool dibujarCirculoDisparo;
    private Vector3 puntoDisparo;

    private IntelligentEnemy portador;

    void Start()
    {
        remainingBullets = cargador;

        //Esto lo agrego para que el propio enemigo no se alerte a si mismo por sus propios disparos
        //No quise modificar mucho el código con respecto al video.
        portador = gameObject.GetComponent<IntelligentEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dibujarCirculoDisparo)
        {
            DibujarCirculoDeDisparo();
        }
    }

    public bool Shoot(Vector3 d)
    {
        

        bool canShoot = false;

        if (remainingBullets > 0)
        {

            canShoot = true;

            EfectuarDisparo(d);

        }

        return canShoot;
    }



    private void EfectuarDisparo(Vector3 d)
    {
        
        Vector3 direccion = Vector3.Normalize(d);
        Vector3 d2 = d - gameObject.transform.position;
        puntoDisparo = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+d.y, gameObject.transform.position.z);
        GameObject proyectilNuevo;
        if (portador != null){
            proyectilNuevo = Instantiate(proyectil, origenDisparo.transform.position, Quaternion.identity);
        }else{
            proyectilNuevo = Instantiate(proyectil, puntoDisparo + d, Quaternion.identity);

        }

        Proyectil p = proyectilNuevo.GetComponent<Proyectil>();
        p.SetShooter(gameObject);
        if (portador != null)
        {
            p.SetOwner(false);
        }
        proyectilNuevo.GetComponent<Rigidbody>().velocity = direccion * velocidadProyectil;
        remainingBullets--;
        EmitirSonido();
    }

    private void EmitirSonido()
    {
        //Buscar todos los enemigos y alertar a los cercanos
        IntelligentEnemy[] enemies = FindObjectsOfType<IntelligentEnemy>();

        if (enemies != null)
        {


            int n = enemies.Length;

            for (int i = 0; i < n; i++)
            {
                if (Vector3.Distance(puntoDisparo, enemies[i].transform.position) < alcanceRuido)
                {
                    //Esta parte es nueva, es para evitar que el propio portador del arma se alerte a si mismo por su disparo
                    if (portador != null)
                    {
                        if (enemies[i] != portador)
                        {
                            enemies[i].DisparosEnLasCercanias(puntoDisparo);
                        }

                    }
                    else
                    {
                        enemies[i].DisparosEnLasCercanias(puntoDisparo);
                    }

                }
            }
            dibujarCirculoDisparo = true;
            
            Invoke("BorrarCirculoDisparo", 3f);
        }
    }

    public void Recargar()
    {
        if (!recargando)
        {
            recargando = true;
            Invoke("ColocarBalas", tiempoRecarga);
        }
    }

    private void ColocarBalas()
    {
        remainingBullets = cargador;
        recargando = false;

    }

    public bool HayBalas()
    {
        return remainingBullets > 0;
    }


    private void DibujarCirculoDeDisparo()
    {

        int divisionesCirculo = 25;
        float angleStep = 360f / (divisionesCirculo);
        for (int i = 0; i < divisionesCirculo; i++)
        {

            float x, z;
            float x2, z2;

            x = alcanceRuido * Mathf.Sin(Mathf.Deg2Rad * angleStep * i);
            z = alcanceRuido * Mathf.Cos(Mathf.Deg2Rad * angleStep * i);

            x2 = alcanceRuido * Mathf.Sin(Mathf.Deg2Rad * angleStep * (i + 1));
            z2 = alcanceRuido * Mathf.Cos(Mathf.Deg2Rad * angleStep * (i + 1));

            Vector3 inicio = new Vector3(x, 1, z);
            Vector3 fin = new Vector3(x2, 1, z2);

            Color c = Color.green;
            

            Debug.DrawLine(puntoDisparo+inicio, puntoDisparo + fin, c);

        }
    }

    private void BorrarCirculoDisparo()
    {
        dibujarCirculoDisparo = false;
    }

    public int GetBalasRestantes()
    {
        return remainingBullets;
    }

}
