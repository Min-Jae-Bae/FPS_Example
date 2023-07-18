using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 손을 IK로 제어하고싶다.
public class IKManager : MonoBehaviour
{
    Animator anim;
    public Transform leftHand;
    public Transform rightHand;
    public Transform lookTarget;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        MySetIK(AvatarIKGoal.LeftHand, leftHand);
        MySetIK(AvatarIKGoal.RightHand, rightHand);

        anim.SetLookAtWeight(1);
        anim.SetLookAtPosition(lookTarget.position);
    }

    void MySetIK(AvatarIKGoal goal, Transform target)
    {
        anim.SetIKPositionWeight(goal, 1);
        anim.SetIKRotationWeight(goal, 1);

        anim.SetIKPosition(goal, target.position);
        anim.SetIKRotation(goal, target.rotation);

    }
}
