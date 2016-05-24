using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class CONTENT_Microbe : NetworkBehaviour 
{
    
    [Command]
    public void CmdHook(NetworkInstanceId netId, Vector2 position, Vector2 direction, float shootSpeed)
    {
//        RpcHook(netId, position, direction, shootSpeed);

        var g = Instantiate((GameObject)Resources.Load("Microbe Hook"));
        g.transform.position = position;// + direction.normalized;
        g.GetComponent<Rigidbody2D>().rotation = direction.ToAngle();
        g.GetComponent<Rigidbody2D>().velocity = direction.normalized * shootSpeed;

        g.GetComponent<CONTENT_Hook>().microbe = NetworkServer.FindLocalObject(netId).GetComponent<CONTENT_Microbe>();
        //g.GetComponent<CONTENT_Microbe>()

        NetworkServer.Spawn(g);
//        NetworkServer.SpawnWithClientAuthority(g, NetworkServer.FindLocalObject(netId));

        RpcHook(g, netId, direction);
    }
    [ClientRpc]
    public void RpcHook(GameObject g, NetworkInstanceId netId, Vector2 direction)
    {
        if (direction.x < 0)
        {
            g.GetComponent<SpriteRenderer>().flipY = true;
        }

//        print(netId);
        foreach (var item in GameObject.FindGameObjectsWithTag("microbe"))
        {
            if (item.GetComponent<CONTENT_Microbe>().netId == netId)
            {
                foreach (var p in item.GetComponent<CONTENT_Microbe>().bodies)
                {
                    Physics2D.IgnoreCollision(g.GetComponent<Collider2D>(), p.GetComponent<Collider2D>());
                }                    
            }
        }
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
//        if (isLocalPlayer)
//        {
//            GameObject prev = null;
//            for (int i = 0; i < 4; i++)
//            {
//                var r = (GameObject)Resources.Load("Microbe Circle");
//                GameObject g = (GameObject)Instantiate(r, transform.position + new Vector3(i * 0.5f, 0), transform.rotation);
//                g.transform.parent = transform;
//
//                if (prev == null)
//                {
//                    GameObject.Destroy(g.GetComponent<SpringJoint2D>());
//                }
//                else
//                {
//                    g.GetComponent<SpringJoint2D>().connectedBody = prev.GetComponent<Rigidbody2D>();
//                }
//                prev = g;
//
//                if (i == 1)
//                {
//                    g.AddComponent<CONTENT_PartGlide>();
//                }
//
////            NetworkServer.Spawn(g);
//            }
//        }
//        var network = GetComponents<NetworkTransformChild>();
//        for (int i = 0; i < network.Length; i++)
//        {
////            print(i);
//            if (i < network.Length && i < bodies.Length)
//            {
//                network[i].target = bodies[i].transform;
//            }
//        }
//        foreach (var item in Network)
//        {
//            item.target = 
//        }
//        GetComponent<NetworkTransformChild>().target = bodies[0].transform;
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
    }

}
