using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{

    public GameObject player;
    public PlayerMechanics playerMechanics;
    public GameObject enemy;
    public bool playerEntered = false;

    //On Collision Enter
    void OnTriggerEnter2D(Collider2D collision) 
    {

        if (!playerEntered)
        {

            var playerPos = player.transform.position;
            playerPos.y = playerPos.y - 6;
            enemy.transform.position = playerPos;
        
            //Round about way to increase enemy speed
            var playerScript = player.GetComponent<CollectionHandler>();
            playerScript.numOfCollections = 15;
            
            //Set end trigger to true
            playerMechanics.endTriggerActive = true;
            playerEntered = true;
        }
        
    }

    //Add way to close game on end screen
    void Update()
    {
        if (playerMechanics.endTriggerActive)
        {
              if (Input.GetKeyDown(KeyCode.Escape))
              {
                Application.Quit();
              }
        }
    }

}
