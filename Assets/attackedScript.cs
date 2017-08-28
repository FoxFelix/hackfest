using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackedScript : MonoBehaviour {
    public Monster monster;
    void OnTriggerEnter(Collider other)
    {
        monster.TriggerEvent(other);
    }
}
