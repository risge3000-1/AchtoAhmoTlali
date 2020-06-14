using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;

    public Text dialogueText;

    public Animator animator;

    public AudioSource botSounds,
                       playerSounds;

    public delegate void DialogueEvents();
    public static event DialogueEvents PlayerIsInADialogue, PlayerIsNotInADialogue;

    void Awake()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue (Dialogue dialogue)
    {
        PlayerIsInADialogue();
        
        animator.SetBool("IsOpen", true);
        
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }
   
    public void DisplayNextSentence()
    {
        //jusp to prevent a bug
        botSounds.Stop();
        playerSounds.Stop();

        if (sentences.Count == 0)
        {
            EndDialogue();
            
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();

        if (sentence.StartsWith("AI:"))
        {
            botSounds.Play();
        }
        else if (sentence.StartsWith("You:"))
        {
            playerSounds.Play();
        }
        
        StartCoroutine(TypeSentence(sentence));
    }

    public void EndDialogue()
    {
        PlayerIsNotInADialogue();
        
        animator.SetBool("IsOpen", false);
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        botSounds.Stop();
        playerSounds.Stop();
    }
}
