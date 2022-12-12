using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueHandler : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI text;

    private Queue<string> sentences;


    // Opens the tutorial screen and displays the sentences one by one
    public void StartDialogue(TutorialDialogue tutorialDialogue)
    {

        sentences = new Queue<string>(); 

        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach (string sentence in tutorialDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //Code to type out the sentences one by one char
    IEnumerator TypeSentence(string sentence)
    {
        text.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }

    //Displays the next sentence in the queue
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Closes the box
    void EndDialogue()
    {
        animator.SetBool("isOpen", false);

    }
    
}
