using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class CONTENT_PartHook : Part 
{
    float lastShoot = -100;
    List<GameObject> shotHooks;
    bool haveShot = false;

    public override void NetworkUpdate()
    {
        if (lastShoot + 0.21f < Time.time && Input.GetMouseButtonDown(0))
        {
            lastShoot = Time.time;
            if (haveShot)
            {
                var d = (Vector2)Input.mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
                var a = d.ToAngle();
                StartCoroutine(Fire(a - 100 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.0f));
                StartCoroutine(Fire(a + 000 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.1f));
                StartCoroutine(Fire(a + 100 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.2f));
            }
            else
            {
                RemoveShot();
            }
            haveShot = !haveShot;
        }
//        if(lastShoot + 1 < Time.time && Input.GetMouseButtonDown(0) && !haveShot)
//        {
//            lastShoot = Time.time;
//
//            var d = (Vector2)Input.mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
//            var a = d.ToAngle();
//            StartCoroutine(Fire(a - 100 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.0f));
//            StartCoroutine(Fire(a + 000 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.1f));
//            StartCoroutine(Fire(a + 100 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.2f));
//
//            haveShot = true;
//        }
//        if (lastShoot + 0.3f < Time.time && Input.GetMouseButtonDown(0) && haveShot)
//        {
//            lastShoot = Time.time;
//            RemoveShot();
//            haveShot = false;
//        }
    }

    public void RemoveShot()
    {
        foreach (var item in GameObject.FindObjectsOfType<CONTENT_Hook>()) 
        {
            if (item.microbeNetId == id)
            {
                Destroy(item.gameObject);
            }
        }
    }

    public IEnumerator Fire(float angle, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        microbe.CmdHook(id, transform.position, angle);
    }
}
