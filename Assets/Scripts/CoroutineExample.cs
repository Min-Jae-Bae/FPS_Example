using System;
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
        StartCoroutine(IEMove(Vector3.right, () =>
        {
            StartCoroutine(IEMove(Vector3.up, () =>
            {
                StartCoroutine(IEMove(Vector3.left, () =>
                {
                    StartCoroutine(IEMove(Vector3.down, () =>
                    {
                        StartCoroutine(IEMoveManager());
                    }));
                }));
            }));
        }));
        yield return 0;
    }

    IEnumerator IEMove(Vector3 move, Action callback)
    {
        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            transform.position += move * 5 * Time.deltaTime;
            yield return 0;

        }

        if (callback != null)
        {
            callback();
        }
    }
}
