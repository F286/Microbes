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
            var a = d.ToAngle();
            StartCoroutine(Fire(a - 100 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.0f));
            StartCoroutine(Fire(a + 000 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.1f));
            StartCoroutine(Fire(a + 100 * Mathf.Sign(d.x) * Mathf.Deg2Rad, 0.2f));
        }
    }

    public IEnumerator Fire(float angle, float delay)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

//        var d = (Vector2)Input.mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
        microbe.CmdHook(id, transform.position, angle);
    }
}
