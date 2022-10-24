using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientarHaciaCamara : MonoBehaviour
{
    //----------Prototipo de inteligencia artificial simple para enemigos-------------
    //En la página web podés leer los artículos sobre este prototipo cuando estén listos
    //https://gamedevtraum.com
    //Visita el canal para ver más cosas sobre programación, diseño 3d y videojuegos :D 
    //https://youtube.com/c/GameDevTraum
    //
    //Suscríbete!

    private Camera camera;
    void Start()
    {

        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
