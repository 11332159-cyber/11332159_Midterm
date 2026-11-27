using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyDestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
