using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CONTENT_PartHook : Part 
{
//    public CONTENT_Microbe microbe;
    public GameObject template;
    public float ShootSpeed = 1f;

    public void Use(NetworkInstanceId netId, Vector2 position, Vector2 direction)
    {
        var f = Instantiate(template);
        f.transform.position = position;
        f.GetComponent<Rigidbody2D>().rotation = direction.ToAngle();
        f.GetComponent<Rigidbody2D>().velocity = direction.normalized * ShootSpeed;

        if (direction.x < 0)
        {
            f.GetComponent<SpriteRenderer>().flipY = true;
        }

        foreach (var item in GameObject.FindGameObjectsWithTag("microbe"))
        {
            if (item.GetComponent<CONTENT_Microbe>().netId == netId)
            {
                foreach (var p in item.GetComponent<CONTENT_Microbe>().pieces) 
                {
                    Physics2D.IgnoreCollision(f.GetComponent<Collider2D>(), p.GetComponent<Collider2D>());
                }                    
            }
        }
    }

    public override void NetworkUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var d = (Vector2)Input.mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            Use(id, transform.position, d);
        }
    }
}
