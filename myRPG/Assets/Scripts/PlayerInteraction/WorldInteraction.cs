using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Class for player movement and interaction
/// </summary>

public class WorldInteraction : MonoBehaviour
{

    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] ParticleSystem AirResistance;

    Animator playerAnimator;
    NavMeshAgent playerAgent;
    GameObject interactedObject;
    Interactable currentInteractedObject;
    GameObject destinationPointer;
    GameObject currentDestinationPointer;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAgent = GetComponent<NavMeshAgent>();
        destinationPointer = Resources.Load<GameObject>("destinationPointer");
    }

    void Update()
    {
        //IsPointerOverGameObject() - check if the mouse was clicked on UI element
        if (Input.GetKeyDown(KeyCode.Mouse0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            GetInteraction();

        OnPlayerStoped();
    }

    void OnPlayerStoped()
    {
        if (!playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                playerAnimator.SetFloat("Speed", 0f);
                if (currentDestinationPointer != null)
                    Destroy(currentDestinationPointer.gameObject);
                AirResistance.Stop();
            }
        }
    }

    void GetInteraction()
    {
        AirResistance.Stop();

        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            playerAgent.updateRotation = true;
            interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject == currentDestinationPointer || (currentInteractedObject != null && interactedObject.GetComponent<Interactable>() == currentInteractedObject))
            {
                DoubleSpeed();
            }
            else
            {
                ClearingPastProcess();

                if (interactedObject.CompareTag("Enemy"))
                {
                    playerAgent.stoppingDistance = 2f;
                    currentInteractedObject = interactedObject.GetComponent<Interactable>();
                    currentInteractedObject.MoveToInteraction(playerAgent);
                }
                else if (interactedObject.CompareTag("Interactable Object"))
                {
                    playerAgent.stoppingDistance = 0.5f;
                    currentInteractedObject = interactedObject.GetComponent<Interactable>();
                    currentInteractedObject.MoveToInteraction(playerAgent);
                }
                else //Go to pointer
                {
                    playerAgent.stoppingDistance = 0f;
                    currentDestinationPointer = Instantiate(destinationPointer, interactionInfo.point, Quaternion.identity);
                    playerAgent.destination = interactionInfo.point;
                    currentInteractedObject = null;
                }

                playerAgent.speed = walkSpeed;
                playerAnimator.SetFloat("Speed", 0.5f);
            }
        }
    }

    void ClearingPastProcess()
    {
        if (currentInteractedObject != null)
        {
            currentInteractedObject.TurnOffBacklight();
            currentInteractedObject.playerAgent = null;
        }

        if (currentDestinationPointer != null)
            Destroy(currentDestinationPointer.gameObject);
    }

    void DoubleSpeed()
    {
        playerAgent.speed = runSpeed;
        playerAnimator.SetFloat("Speed", 1f);
        AirResistance.Play();
        interactedObject.gameObject.GetComponent<IDoubleSpeed>().DoubleSpeeedInteract();
    }

    public void PlayerStop()
    {
        playerAnimator.SetFloat("Speed", 0f);
        playerAgent.SetDestination(transform.position);
    }
}
