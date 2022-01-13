using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryZone : MonoBehaviour
{
    public MonoBehaviour objToBeActivated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            objToBeActivated.enabled = true;
            Destroy(gameObject);
        }
    }
}
