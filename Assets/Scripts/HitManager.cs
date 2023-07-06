using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Image Hit를 번쩍이는 느낌으로 껏다 켜고싶다.
public class HitManager : MonoBehaviour
{
    public static HitManager instance;
    public GameObject imageHit;

    private void Awake()
    {
        instance = this;
    }
    // 번쩍이는 기능을 호출할 함수를 만들고 싶다.

    private void Start()
    {
        imageHit.SetActive(false);
    }
    public void DoHit()
    {
        if (crt != null)
        {
            StopCoroutine(crt);
        }
        crt = StartCoroutine(IEHit(0.3f));
    }

    Coroutine crt;
    // 번쩍이는 코투린 함수를 만들고 싶다.
    IEnumerator IEHit(float time)
    {
        //1.imageHit를 보이게 하고싶다.
        imageHit.SetActive(true);
        //2. 0.1초 기다렸다가
        yield return new WaitForSeconds(time);
        //3. imageHit를 보이지 않게 하고싶다.
        imageHit.SetActive(false);
    }
}
