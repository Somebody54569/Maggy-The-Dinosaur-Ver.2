using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class HealthDecreaseStop : MonoBehaviour
{
    [SerializeField] private float healthBoostDuration = 5f;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private int rdNum;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        rdNum = Random.Range(0, 1);
        if (rdNum == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                // Stop health decrease and prevent damage for the specified duration
                player.StopHealthDecrease();
                player.PreventDamage(healthBoostDuration);
                Destroy(gameObject);
            }
        }
    }
}
