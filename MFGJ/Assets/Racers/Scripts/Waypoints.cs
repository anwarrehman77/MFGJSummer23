using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, 0.1f);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }
    }

    public Transform GetNextWaypoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null) return transform.GetChild(0);

        return currentWaypoint.GetSiblingIndex() < transform.childCount - 1 ? transform.GetChild(currentWaypoint.GetSiblingIndex() + 1) : transform.GetChild(currentWaypoint.GetSiblingIndex());
    }
}
