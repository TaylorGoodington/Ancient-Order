using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OverworldController : MonoBehaviour
{
    [SerializeField] float maxSpeed = 5f;

    NavMeshAgent nav;
    Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = maxSpeed;
    }

    public void RecieveInput(Vector2 input)
    {
        DetermineState(input);
        Move(input);
    }

    private void DetermineState (Vector2 input)
    {
        
    }

    private void Move(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isRunning", true);
            nav.destination = transform.position + direction;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}