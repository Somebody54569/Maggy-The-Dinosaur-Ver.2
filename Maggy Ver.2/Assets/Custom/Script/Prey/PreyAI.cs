using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PreyAI : MonoBehaviour
{   
    /*[SerializeField] float moveSpeed = 5f; // Speed at which the enemy moves
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float detectionRadius = 5f; // Radius to detect the player
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private Animator animator;
    [SerializeField] GameObject detectionArea; // Reference to the detection area visualization GameObject
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private bool alreadyRun;
    
    private void Start()
    {
        alreadyRun = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        detectionArea.transform.position = transform.position;
        detectionArea.transform.localScale = new Vector3(detectionRadius, 0.1f, detectionRadius);
        
        if (distanceToPlayer <= detectionRadius && playerMovement.isDead == false)
        {
            if (player.position.z - gameObject.transform.position.z <= 10)
            {
                alreadyRun = true;
                // Calculate the direction to move away from the player
                Vector3 runDirection = (transform.position - player.position).normalized;
                
                runDirection.x = 0;
                runDirection.y = 0;
                
                // Update the rotation to face the runDirection
                Quaternion targetRotation = Quaternion.LookRotation(runDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                
                transform.position += runDirection * moveSpeed * Time.deltaTime;
                
                animator.SetBool("isRunning", true);
            }
            else
            {
                Vector3 runDirection = (transform.position - player.position).normalized;
                
                runDirection.x = 0;
                runDirection.y = 0;
                moveSpeed = 0;
                transform.position += runDirection * moveSpeed * Time.deltaTime;
                animator.SetBool("isRunning", false);
            }
            
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (player.position.z > gameObject.transform.position.z)
        {
            detectionArea.SetActive(false);
            moveSpeed = 0;
        }
    }*/
    [SerializeField] float moveSpeed = 5f; // Speed at which the enemy moves
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float detectionRadius = 5f; // Radius to detect the player
    [SerializeField] private Transform player; // Reference to the player's transform
    [SerializeField] private Animator animator;
    [SerializeField] GameObject detectionArea; // Reference to the detection area visualization GameObject
    [SerializeField] PlayerMovement playerMovement;
    
    private bool isRunning;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        isRunning = false;
    }

    private void Update()
    {
        if (playerMovement.isDead)
        {
            // Stop running if the player is dead
            isRunning = false;
            animator.SetBool("isRunning", false);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        detectionArea.transform.position = transform.position;
        detectionArea.transform.localScale = new Vector3(detectionRadius, 0.1f, detectionRadius);
        
        if (distanceToPlayer <= detectionRadius && !isRunning)
        {
            // Calculate the direction to move away from the player
            Vector3 runDirection = (transform.position - player.position).normalized;

            // Set the x and y components to 0
            runDirection.x = 0;
            runDirection.y = 0;

            // Update the rotation to face the runDirection
            Quaternion targetRotation = Quaternion.LookRotation(runDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            
            transform.position += runDirection * moveSpeed * Time.deltaTime;
            
            animator.SetBool("isRunning", true);

            isRunning = true; // Set to true after starting to run
        }
        else if (isRunning)
        {
            // Continue running if already started
            Vector3 runDirection = (transform.position - player.position).normalized;
            
            // Set the x and y components to 0
            runDirection.x = 0;
            runDirection.y = 0;
            
            transform.position += runDirection * moveSpeed * Time.deltaTime;
            animator.SetBool("isRunning", true);
        }
        if (player.position.z > gameObject.transform.position.z)
        {
            detectionArea.SetActive(false);
            moveSpeed = 0;
            animator.SetBool("isRunning", false);
        }
    }
    
    
    

    
}
