using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionGizmo : MonoBehaviour
{
    [SerializeField] private bool isEnemy;

    private void OnDrawGizmos()
    {
        Gizmos.color = !isEnemy ? Color.white : Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
