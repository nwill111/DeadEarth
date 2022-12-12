using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour
{

    public Sprite BlueleverActive;
    public Sprite RedleverActive;
    public Sprite YellowleverActive;
    public Sprite BlueleverInactive;
    public Sprite RedleverInactive;
    public Sprite YellowleverInactive;

    public bool blue = false;
    public bool red = false;
    public bool yellow = false;

    public GameObject lever;
    public GameObject player;

    public float interactionDistance = 2.0f;

    public bool leverActive = false;

    public TileChanger tileChanger;
    


    //Compare tag to determine which lever is being activated
    public void Start()
    {
        if (lever.CompareTag("BlueLever"))
        {
            blue = true;
        }
        else if (lever.CompareTag("RedLever"))
        {
            red = true;
        }
        else if (lever.CompareTag("YellowLever"))
        {
            yellow = true;
        }
    }

    public void Update()
    {
        //If user is close to lever, when pressed E sprite should change
        Vector3 leverPosition = lever.transform.position;
        Vector3 playerPosition = player.transform.position;
        float distance = Vector3.Distance(playerPosition, leverPosition);

        //Get Audio source
        AudioSource audioSource = GetComponent<AudioSource>();

        if (distance < interactionDistance)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                //Play sound
                audioSource.Play();

                if (!leverActive)
                {
                    if (blue)
                    {
                        lever.GetComponent<SpriteRenderer>().sprite = BlueleverActive;
                    }
                    else if (red)
                    {
                        lever.GetComponent<SpriteRenderer>().sprite = RedleverActive;
                    }
                    else if (yellow)
                    {
                        lever.GetComponent<SpriteRenderer>().sprite = YellowleverActive;
                    }

                    leverActive = true;

                    //Replace tiles
                    tileChanger.WallToFloor();



                }
                else
                {
                    if (blue)
                    {
                        lever.GetComponent<SpriteRenderer>().sprite = BlueleverInactive;
                    }
                    else if (red)
                    {
                        lever.GetComponent<SpriteRenderer>().sprite = RedleverInactive;
                    }
                    else if (yellow)
                    {
                        lever.GetComponent<SpriteRenderer>().sprite = YellowleverInactive;
                    }
                    leverActive = false;


                    //Replace tiles
                    tileChanger.FloorToWall();

                }





            }
        }
    }


    
}
