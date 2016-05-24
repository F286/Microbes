using UnityEngine;
using System.Collections;

public class CONTENT_Planet : MonoBehaviour 
{
    public float Rotation = 1;

    public void Awake()
    {
        GetComponent<Rigidbody2D>().angularVelocity = Rotation * 3.8f;
    }

//    public void FixedUpdate()
//    {
//        GetComponent<Rigidbody2D>().rotation = Rotation;
//    }
}
