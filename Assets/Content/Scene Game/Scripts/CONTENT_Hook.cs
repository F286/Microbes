using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CONTENT_Hook : NetworkBehaviour 
{
    [SyncVar]
    public Vector2 initialVelocity;
//    [SyncVar]
//    public NetworkInstanceId microbeId;
//    [SyncVar]
    public Transform attach;
//    public GameObject microbe2;
    public ParticleSystem particles;
    public float rotateForce = 1;

    ParticleSystem.Particle[] p = new ParticleSystem.Particle[100];

//    public void Awake()
//    {
//        print(Time.frameCount);
//    }

//    override ons

//    override 

    public override void OnStartClient()
    { 
        base.OnStartClient();

//        NetworkIdentity n = null;
//        foreach (var item in GameObject.FindObjectsOfType<NetworkIdentity>())
//        {
//            if (item.netId == microbeId)
//            {
//                n = item;
//                break;
//            }
//        }
//        print(n);


//        print(Time.frameCount);

//        print(microbeId);
//        print(NetworkServer.FindLocalObject(microbeId));

//        print(transform.position);
//        print(transform.right);

//        print(initialVelocity);

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
            attach = find.gameObject.GetComponentInChildren<CONTENT_PartHook>().transform;
        }
//        if (n)
//        {
//            foreach (var body in n.GetComponent<CONTENT_Microbe>().bodies)
//            {
//                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), body.GetComponent<Collider2D>());
//            }  
//        }
//        foreach (var item in GameObject.FindGameObjectsWithTag("microbe"))
//        {
//            if (item.GetComponent<CONTENT_Microbe>().netId == microbeId)
//            {
//                foreach (var body in item.GetComponent<CONTENT_Microbe>().bodies)
//                {
//                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), body.GetComponent<Collider2D>());
//                }                    
//            }
//        }
    }

    public void FixedUpdate()
    {
        var r = GetComponent<Rigidbody2D>();
        r.AddTorque(Mathf.DeltaAngle(r.rotation, r.velocity.ToAngle()) * rotateForce * Time.fixedDeltaTime, 
            ForceMode2D.Impulse);
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
        if (isServer)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
        }

    }
}

