using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [field: SerializeField]
    private GameObject playerHexGrid;
    [field: SerializeField]
    private GameObject enemyHexGrid;

    private void Awake()
    {
        playerHexGrid.SetActive(false);
        //enemyHexGrid.SetActive(false);
    }

    private void Update()
    {
        if(DraggableObject.isDraggable)
        {
            playerHexGrid.SetActive(true);
        }
        else
        {
            playerHexGrid.SetActive(false);
        }
    }
}
