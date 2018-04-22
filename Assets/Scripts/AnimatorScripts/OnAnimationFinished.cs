using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAnimationFinished : StateMachineBehaviour {
    
    //OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(stateInfo.normalizedTime >= 1f)
        {
            animator.SetBool("End", true);
        }
    }
}
