using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCoroutine : MonoBehaviour
{
    public AnimationCurve curve;

    public float t;


    IEnumerator grow()
    {
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.one * curve.Evaluate(t);
            yield return null;
        }
    }

    public void callGrow()
    {
        t = 0;
        StartCoroutine(grow());
    }
}
