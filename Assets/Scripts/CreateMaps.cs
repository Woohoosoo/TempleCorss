using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour
{
    public GameObject _middle;

    private void OnEnable()
    {
        Init();
    }


    void Init()
    {
        int random = Random.Range(1, 3);
        for (int i = 0; i < random; i++)
        {
            GameObject middle = Instantiate(_middle, transform);
            middle.transform.SetSiblingIndex(1);
        }
    }
}
