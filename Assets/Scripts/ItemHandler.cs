using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public GameObject player;
    public AudioSource collectSound;
    [SerializeField] public CollectionHandler collectionHandler;

    // Create a reference to the CollectionHandler script
    void Start()
    {
        collectionHandler = player.GetComponent<CollectionHandler>();
    }

    //When the player enters the trigger, the item is collected and the sound is played
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            collectSound.Play();
            collectionHandler.numOfCollections += 1;
            gameObject.SetActive(false);
          
        }
    }
}
