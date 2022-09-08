using UnityEngine;

public enum UIAnimationState { Entry, Exit }

public abstract class AnimationGroup : MonoBehaviour
{
    // Animations loop when this is true.
    public bool loop;

    // Properties used for Syncronisation
    public bool playing { get; protected set; }
    public WaitUntil waitUntilNotPlaying { get; protected set; }

    // Initializes WaitUntil.
    private void Awake()
    {
        this.waitUntilNotPlaying = new WaitUntil(() => !playing);
    }

    // Plays the animation.
    public abstract void Play(UIAnimationState state);
}
