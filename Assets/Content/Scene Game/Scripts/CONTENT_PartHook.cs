using UnityEngine;
using System.Collections;

public class CONTENT_PartHook : MonoBehaviour 
{
    public CONTENT_Microbe microbe;
    public GameObject template;
    public float ShootSpeed = 1f;

    public void Use(byte guid, Vector2 position, Vector2 direction)
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
            if (item.GetComponent<CONTENT_Microbe>().guid == guid)
            {
                foreach (var p in item.GetComponent<CONTENT_Microbe>().pieces) 
                {
                    Physics2D.IgnoreCollision(f.GetComponent<Collider2D>(), p.GetComponent<Collider2D>());
                }                    
            }
        }
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var d = (Vector2)Input.mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
            Use(microbe.guid, transform.position, d);
        }
    }
}
