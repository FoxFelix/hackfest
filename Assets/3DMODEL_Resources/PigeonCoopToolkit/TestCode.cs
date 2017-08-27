using PigeonCoopToolkit.Effects.Trails;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TestCode : MonoBehaviour
{

    public List<TrailRenderer_Base> Trails;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Trails.ForEach(a => a.Emit = true);

            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 0.01f));

        }
        else
        {
            Trails.ForEach(a => a.Emit = false);
        }
    }

}
