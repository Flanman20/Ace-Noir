using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public Image dialogueBox;
    public GameObject textObject;
    public AudioSource audioSource;
    public AudioClip typeSound;
    public float textSpeed = 0.05f;

    public static DialogueSystem Instance;

    private string[] lines;
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

    public void ShowDialogue(string line)
    {
        ShowDialogue(new string[] { line });
    }

    public void ShowDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;
        isActive = true;

        dialogueBox.gameObject.SetActive(true);
        textObject.SetActive(true);

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
    }
}
