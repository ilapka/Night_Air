using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PortalController : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject backPanel;
    [SerializeField] GameObject portalPanel;
    [SerializeField] Player player;
    private Portal[] portal;
    private NavMeshAgent playerNavMeshAgent;

    void Start()
    {
        player = FindObjectOfType<Player>();
        portalPanel.SetActive(false);
        playerNavMeshAgent = player.GetComponent<NavMeshAgent>();
    }

    public void ActivatePortal(Portal[] portals)
    {
        backPanel.SetActive(true);
        portalPanel.SetActive(true);
        for (int i = 0; i < portals.Length; i++)
        {
            Button portalButton = Instantiate(button, portalPanel.transform);
            portalButton.GetComponentInChildren<Text>().text = portals[i].PortalName;
            int x = i;
            portalButton.onClick.AddListener(delegate { OnPortalButtonClick(x, portals[x]); });
        }

        Button closeButton = Instantiate(button, portalPanel.transform);
        closeButton.GetComponentInChildren<Text>().text = "Закрыть";
        closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    void OnPortalButtonClick(int portalIndex, Portal portal)
    {
        playerNavMeshAgent.enabled = false;
        player.transform.position = portal.TeleportLocation;
        Debug.Log(portal.name + portal.TeleportLocation);
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
        backPanel.SetActive(false);
        portalPanel.SetActive(false);
        playerNavMeshAgent.enabled = true;
    }

    void OnCloseButtonClick()
    {
        foreach (Button button in GetComponentsInChildren<Button>())
        {
            Destroy(button.gameObject);
        }
        backPanel.SetActive(false);
        portalPanel.SetActive(false);
    }
}
