using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CONTENT_Microbe : NetworkBehaviour 
{
    [SyncVar]
    public float health = 100;
    [Command]
    public void CmdAddHealth(float add)
    {
        health += add;
    }
//    [Command]
//    public void CmdDestroyPart(int index)
//    {
//        RpcDestroyPart(index);
//    }
//    [ClientRpc]
//    public void RpcDestroyPart(int index)
//    {
//        bodies[index].gameObject.SetActive(false);
//    }
    [Command]
    public void CmdHook(NetworkInstanceId netId, Vector2 position, float angle)
    {
        var g = Instantiate((GameObject)Resources.Load("Microbe Hook"));
        g.transform.position = position;
        g.transform.rotation = Quaternion.Euler(0, 0, angle);

        g.GetComponent<CONTENT_Hook>().initialVelocity = g.transform.right;
        g.GetComponent<CONTENT_Hook>().microbeNetId = netId;

        NetworkServer.SpawnWithClientAuthority(g, NetworkServer.FindLocalObject(netId));
    }

    [Command]
    public void CmdDestroy(GameObject g)
    {
        Destroy(g);
    }

    public Rigidbody2D[] bodies
    {
        get
        {
            return gameObject.GetComponentsInChildren<Rigidbody2D>();
        }
    }
    public Part[] parts
    {
        get
        {
            return gameObject.GetComponentsInChildren<Part>();
        }
    }
    public float repelDistance = 4.73f;
    public float repelForce = -0.3f;
    public Transform cameraFollow;
    public TextMesh text;

    public Vector2 forward
    {
        get
        {
            var r = Vector2.zero;
            for (int i = 1; i < bodies.Length; i++)
            {
                r += bodies[i].position - bodies[i - 1].position;
            }
            r /= bodies.Length - 1;
            return r;
        }
    }
    public void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (var item in bodies)
            {
                item.gravityScale = 0;
                item.drag = 7.86f;
                item.angularDrag = 5;
            }
        }
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
            for (int j = 0; j < bodies.Length; j++) 
            {
                if (i != j)
                {
                    var d = bodies[j].position - bodies[i].position;
                    var m = d.magnitude;
                    if (m < repelDistance)
                    {
                        d = d.normalized * (repelDistance - m);
                        bodies[i].AddForce(d * repelForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                        bodies[j].AddForce(-d * repelForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }

    public void Update()
    {
        if (bodies.Length > 0)
        {
            var p = Vector2.zero;
            foreach (var item in bodies)
            {
                p += item.position;
            }
            p /= bodies.Length;
            cameraFollow.position = p;
        }
        if (isLocalPlayer)
        {
            foreach (var item in parts)
            {
                item.NetworkUpdate();
            }
        }
        text.text = health.ToString();
    }

}
