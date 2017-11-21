using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private GameObject droppedItemPrefab;

    public GameObject DroppedItemPrefab
    {
        get { return droppedItemPrefab; }
        set { droppedItemPrefab = value; }
    }
}
