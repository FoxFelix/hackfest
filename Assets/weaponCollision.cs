using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponCollision : MonoBehaviour {


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            col.gameObject.SendMessage("GetDoFu");          

        }
    }
}
