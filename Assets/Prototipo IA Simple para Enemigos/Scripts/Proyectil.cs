using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //----------Prototipo de inteligencia artificial simple para enemigos-------------
    //En la página web podés leer los artículos sobre este prototipo cuando estén listos
    //https://gamedevtraum.com
    //Visita el canal para ver más cosas sobre programación, diseño 3d y videojuegos :D 
    //https://youtube.com/c/GameDevTraum
    //
    //Suscríbete!
    private GameObject shooter;
    private bool isPlayerAmmo=true;

    private void OnCollisionEnter(Collision collision)
    {

        IntelligentEnemy enemigo = collision.gameObject.GetComponent<IntelligentEnemy>();
        ControlJugador jugador = collision.gameObject.GetComponent<ControlJugador>();
        Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();



        if (collision.gameObject != shooter)
        {
            
                if (enemigo != null)
                {
                    enemigo.RecibeDisparo();
                    Destroy(gameObject);
                }
           
                if (jugador != null)
                {
                    jugador.RecibeDisparo();
                    Destroy(gameObject);
                }
           

            if (obstacle != null)
            {
                Destroy(gameObject);
            }
        }


    }
   

    public void SetShooter(GameObject s)
    {
        shooter = s;
    }
    public void SetOwner(bool b)
    {
        isPlayerAmmo = b;
    }


}
