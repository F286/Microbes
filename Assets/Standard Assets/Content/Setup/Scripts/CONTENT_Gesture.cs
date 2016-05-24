using UnityEngine;
using System.Collections;

public class CONTENT_Gesture : MonoBehaviour 
{
    public void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            var overlap = Physics2D.OverlapCircle(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition), 0.35f, LayerMask.NameToLayer("Input"));

            if (overlap)
            {
                print(overlap);
            }
        }
	}
}
