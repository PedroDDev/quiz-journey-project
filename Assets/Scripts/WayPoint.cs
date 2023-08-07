using UnityEngine;

[System.Serializable]
public class WayPoint
{
    [System.Serializable]
    public struct waypointData
    {
        public int index;
        public Transform[] waypoint;
    }

    public waypointData[] waypoints;
}
