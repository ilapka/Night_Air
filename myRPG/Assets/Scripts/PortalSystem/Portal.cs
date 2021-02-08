using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public Vector3 TeleportLocation { get; set; }
    public string PortalName;

    [SerializeField] Portal[] linkedPortals;
    PortalController PortalController { get; set; }

    protected override void Start()
    {
        base.Start();
        PortalController = FindObjectOfType<PortalController>();
        TeleportLocation = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
    }

    public override void Interact()
    {
        base.Interact();
        if (PortalController != null)
        {
            PortalController.ActivatePortal(linkedPortals);
            Debug.Log("Все ок");
        }
        else
        {
            Debug.Log("PortalController = null");
        }
        playerAgent.ResetPath();
    }
}
