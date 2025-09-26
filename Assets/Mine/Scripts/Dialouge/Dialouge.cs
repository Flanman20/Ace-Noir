using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [Header("UI References")]
    public GameObject speakerObject;            // Box for speaker name
    public TextMeshProUGUI speakerComponent;    // Text for speaker name
    public TextMeshProUGUI textComponent;       // Text for dialogue
    public Image dialogueBox;                   // Background for dialogue
    public GameObject textObject;               // Container for text

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip typeSound;

    [Header("Settings")]
    public float textSpeed = 0.05f;

    public static DialogueSystem Instance;

    private string[] lines;
    private string currentSpeaker;
    private int index;
    private bool isActive;

    void Awake()
    {
        Instance = this;
        HideDialogue();
    }

    void Update()
    {
        if (isActive && Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
                NextLine();
            else
                SkipLine();
        }
    }

    // New ShowDialogue with speaker name required
    public void ShowDialogue(string speaker, string line)
    {
        ShowDialogue(speaker, new string[] { line });
    }

    public void ShowDialogue(string speaker, string[] dialogueLines)
    {
        currentSpeaker = speaker;
        lines = dialogueLines;
        index = 0;
        isActive = true;

        // Activate UI
        dialogueBox.gameObject.SetActive(true);
        textObject.SetActive(true);
        speakerObject.SetActive(true);

        // Set speaker name
        if (speakerComponent)
            speakerComponent.text = currentSpeaker;

        StartCoroutine(TypeLine());
    }

    void SkipLine()
    {
        StopAllCoroutines();
        textComponent.text = lines[index];
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            HideDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach (char c in lines[index])
        {
            textComponent.text += c;
            if (audioSource) audioSource.PlayOneShot(typeSound);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void HideDialogue()
    {
        isActive = false;
        dialogueBox.gameObject.SetActive(false);
        textObject.SetActive(false);
        speakerObject.SetActive(false);
    }
}
