using UnityEngine;
using System.Collections;

public class CONTENT_Driller : MonoBehaviour 
{
    public ParticleSystem Laser;

    public void Awake()
    {
        GetComponent<Rigidbody2D>().centerOfMass = new Vector2(0, 0.05f);
        Laser.enableEmission = false;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        Laser.enableEmission = true;
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        Laser.enableEmission = false;
    }
}
