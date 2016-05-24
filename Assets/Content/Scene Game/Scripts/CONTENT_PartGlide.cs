using UnityEngine;
using System.Collections;

public class CONTENT_PartGlide : Part 
{
//    public CONTENT_Microbe microbe;
    public float force = 1;

    public void Use(Vector2 forward)
    {
        foreach (var item in microbe.pieces)
        {
            item.AddForce(forward * Time.fixedDeltaTime * force, ForceMode2D.Impulse);
        }
    }

    public override void NetworkUpdate () 
    {
        if (Input.GetKey("d"))
        {
            Use(Vector2.right);
//            Use(microbe.forward);
        }
        if (Input.GetKey("a"))
        {
            Use(Vector2.left);
//            Use(microbe.forward * -1);
        }
	}
}
