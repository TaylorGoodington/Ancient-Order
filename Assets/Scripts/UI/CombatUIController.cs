using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUIController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TransitionInStart()
    {
        anim.Play("TransitionInStart");
    }

    public void TransitionInEnd()
    {
        anim.Play("TransitionInEnd");
    }
}