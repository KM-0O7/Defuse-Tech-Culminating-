using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speak : MonoBehaviour, IDialogue
{
    private bool interacting = false;
    public bool isInteracting => interacting;
    [SerializeField] private float typingSpeed = 0.1f;
    private Coroutine dialogueRoutine;
    private Image dialogueBox;
    private TextMeshProUGUI textBox;
    private Animator dialogueAnimator;
    private bool textOn = false;
    private bool skippedText = false;
    private bool canSkip = false;
    [SerializeField] private string text;

    private void Start()
    {
        dialogueBox = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<Image>();
        dialogueAnimator = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<Animator>();
        textBox = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (textOn == true && skippedText == false && canSkip)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                skippedText = true;
                canSkip = false;
            }
        }
    }

    private IEnumerator InteractingCoroutine()
    {
        dialogueAnimator.SetTrigger("Show");
        PlayerMovement.canMove = false;
        yield return new WaitForSeconds(0.5f);
        
        textOn = true;
        dialogueBox.enabled = true;
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;

        canSkip = true;
        for (int j = 0; j < textBox.text.Length; j++)
        {
            textBox.maxVisibleCharacters += 1;
            if (text[j] == '.' || text[j] == ',' || text[j] == '!' || text[j] == '?' || text[j] == '-')
            {
                yield return new WaitForSeconds(typingSpeed + 0.5f);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }

            if (skippedText == true)
            {
                skippedText = false;
                textBox.maxVisibleCharacters = text.Length;
                break;
            }
        }
       
        yield return new WaitForSeconds(1.5f);
        textBox.maxVisibleCharacters = 0;
      
        textOn = false;
        yield return new WaitForSeconds(0.1f);
        skippedText = false;
        PlayerMovement.canMove = true;
        textBox.text = "";
        dialogueAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(1f);
        canSkip = false;
        dialogueBox.enabled = false;
        interacting = false;
    }

    public void Interact()
    {
        if (interacting == false)
        {
            interacting = true;
            dialogueRoutine = StartCoroutine(InteractingCoroutine());
        }
    }

    private void OnDisable()
    {
        if (interacting)
        {
            if (dialogueRoutine != null)
            {
                StopCoroutine(dialogueRoutine);
            }

            ResetDialogue();
        }
    }

    private void ResetDialogue()
    {
        Debug.Log("Dialogue Reset!");
        interacting = false;

        if (textBox != null)
        {
            textBox.text = "";
        }

        if (dialogueBox != null)
        {
            dialogueBox.enabled = false;
        }
    }
}