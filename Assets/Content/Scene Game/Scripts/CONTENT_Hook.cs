using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CONTENT_Hook : NetworkBehaviour 
{
    [SyncVar]
    public Vector2 initialVelocity;
    [SyncVar]
    public NetworkInstanceId microbeNetId;

    public Rigidbody2D attach;
    public ParticleSystem particles;
    public float rotateForce = 1;
    public CONTENT_Microbe microbe;

    ParticleSystem.Particle[] p = new ParticleSystem.Particle[100];

    public override void OnStartClient()
    { 
        base.OnStartClient();

        GetComponent<Rigidbody2D>().velocity = initialVelocity * 17;

        var direction = transform.right;

        if (direction.x < 0)
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }

        CONTENT_Microbe find = null;
        var distance = float.PositiveInfinity;
        foreach (var item in GameObject.FindGameObjectsWithTag("microbe"))
        {
            var d = item.GetComponent<CONTENT_Microbe>().cameraFollow.position - transform.position;
            var m = d.sqrMagnitude;
            if(m < distance)
            {
                distance = m;
                find = item.GetComponent<CONTENT_Microbe>();
            }
        }
        if (find)
        {
            var part = find.gameObject.GetComponentInChildren<CONTENT_PartHook>();
            attach = part.GetComponent<Rigidbody2D>();

            foreach (var item in find.GetComponentsInChildren<Collider2D>())
            {
                Physics2D.IgnoreCollision(item, GetComponent<Collider2D>());
            }
            microbe = find;
        }
    }

    public void FixedUpdate()
    {
        var r = GetComponent<Rigidbody2D>();
        r.AddTorque(Mathf.DeltaAngle(r.rotation, r.velocity.ToAngle()) * rotateForce * Time.fixedDeltaTime, 
            ForceMode2D.Impulse);

        if (attach && r.isKinematic)
        {
            var diff = r.position - attach.position;
            attach.AddForce(diff * Time.fixedDeltaTime * 185);
//            attach.AddForce(diff * Time.fixedDeltaTime * 120);
        }
    }

    public void Update()
    {
        if (attach != null)
        {
            var s = attach.position;
            var e = transform.position;

            particles.GetParticles(p);
            for (int i = 0; i < 100; i++)
            {
                p[i].position = Vector3.Lerp(s, e, i / 100f);
            }
            particles.SetParticles(p, 100);
        }
    }

    void OnCollisionEnter2D(Collision2D coll) 
    {
//        print(this.hasAuthority);
        if (this.hasAuthority)
        {
            CmdSetKinematic();
            if (coll.rigidbody)
            {
                var p = coll.rigidbody.GetComponentInParent<CONTENT_Microbe>();
                if (p)
                {
                    Rigidbody2D hit = null;
                    int index = 0;
                    for (int i = 0; i < p.bodies.Length; i++)
                    {
                        var item = p.bodies[i];
                        if (item == coll.rigidbody)
                        {
                            hit = item;
                            index = i;
                        }
                    }
                    if (hit)
                    {
//                        p.health -= 5;
                        CmdDamage(p.netId, -5);
//                        p.CmdAddHealth(-10);
                        CmdDestroy();

//                        microbe.health -= 10;
//                        Destroy(hit);
//                        hit.gameObject.SetActive(false);
//                        p.CmdDestroyPart(index);
//                        Destroy(gameObject);
                    }
                }
            }
        }

    }
    [Command]
    public void CmdDamage(NetworkInstanceId netId, float add)
    {
        var l = NetworkServer.FindLocalObject(netId);
        if(l)
        {
            l.GetComponent<CONTENT_Microbe>().health += add;
        }
    }
    [Command]
    public void CmdDestroy()
    {
        Destroy(gameObject);
    }

    [Command]
    public void CmdSetKinematic()
    {
        RpcSetKinematic();
    }

    [ClientRpc]
    public void RpcSetKinematic()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
    }
}

