using UnityEngine;
using System.Collections;

public class CONTENT_Hook : MonoBehaviour 
{
    public float rotateForce = 1;

    public void FixedUpdate()
    {
        var r = GetComponent<Rigidbody2D>();
        r.AddTorque(Mathf.DeltaAngle(r.rotation, r.velocity.ToAngle()) * rotateForce * Time.fixedDeltaTime, 
            ForceMode2D.Impulse);
    }
}
