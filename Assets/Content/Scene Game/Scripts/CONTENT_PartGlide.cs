using UnityEngine;
using System.Collections;

public class CONTENT_PartGlide : Part 
{
    public float force = 6.2f;

    public void Use(Vector2 forward)
    {
        foreach (var item in microbe.bodies)
        {
            item.AddForce(forward * Time.fixedDeltaTime * force, ForceMode2D.Impulse);
        }
    }

    public override void NetworkUpdate () 
    {
        if (Input.GetKey("d"))
        {
            Use(Vector2.right);
        }
        if (Input.GetKey("a"))
        {
            Use(Vector2.left);
        }
	}
}
