using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IEMoveManager());
    }

    IEnumerator IEMoveManager()
    {
        while (true)
        {
            yield return StartCoroutine(IEMove(Vector3.right));
            yield return StartCoroutine(IEMove(Vector3.up));
            yield return StartCoroutine(IEMove(Vector3.left));
            yield return StartCoroutine(IEMove(Vector3.down));
        }
    }

    IEnumerator IEMove(Vector3 move)
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            transform.position += move * 5 * Time.deltaTime;
            yield return 0;

        }
    }
}
