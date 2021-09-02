using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimationParamterControl : MonoBehaviour {
    
    private void AnimationEventPlayFootstepSound1() {

        SoundManager.PlaySound(Sound.Step1);
    }

    private void AnimationEventPlayFootstepSound2() {

        SoundManager.PlaySound(Sound.Step2);
    }
}
