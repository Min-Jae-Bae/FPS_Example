using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PatrolNChase
{
    public class PathManager : MonoBehaviour
    {
        public static PathManager instance;
        void Awake()
        {
            instance = this;
        }

        public Transform[] points;

        void Start()
        {
            points = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                points[i] = transform.GetChild(i);
            }
        }
    } 
}
