using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 오브젝트풀로 관리할 객체들을 미리 만들어서 목록으로 가지고 있고싶다.
// 해당 객체를 필요로 하는 다른 객체가 있다면 미리 비활성화된 객체를 반환하고싶다.
// 만약 Enemy2가 죽었어요. init함수 호출 시 모든 것을 초기화 할 수 있어야한다.
// 필요요소
// 공장 목록
// 각 공장 객체 목록
// 공장별 구분자
public class ObjectPool : MonoBehaviour
{
    enum FactoryName
    {
        Enemy2 = 0,
    }
    public int poolCount = 10;
    public List<GameObject> pool;
    public static ObjectPool instance;

    void Awake()
    {
        instance = this;
        string factoryName = FactoryName.Enemy2.ToString();
        GameObject factory = Resources.Load<GameObject>(factoryName);
        pool = new List<GameObject>();
        for (int i = 0; i < poolCount; i++)
        {
            GameObject obj = Instantiate(factory);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }



    public GameObject GetDeactiveObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeSelf == false)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        return null;
    }
}
