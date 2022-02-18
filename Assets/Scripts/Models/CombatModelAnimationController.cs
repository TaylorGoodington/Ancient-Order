using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatModelAnimationController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(AnimationState animationState)
    {
        anim.Play(animationState.ToString());
    }
}