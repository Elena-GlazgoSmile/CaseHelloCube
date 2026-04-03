using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    public Collider colliderCube1;
    public Collider colliderCube2;

    void Start()
    {
        Physics.IgnoreCollision(colliderCube1, colliderCube2);
    }

    void Update()
    {
        
    }
}
