using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RoadInfo : MonoBehaviour
    {
        public static RoadInfo current;
        public float[] laneXPositions;
        public float roadEndZ;
        public float laneDistance;

        private void Awake()
        {
            laneDistance = Mathf.Abs(laneXPositions[0] - laneXPositions[1]);
            current = this;
        }

    }
}