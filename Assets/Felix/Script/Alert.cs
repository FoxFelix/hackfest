using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    public Monster monster;
    void OnTriggerEnter(Collider other)
    {
        monster.FightEnter(other);
    }
}
