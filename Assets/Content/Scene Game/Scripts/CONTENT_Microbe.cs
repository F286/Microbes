using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CONTENT_Microbe : NetworkBehaviour 
{
    public List<Rigidbody2D> pieces;
    public List<Part> parts;
    public float repelDistance = 0.5f;
    public float repelForce = 1f;
    public byte guid;
    public Transform cameraFollow;

    public Vector2 forward
    {
        get
        {
            var r = Vector2.zero;
            for (int i = 1; i < pieces.Count; i++)
            {
                r += pieces[i].position - pieces[i - 1].position;
            }
            r /= pieces.Count - 1;
            return r;
        }
    }


    public void FixedUpdate()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            for (int j = 0; j < pieces.Count; j++) 
            {
                if (i != j)
                {
                    var d = pieces[j].position - pieces[i].position;
                    var m = d.magnitude;
                    if (m < repelDistance)
                    {
                        d = d.normalized * (repelDistance - m);
                        pieces[i].AddForce(d * repelForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                        pieces[j].AddForce(-d * repelForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    public void Update()
    {
        var p = Vector2.zero;
        foreach (var item in pieces) 
        {
            p += item.position;
        }
        p /= pieces.Count;
        cameraFollow.position = p;

        if (isLocalPlayer)
        {
            foreach (var item in parts)
            {
                item.NetworkUpdate();
            }
        }
    }

}
