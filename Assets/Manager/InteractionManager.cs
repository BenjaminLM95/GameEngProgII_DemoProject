using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("Interaction Setting")]
    private LayerMask interactableLayer;
    [SerializeField] private float interactionDistance = 3f;

    private InputManager inputManager => GameManager.Instance.InputManager;

    // Interface reference used internally
    private IInteractable currentFocusedInteractable;

    string DebugCurrentInteractable;

    private Transform cameraRoot; // Reference to the player's camera root Transform


    [Header("Interaction Cooldown")]
    [Tooltip("Time in seconds before the player can Interact again after a succesfull interaction")]
    private float interactionCooldown = 0.1f;
    float lastInteractionTime = -Mathf.Infinity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactableLayer = LayerMask.GetMask("Interactable");

        cameraRoot = GameManager.Instance.PlayerController.CameraRoot;
    }


    // Update is called once per frame
    void Update()
    {
        HandleInteractionDetection();
    }

    private void HandleInteractionDetection()
    {
        if (Physics.Raycast(cameraRoot.transform.position, cameraRoot.transform.forward, out RaycastHit hitInfo, interactionDistance, interactableLayer))
        {
            Debug.Log("Raycast hit object: " + hitInfo.collider.name);

            // Get the interactable component from the hit object
            IInteractable hitInteractable = hitInfo.collider.GetComponent<IInteractable>();

            if (hitInteractable != null)
            {
                if (hitInteractable != currentFocusedInteractable)
                {
                    // 1. Clear previous focus if we had one
                    if (currentFocusedInteractable != null)
                    {
                        currentFocusedInteractable.SetFocus(false);
                    }
                }

                // 2, set new Focused interactable
                currentFocusedInteractable = hitInteractable;
                currentFocusedInteractable.SetFocus(true);

                // 3. 

                // we hit an object with an interactable layer

                Debug.LogWarning("Hit object does not have an IINteractable component.");



            }

            currentFocusedInteractable = hitInteractable;

            currentFocusedInteractable.SetFocus(true);

            DebugCurrentInteractable = hitInfo.collider.name;

        }
        else if (currentFocusedInteractable != null)
        {
            currentFocusedInteractable.SetFocus(false);
            currentFocusedInteractable = null;

            DebugCurrentInteractable = null;
        }



    }

    private void OnEnable() 
    {
        inputManager.InteractInputEvent += OnInteract; 
    }

    private void OnInteract(UnityEngine.InputSystem.InputAction.CallbackContext context) 
    {
        if(context.performed) 
        {
            if(currentFocusedInteractable != null) 
            {
                currentFocusedInteractable.OnInteract(); 
            }
        }
    }

    private void OnDestroy() 
    {
        inputManager.InteractInputEvent -= OnInteract;
    }
    
}
