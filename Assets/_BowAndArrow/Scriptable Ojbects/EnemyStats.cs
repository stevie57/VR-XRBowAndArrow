using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public int health;
    public int strength;
    public int speed;
}
