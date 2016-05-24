using UnityEngine;
using System.Collections;

public class CONTENT_Player : MonoBehaviour 
{
    public float MoveForce = 1f;

    public void Update () 
    {
        var force = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            force.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            force.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            force.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            force.x -= 1;
        }

        GetComponent<Rigidbody2D>().AddForce(GetComponent<Rigidbody2D>().mass * force * Time.deltaTime * MoveForce);
	}
}
