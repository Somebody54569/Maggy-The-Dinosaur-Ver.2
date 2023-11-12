using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent((typeof(CharacterController)))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float eatDistance = 2.0f;
    [SerializeField] private LayerMask preyLayer;

    private PlayerInput playerInput;
    private CharacterController controller;
    private Transform cameraTransform;
    private Vector3 playerVelocity;
    private Animator anim;

    private InputActionMap playerMap;

    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cameraTransform = Camera.main.transform;
        playerMap = playerInput.actions.FindActionMap("Game");
    }

    void Start()
    {

    }

    private void OnEnable()
    {
        playerMap.Enable();
    }

    private void OnDisable()
    {
        playerMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, 0);
        //Debug.Log(move)
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (playerInput.actions["Eat"].triggered)
        {
            EatOpponent();
        }
        controller.Move(playerVelocity * Time.deltaTime);

      
    }

    private void EatOpponent()
    {
        
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, eatDistance))
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Opponent"))
                {
                    Debug.Log("Eating opponent!");
                    Destroy(hit.collider.gameObject);
                    Debug.Log("Opponent destroyed.");
                }
            }
            else
            {
                Debug.Log("No opponents in front.");
            }
    }

    private void OnDrawGizmos()
    {
        Vector3 start = transform.position + Vector3.up * 1.5f;
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
}
