using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class DATA_Setup : MonoBehaviour 
{
    #if UNITY_EDITOR
    public void Update () 
    {
        var f = GameObject.FindObjectsOfType<Transform>();
        for (int i = 0; i < f.Length; i++)
        {
            if (f[i].GetComponent<SpriteRenderer>())
            {
                f[i].GetComponent<SpriteRenderer>().sortingOrder = f[i].GetSiblingIndex();
                f[i].GetComponent<SpriteRenderer>().sortingLayerName = NumberOfParents(f[i]).ToString();
            }
        }
	}
    public int NumberOfParents(Transform t)
    {
        if (t)
        { return 1 + NumberOfParents(t.parent); }
        else
        { return 0; }
    }
    #endif
}
