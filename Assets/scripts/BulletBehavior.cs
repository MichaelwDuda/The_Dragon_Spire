using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("hit " + other.name);
        Destroy(gameObject);
    }
}
