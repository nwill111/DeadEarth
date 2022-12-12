using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public GameObject player;
    [SerializeField] public CollectionHandler collectionHandler;
    [SerializeField] public int itemNum;

    // Start is called before the first frame update
    void Start()
    {
        collectionHandler = player.GetComponent<CollectionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectionHandler.numOfCollections >= itemNum)
        {
            gameObject.SetActive(false);
        }
    }
}
