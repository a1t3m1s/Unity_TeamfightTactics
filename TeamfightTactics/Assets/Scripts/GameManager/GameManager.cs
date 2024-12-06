using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Nav Mesh")]
    [field: SerializeField]
    private NavMeshSurface surface;

    [Header("Hex Grid Game Objects")]
    [field: SerializeField]
    private GameObject playerHexGrid;
    private List<GameObject> playerUnits;
    [field: SerializeField]
    private GameObject enemyHexGrid;
    private List<GameObject> enemyUnits;

    [Header("Bundles")]
    [field: SerializeField]
    private GameObject planeHelder;
    [field: SerializeField]
    private GameObject playerBenchHelder;
    private List<Transform> playerBench;
    [field: SerializeField]
    private GameObject enemyBenchHelder;

    [Header("Buttons")]
    [field: SerializeField]
    private Button startFightBtn;

    private void Awake()
    {
        startFightBtn.onClick.AddListener(StartFightOnClick);

        playerHexGrid.SetActive(false);

        playerBench = new List<Transform>();
        int childCounter = playerBenchHelder.transform.childCount;
        for (int i=0; i<childCounter; ++i)
        {
            playerBench.Add(playerBenchHelder.transform.GetChild(i));
        }
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

    #region StartFightBtn
    private void SetActiveGO(bool isEnable)
    {
        planeHelder.SetActive(isEnable);
        playerBenchHelder.SetActive(isEnable);
        enemyBenchHelder.SetActive(isEnable);
    }

    private void GetAllUnitsFromHexGrid(ref GameObject hexGrid, ref List<GameObject> units)
    {
        units = new List<GameObject>();
        int hexGridCounter = hexGrid.transform.childCount;
        for (int i = 0; i < hexGridCounter; ++i)
        {
            Transform hexTileTransform = hexGrid.transform.GetChild(i);
            if(hexTileTransform.childCount > 0)
            {
                units.Add(hexTileTransform.GetChild(0).gameObject);
            }
        }
    }

    private void SetMeshAgentComponent(ref List<GameObject> units)
    {
        for(int i=0; i< units.Count; ++i)
        {
            units[i].AddComponent<NavMeshAgent>();
        }
    }

    private void StartFightOnClick()
    {
        SetActiveGO(false);

        GetAllUnitsFromHexGrid(ref playerHexGrid, ref playerUnits);
        GetAllUnitsFromHexGrid(ref enemyHexGrid, ref enemyUnits);

        SetMeshAgentComponent(ref playerUnits);
        SetMeshAgentComponent(ref enemyUnits);

        surface.BuildNavMesh();
        
        SetActiveGO(true);


    }
    #endregion


}
