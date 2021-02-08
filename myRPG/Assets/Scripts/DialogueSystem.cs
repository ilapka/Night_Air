using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] RectTransform backPanel;
    [SerializeField] CameraControl cameraController;

    private string npcName;
    private List<string> dialogueLines;
    private Button continueButton;
    private Text dialogueText, nameText;
    private int dialogueIndex;
    private Animator npcAnim;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        Init();
    }

    private void Init()
    {
        continueButton = dialoguePanel.GetComponent<Transform>().Find("ContinueButton").GetComponent<Button>();
        dialogueText = dialoguePanel.GetComponent<Transform>().Find("Text").GetComponent<Text>();
        nameText = dialoguePanel.GetComponent<Transform>().Find("NamePanel").GetChild(0).GetComponent<Text>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialoguePanel.SetActive(false);
    }

    /// <summary>
    /// Add dialog window for animated NPCs
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="npcName"></param>
    /// <param name="anim"></param>
    public void AddNewDialogue(string[] lines, string npcName, Animator anim)
    {
        cameraController.thirdPersonEnable();
        npcAnim = anim;
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        CreateDialogue();
        npcAnim.SetTrigger("Talk");
    }

    /// <summary>
    /// Add dialog window for various static objects 
    /// </summary>
    /// <param name="lines"></param>
    /// <param name="npcName"></param>
    public void AddNewDialogue(string[] lines, string npcName)
    {
        cameraController.thirdPersonEnable();
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        this.npcName = npcName;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
        backPanel.gameObject.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            backPanel.gameObject.SetActive(false);
            cameraController.thirdPersonDisable();

            if (npcAnim != null)
            {
                npcAnim.SetTrigger("StopTalking");
                npcAnim = null;
            }
        }
    }
}
