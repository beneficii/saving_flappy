using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyMe : MonoBehaviour
{
    public float delay = 3f;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
