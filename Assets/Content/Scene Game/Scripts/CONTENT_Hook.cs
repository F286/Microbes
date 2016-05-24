using UnityEngine;
using System.Collections;

public class CONTENT_Hook : MonoBehaviour 
{
    public CONTENT_Microbe microbe;
    public float rotateForce = 1;

    public void FixedUpdate()
    {
        var r = GetComponent<Rigidbody2D>();
        r.AddTorque(Mathf.DeltaAngle(r.rotation, r.velocity.ToAngle()) * rotateForce * Time.fixedDeltaTime, 
            ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
        //if (coll.gameObject.tag == "Enemy")
        //    coll.gameObject.SendMessage("ApplyDamage", 10);

    }
}
