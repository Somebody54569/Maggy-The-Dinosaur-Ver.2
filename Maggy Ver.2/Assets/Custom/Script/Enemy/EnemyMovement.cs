using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed

    private void Update()
    {
        // Move the enemy forward along the Z-axis (straight line)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
