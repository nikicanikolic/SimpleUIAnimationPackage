using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// OnClick Animation trigger for Buttons. Triggers assigned animations when the button is clicked.
[RequireComponent(typeof(Button))]
public class OnClickAnimationTrigger : MonoBehaviour
{
    [SerializeField] List<AnimationGroup> animationToPlay;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => animationToPlay.ForEach(animation => animation.Play(UIAnimationState.Entry)));
    }
}