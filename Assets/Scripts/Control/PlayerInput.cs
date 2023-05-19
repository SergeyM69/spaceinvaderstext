using SpaceInvaders.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool lockYAxis = true;

    private void Update()
    {
        var vertical = 0.0f;
        var horizontal = 0.0f;

        if (!lockYAxis)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                vertical += 1.0f;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                vertical -= 1.0f;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal -= 1.0f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal += 1.0f;
        }

        EventsHub.InvokeInputVectorChanged(new Vector2(horizontal, vertical));

        var isFireButtonHold = Input.GetKey(KeyCode.Space);
        EventsHub.InvokeFireButtonStateChanged(isFireButtonHold);
    }
}
