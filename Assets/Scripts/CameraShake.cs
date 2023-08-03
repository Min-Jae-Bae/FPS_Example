using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    void Awake()
    {
        instance = this;
    }

    Coroutine cor;
    public void Play(float time, float intensity)
    {
        Stop();
        cor = StartCoroutine(IEPlay(time, intensity));
    }

    public void Stop()
    {
        if (cor == null) return;
        StopCoroutine(cor);

    }

    IEnumerator IEPlay(float time, float intensity)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            transform.localPosition = Random.insideUnitSphere * intensity * 0.1f;
            yield return 0;
        }
    }
}
