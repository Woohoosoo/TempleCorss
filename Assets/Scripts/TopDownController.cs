using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvnet;

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvnet?.Invoke(direction);
    }
}


