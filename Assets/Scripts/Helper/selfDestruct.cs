using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float deathTimer = 3f;
    // Start is called before the first frame update
    private void Update()
    {
        Destroy(gameObject, deathTimer);
    }
}
