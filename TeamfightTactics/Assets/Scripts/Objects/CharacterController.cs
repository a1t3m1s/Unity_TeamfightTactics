using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    //public int hitPoints; //hit points
    //public int energyPoints; //energy points
    //public float attackSpeed;
    //public float magicResist;
    //public float physicResist;

    public NavMeshAgent agent;
    public List<Transform> enemies;

    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;

    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    public bool alreadyAttack;

    [Header("States")]
    public float sightRange;
    public float attackRange;
    public bool enemyInSightRange;
    public bool enemyInAttackRange;

    public void SetCharacterSettings(List<GameObject> enemies, NavMeshAgent agent)
    {
        this.enemies = new List<Transform>();
        foreach(var enemy in enemies)
        {
            this.enemies.Add(enemy.transform);
        }

        this.agent = agent;
    }

    public void UpdateStatement()
    {
        //enemyInSightRange = Physics.CheckSphere();
    }

}
