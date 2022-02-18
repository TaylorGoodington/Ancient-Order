using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldUIController : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TransitionOut()
    {
        anim.Play("TransitionOut");
    }
}
