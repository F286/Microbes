using UnityEngine;
using System.Collections;

public class CONTENT_Camera : MonoBehaviour 
{
    public float Smooth = 0.2f;
    public float offset = 2;
    public Transform target;
    Vector2 Velocity;

	public void Update () 
    {
        var o = Vector2.Scale(Input.mousePosition, new Vector2(1f / Screen.width, 1f / Screen.height)) * offset;
//        Vector2 target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 set = Vector2.SmoothDamp(
            (Vector2)transform.position, (Vector2)target.position + o, ref Velocity, Smooth);
        set.z = -10;
        transform.position = set;
	}
}
