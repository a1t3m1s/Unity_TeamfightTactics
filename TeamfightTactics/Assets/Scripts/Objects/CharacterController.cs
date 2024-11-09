using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController
{
    public int hitPoints; //hit points
    public int energyPoints; //energy points
    public float attackSpeed;
    public float magicResist;
    public float physicResist;

    protected virtual void UseUltimateSkill() { }
}
