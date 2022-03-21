using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    [SerializeField] public string m_sName;
    [SerializeField] public Ability m_iMajor;
    [SerializeField] public Ability m_iMinor;
}

public enum Ability
{
    STRENGHT = 0,
    DEXTERITY = 1,
    INTELINGENCE = 2,
    SPEED = 3,
    CHARISMA = 4
}
