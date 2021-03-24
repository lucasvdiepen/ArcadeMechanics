using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackDelay = 1f;

    public int minDamage = 5;
    public int maxDamage = 15;

    [HideInInspector] public bool canAttack = false;
}
