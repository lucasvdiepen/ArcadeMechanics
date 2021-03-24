using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float attackDelay = 1f;

    [HideInInspector] public bool canAttack = false;
}
