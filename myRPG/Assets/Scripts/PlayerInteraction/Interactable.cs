using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(InteractionEffects))]
/// <summary>
/// Сlass that inherits by interactive objects  
/// </summary>

public class Interactable : MonoBehaviour, IDoubleSpeed
{
    [HideInInspector] public NavMeshAgent playerAgent;
    [SerializeField] Transform destinationAnchor;

    private InteractionEffects interactionEffects;
    private bool hasInteracted;
    private bool isEnemy;

    protected virtual void Start()
    {
        isEnemy = gameObject.CompareTag("Enemy");
        interactionEffects = GetComponent<InteractionEffects>();
    }

    public virtual void MoveToInteraction(NavMeshAgent playerAgent)
    {
        hasInteracted = false;
        this.playerAgent = playerAgent;
        playerAgent.destination = destinationAnchor.position;
        TurnOnBacklight();
    }
    

    private void Update()
    {
        if (!hasInteracted && playerAgent != null && !playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                if(!isEnemy)
                    Interact();
                EnsureLookDirection();
                hasInteracted = true;
            }
        }
    }

    /// <summary>
    /// make player direction on this object
    /// </summary>
    void EnsureLookDirection()
    {
        playerAgent.updateRotation = false;
        Vector3 lookDirection = new Vector3(transform.position.x, playerAgent.transform.position.y, transform.position.z);
        playerAgent.transform.LookAt(lookDirection);
        playerAgent.updateRotation = true;
    }
    
    public virtual void Interact()
    {
        TurnOffBacklight();
    }

    public void DoubleSpeeedInteract()
    {
        interactionEffects.TurnLightToDoubleSpeed();
    }

    public void TurnOffBacklight()
    {
        interactionEffects.TurnOffBacklight();
    }

    private void TurnOnBacklight()
    {
        interactionEffects.TurnOnBacklight();
    }
}
