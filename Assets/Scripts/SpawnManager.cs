using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//일정시간마다 적공장에서 적을 생성하고싶다.
// 스폰목록중에 랜덤한 위치에 배치하고싶다.
public class SpawnManager : MonoBehaviour
{
    public float makeTime = 2;
    public GameObject enemyFactory;
    public Transform[] spawnList;
    //생성수를 최대갯수로 제한하고싶다.
    //만약 생성된 녀석이 파괴되면 생성수를 1 감소하고싶다.
    public int makeCount;
    public int maxMakeCount = 5;

    IEnumerator Start()
    {
        while (true)
        {
            // 만약 생성수가 최대수 미만이라면 생성하고싶다.
            //적공장을 생성한다.
            if (makeCount < maxMakeCount)
            {
                GameObject enemy = ObjectPool.instance.GetDeactiveObject();
                // 나는 내가 죽었어
                if (enemy != null)
                {
                    makeCount++;
                    // 스폰목록중에 랜덤한 위치에 배치하고싶다.
                    int randomIndex = Random.Range(0, spawnList.Length);
                    enemy.transform.position = spawnList[randomIndex].position;

                    // 생성후에 생성시간을 랜덤으로 정하고 싶다.
                    enemy.GetComponent<Enemy2>().Init(ImDie);

                }
                yield return new WaitForSeconds(makeTime);
                //랜덤 시간
                makeTime = Random.Range(1f, 2f);
            }
            yield return 0;
        }
    }

    internal void ImDie(GameObject enemy2)
    {
        makeCount--;
    }
}
