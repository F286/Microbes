using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CONTENT_Microbe : MonoBehaviour 
{
    public List<Rigidbody2D> pieces;
    public float repelDistance = 0.5f;
    public float repelForce = 1f;
    public byte guid;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
