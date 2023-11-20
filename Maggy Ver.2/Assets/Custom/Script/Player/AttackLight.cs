using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLight : MonoBehaviour
{
    [SerializeField] private float attackRange = 10f;
    [SerializeField] private Light redLight; // Reference to the red light GameObject

    private void Update()
    {
        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        // Assuming the survivors are tagged as "Survivor"
        GameObject[] survivors = GameObject.FindGameObjectsWithTag("Survivor");

        bool survivorInAttackRange = false;

        foreach (GameObject survivor in survivors)
        {
            float distance = Vector3.Distance(transform.position, survivor.transform.position);

            if (distance <= attackRange)
            {
                survivorInAttackRange = true;
                // Perform attack or other actions here
            }
        }

        // Change the light color based on whether a survivor is in the attack range
        ChangeLightColor(survivorInAttackRange);
    }

    private void ChangeLightColor(bool survivorInAttackRange)
    {
        if (redLight != null)
        {
            if (survivorInAttackRange)
            {
                // Change light color to indicate a survivor is in the attack range (e.g., green)
                redLight.color = Color.green;
            }
            else
            {
                // Reset light color to red when no survivor is in the attack range
                redLight.color = Color.red;
            }
        }
    }
}
