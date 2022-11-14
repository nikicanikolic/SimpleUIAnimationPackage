// Version: 14112022
using System.Collections;
using System.Collections.Generic;

public enum ChildrenAnimationType { Simultaneous, Consecutive }

// Animation group that animates it's children.
public class AnimateChildrenAnimationGroup : AnimationGroup
{
    // Parallel or Series.
    public ChildrenAnimationType animationType;

    // List of Animation groups attached to child objects, the elements are dynamicly added when Play() is called.
    private List<AnimationGroup> animations;

    public override void Play(UIAnimationState state)
    {
        GetChildren();

        this.playing = true;
        switch (state)
        {
            case UIAnimationState.Entry:
                PlayEntryAnmation();
                break;

            case UIAnimationState.Exit:
                PlayExitAnmation();
                break;
        }
    }

    // Adds animationGroups from clid objects to the list. 
    private void GetChildren()
    {
        animations = new List<AnimationGroup>();

        AnimationGroup[] animationGroups;
        for (int i = 0; i < transform.childCount; i++)
        {
            animationGroups = transform.GetChild(i).GetComponents<AnimationGroup>();

            for (int j = 0; j < animationGroups.Length; j++)
            {
                animations.Add(animationGroups[j]);
            }
        }
    }

    private void PlayEntryAnmation()
    {
        switch (animationType)
        {
            case ChildrenAnimationType.Consecutive:
                StartCoroutine(SeriesAnimation(UIAnimationState.Entry));
                break;

            case ChildrenAnimationType.Simultaneous:
                StartCoroutine(ParallelAnimation(UIAnimationState.Entry));
                break;
        }
    }

    private void PlayExitAnmation()
    {
        switch (animationType)
        {
            case ChildrenAnimationType.Consecutive:
                StartCoroutine(SeriesAnimation(UIAnimationState.Exit));
                break;

            case ChildrenAnimationType.Simultaneous:
                StartCoroutine(ParallelAnimation(UIAnimationState.Exit));
                break;
        }
    }

    // Starts all animations at the same time.
    private IEnumerator ParallelAnimation(UIAnimationState state)
    {
        foreach (AnimationGroup child in animations)
        {
            child.Play(state);
        }

        yield return animations[animations.Count - 1].waitUntilNotPlaying;

        this.playing = false;
    }

    // Waits for first animation group to finish playing before playing the next.
    private IEnumerator SeriesAnimation(UIAnimationState state)
    {
        if (animations.Count > 0)
        {
            foreach (AnimationGroup child in animations)
            {
                child.Play(state);
                yield return null;
                yield return child.waitUntilNotPlaying;
            }

            yield return animations[animations.Count - 1].waitUntilNotPlaying;
        }

        playing = loop;
        if (playing)
        {
            PlayEntryAnmation();
        }
    }
}
