using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public Monster monster;
    void OnTriggerEnter(Collider other)
    {
        monster.OnTriggerEvent(other);
    }
}
