using UnityEngine;

public class Star
{
    public string name;
    //자식들이 사용할 수 있게 virtual을 지정한다.
    public virtual void Rotate()
    {
        Debug.Log("오른쪽으로 회전한다. 회전 속도는 10이다.");
    }

}

public class Earth : Star
{
    // 부모의 기능을 사용한다.
    public override void Rotate()
    {
        base.Rotate();
        Debug.Log("왼쪽으로 회전한다. 회전 속도는 10이다.");
    }
}

public class ClassExample : MonoBehaviour
{
    public void Start()
    {
        Earth earth = new Earth();
        earth.Rotate();
    }

}
