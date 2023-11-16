using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Services.RemoteConfig;

public class PlayerMovement : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }
    
    [Header("Speed & Distance")]
    [SerializeField] private float initialMoveSpeed = 10f;
    [SerializeField] private float maxMoveSpeed = 20f;
    [SerializeField] private float speedIncreaseRate = 1f; // The rate at which speed increases per second
    [SerializeField] private float speedIncreaseInterval = 5f; // Increase speed every 5 seconds
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float timeSinceLastSpeedIncrease = 0f;
    [SerializeField] private float eatDistance = 12.5f;
    [SerializeField] private float rotationSpeed = 5;
    [SerializeField] private float speedTakeHitDuration = 1.5f;

    [Header("Jump")]
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool comingDown = false;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private float jumpDuration = 0.45f;
    
    [Header("HP")]
    [SerializeField] private int startHP = 100;
    [SerializeField] private int currentHP = 100;
    [SerializeField] private float hpDecreaseRate = 2f;
    [SerializeField] private HealthBar healthBar;
    public bool isDead;

    [Header("Level Boundary")] 
    [SerializeField] private float minBoundary = -10f;
    [SerializeField] private float maxBoundary = 10f;
    
    [Header("Object")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject levelControl;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private GameObject infoDisplay;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject cameraPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject totalBoneDisplay;
    [SerializeField] private GameObject HighScoreDisplay;
    [SerializeField] private LayerMask preyLayer;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputActionMap playerMap;
    

    [Header("Ads")]
    [SerializeField] private InterstitialAdExample interstitialAdExample;
    [SerializeField] private BannerAdExample bannerAdExample;

    [Header("Camera")] 
    [SerializeField] private CameraShake cameraShake;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMap = playerInput.actions.FindActionMap("Game");
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        isDead = false;
        LevelDistance.disRun = 0;
        currentHP = startHP;
        audioSource = GetComponent<AudioSource>();
        healthBar.SetMaxHealth(startHP);
        currentMoveSpeed = initialMoveSpeed;
        StartCoroutine(DecreaseHPOverTime());

        while (!RemoteConfigSetting.Instance)
        {
            yield return null;
        }

        RemoteConfigService.Instance.FetchCompleted += SetupSpeedRemote;
        RemoteConfigService.Instance.FetchConfigs<userAttributes, appAttributes>(new userAttributes(), 
            new appAttributes());
    }

    void SetupSpeedRemote(ConfigResponse response)
    {
        float playerSpeed = RemoteConfigService.Instance.appConfig.GetFloat("playerSpeed");
        if (playerSpeed > 0)
        {
            initialMoveSpeed = playerSpeed;
        }
    } 
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            timeSinceLastSpeedIncrease += Time.deltaTime;
            if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
            {
                timeSinceLastSpeedIncrease = 0f;
                IncreaseSpeed();
            }
            
            transform.Translate(Vector3.forward * Time.deltaTime * currentMoveSpeed, Space.World);
            
            MovePlayer();
            Jump();
            EatOpponent();
        }

        if (currentMoveSpeed >= 20)
        {
            playerObject.GetComponent<Animator>().SetBool("fastRun",true);
        }
        else
        {
            playerObject.GetComponent<Animator>().SetBool("fastRun",false);
        }
    }
    
    void IncreaseSpeed()
    {
        if (!isDead)
        {
            if (currentMoveSpeed < maxMoveSpeed)
            {
                currentMoveSpeed += speedIncreaseRate;
            }
            
            // Ensure the player's speed doesn't exceed the max speed
            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, initialMoveSpeed, maxMoveSpeed);
        }
        
    }

    void MovePlayer()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 newPosition = transform.position + new Vector3(input.x, 0, 0) * currentMoveSpeed * Time.deltaTime;
        
        bool atBoundary = newPosition.x <= minBoundary || newPosition.x >= maxBoundary;

        if (atBoundary)
        {
            // If at the boundary, smoothly return the rotation to the center
            Quaternion centerRotation = Quaternion.Euler(0, 0, 0); // Center rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, centerRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minBoundary, maxBoundary);
            transform.position = newPosition;

            if (input != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
                // Limit the rotation to a specific range (e.g., -45 to 45 degrees)
                targetAngle = Mathf.Clamp(targetAngle, -45f, 45f);
                Quaternion rotation = Quaternion.Euler(0, targetAngle, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                // If there is no input, smoothly return the rotation to the center
                Quaternion centerRotation = Quaternion.Euler(0, 0, 0); // Center rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, centerRotation, Time.deltaTime * rotationSpeed);
            }
        }

        
    }
    
    private void EatOpponent()
    {
        if (playerInput.actions["Eat"].triggered)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, eatDistance))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Prey"))
                {
                    Debug.Log("Eating Prey!");
                    Destroy(hit.collider.gameObject);
                    IncreaseHP(10);
                    Debug.Log("Prey destroyed.");
                }
            }
            else
            {
                Debug.Log("No Prey in front.");
            }
        }
        
    }

    private void Jump()
    {
        if (playerInput.actions["Jump"].triggered)
        {
            if (isJumping == false)
            {
                isJumping = true;
                playerObject.GetComponent<Animator>().SetTrigger("isJumping");
                StartCoroutine(JumpSequence());
            }
        }

        if (isJumping == true)
        {
            if (comingDown == false)
            {
                transform.Translate(Vector3.up * Time.deltaTime * jumpForce, Space.World);
            }

            if (comingDown == true)
            {
                transform.Translate(Vector3.up * Time.deltaTime * -jumpForce, Space.World);
            }
        }
    }

    IEnumerator JumpSequence()
    {
        yield return new WaitForSeconds(jumpDuration);
        comingDown = true;
        yield return new WaitForSeconds(jumpDuration);
        isJumping = false;
        comingDown = false;
        gameObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
    }
    private void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 end = start + transform.forward * eatDistance;

        if (Physics.Raycast(start, transform.forward, out RaycastHit hit, eatDistance, preyLayer))
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawLine(start, end);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Prey"))
        {
            Debug.Log("Hit prey");
            Destroy(other.gameObject);
            int damageAmount = 10;
            DecreaseHP(damageAmount);
            cameraShake.Shake();
            StartCoroutine(SpeedTakeHit());
        }

        if (other.CompareTag("Obstacle"))
        {
            Debug.Log($"Hit Obstacle: {other.gameObject.name}");
            Destroy(other.gameObject);
            int damageAmount = 20;
            DecreaseHP(damageAmount);
            cameraShake.Shake();
            StartCoroutine(SpeedTakeHit());
        }

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit Enemy");
            int damageAmount = 10;
            DecreaseHP(damageAmount);
            cameraShake.Shake();
            StartCoroutine(SpeedTakeHit());
        }
    }

    private IEnumerator DecreaseHPOverTime()
    {
        while (!isDead)
        {
            int damageAmount = 1;
            DecreaseHP(damageAmount);
            Debug.Log($"HP : {currentHP}");
            yield return new WaitForSeconds(hpDecreaseRate);
        }
    }
    
    private void DecreaseHP(int amount)
    {
        currentHP -= amount;
        healthBar.SetHealth(currentHP);
        if (currentHP <= 0)
        {
            Debug.Log("Game Over");
            isDead = true;
            StartCoroutine(Dead());
        }
    }
    private void IncreaseHP(int amount)
    {
        currentHP += amount;
        healthBar.SetHealth(currentHP);
        if (currentHP > startHP)
        {
            currentHP = startHP;
        }
    }

    private IEnumerator Dead()
    {
        if (isDead)
        {
            currentMoveSpeed = 0;
            playerObject.GetComponent<Animator>().SetBool("isDead", true);
            bannerAdExample.HideBannerAd();
            levelControl.GetComponent<LevelDistance>().enabled = false;
            yield return new WaitForSeconds(2f);
            int bones = PlayerPrefs.GetInt("totalBones");
            bones += CollectableControl.boneCount;
            PlayerPrefs.SetInt("totalBones",bones);
            
            if (LevelDistance.disRun > GameManager.gameManager.highScore)
            {
                int highScore = GameManager.gameManager.highScore;
                highScore = LevelDistance.disRun;
                PlayerPrefs.SetInt("highScore",highScore);
            }
            totalBoneDisplay.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("totalBones").ToString();
            HighScoreDisplay.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt("highScore").ToString();
            infoDisplay.SetActive(false);
            endGamePanel.SetActive(true);
            cameraPanel.SetActive(false);
            controlPanel.SetActive(false);
            pausePanel.SetActive(false);
            
        }
    }

    private IEnumerator SpeedTakeHit()
    {
        float beforeTakeHit = currentMoveSpeed;
        currentMoveSpeed = 15;
        timeSinceLastSpeedIncrease = 0f;
        yield return new WaitForSeconds(speedTakeHitDuration);
        currentMoveSpeed = beforeTakeHit;
        currentMoveSpeed = Mathf.Max(currentMoveSpeed, 15); // Ensure it doesn't go below 15
        timeSinceLastSpeedIncrease = 0f;
    }
}
