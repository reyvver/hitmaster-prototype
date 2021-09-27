using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRagdollControl : MonoBehaviour
{
    public Animator Animator;

    public Rigidbody[] AllRigidbodies;

    private void Awake()
    {
    }

    void Start()
    {
        ChangeRigidbodyType(true);
       
        //   Animator.enabled = true;
    }

    void Update()
    {

    }

    private void ChangeRigidbodyType(bool isKinematic)
    {
        foreach (var currentRigidBody in AllRigidbodies)
        {
            currentRigidBody.isKinematic = isKinematic;

        }

        if (!isKinematic)
            Animator.enabled = false;
        

    }
}
