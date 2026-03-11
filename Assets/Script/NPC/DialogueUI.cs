using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

// - Script for the Dialogue UI
// - 
// - Daniel Bruijn

public class DialogueUI : MonoBehaviour
{
    // - Variables
    public static DialogueUI Instance;

    [Header("References")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    // - Private
    List<string> lines;
    int currentLine;

    void Awake()
    {
        // - Sets the Dialogue Panel to Active
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // - Returns if active
        if (!dialoguePanel.activeSelf)
            return;

        // - If Player Presses E the Next Line will appear
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }
    }

    public void StartDialogue(List<string> dialogueLines)
    {
        Debug.Log("Starting Dialogue");
        
        // - Sets lines
        lines = dialogueLines;
        currentLine = 0;

        // - Sets DialoguePanel to Active and sets the currentLine
        dialoguePanel.SetActive(true);
        dialogueText.text = lines[currentLine];
    }

    void NextLine()
    {
        // - Goes to the next line
        currentLine++;

        // - If there are no more lines the dialogue will end
        if (currentLine >= lines.Count)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = lines[currentLine];
    }

    void EndDialogue()
    {
        // - End Dialogue and hide the Dialogue box
        dialoguePanel.SetActive(false);

        // - Return Camera to Player
        DialogueCameraController.Instance.ReturnCamera();
    }
}