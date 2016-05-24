using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CONTENT_PartHook : Part 
{
//    public CONTENT_Microbe microbe;
//    public GameObject template;
    public float shootSpeed = 10f;

    public override void NetworkUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var d = (Vector2)Input.mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            microbe.CmdHook(id, transform.position, d, shootSpeed);
        }
    }
}
