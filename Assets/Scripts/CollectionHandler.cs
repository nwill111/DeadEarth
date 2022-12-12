using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectionHandler : MonoBehaviour
{

    public int numOfCollections = 0;
    public GridMaker gridHandler;
    public TextMeshProUGUI textMesh;
    [SerializeField] private bool allItemsCollected;

    //Handles the events that happen when items are collected (turning up enemy speed, updating UI, etc.)
    void Update()
    {
        //Update enemy speed
        gridHandler.maxSpeed = numOfCollections * 0.7f;

        //Update UI
        if (numOfCollections < 6)
        {
            textMesh.SetText("Keys Collected: " + numOfCollections.ToString() + "/5");
        }
        
    }

  
}
