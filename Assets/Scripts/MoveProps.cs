using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveProps : MonoBehaviour
{
    float speed = 0.05f;

    void Start()
    {
        Application.targetFrameRate = 60;
        float y = Random.Range(-5.0f, 5.0f);
        float x = -4.0f;
        transform.position = new Vector2(x, y);
    }

    
    void Update()
    {
    
        transform.position += Vector3.right * speed;
        
    }
}
