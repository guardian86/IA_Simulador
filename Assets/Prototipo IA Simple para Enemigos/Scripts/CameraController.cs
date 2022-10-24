using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //----------Prototipo de inteligencia artificial simple para enemigos-------------
    //En la página web podés leer los artículos sobre este prototipo cuando estén listos
    //https://gamedevtraum.com
    //Visita el canal para ver más cosas sobre programación, diseño 3d y videojuegos :D 
    //https://youtube.com/c/GameDevTraum
    //
    //Suscríbete!

    [SerializeField]
    private Vector3 offSet;
    [SerializeField]
    private GameObject targetFollow;
    [SerializeField]
    private float speed;
    private Vector3 finalPosition;
    private Vector3 currentPosition;

    private float threshold = 0.01f;

    public Transform target;
    public float distance = 2.0f;
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float yMinLimit = -90f;
    public float yMaxLimit = 90f;
    public float distanceMin = 10f;
    public float distanceMax = 10f;
    public float smoothTime = 2f;
    float rotationYAxis = 0.0f;
    float rotationXAxis = 0.0f;
    float velocityX = 0.0f;
    float velocityY = 0.0f;
    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        rotationYAxis = angles.y;
        rotationXAxis = angles.x;
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
 
    void FixedUpdate()
    {
        currentPosition = gameObject.transform.position;
        finalPosition = targetFollow.transform.position - offSet;
        gameObject.transform.position = Vector3.Lerp(currentPosition, finalPosition, Time.deltaTime * speed);
        gameObject.transform.LookAt(target);

    }

}
