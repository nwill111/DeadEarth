using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutTrigger : MonoBehaviour
{
    public TutorialDialogue tutorialDialogue;


    public void Start()
    {
        //Triggers dialogue on start
        FindObjectOfType<DialogueHandler>().StartDialogue(tutorialDialogue);
    }


}
