using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SyncingOption { WaitUntil, WaitForSeconds, None }

// Animation group used to syncronise other animations using WaitForSeconds and WaitUntil.
public class SyncronisationAnimationGroup : AnimationGroup
{
    //List of animations to play when Play(Entry) is called.
    public List<SyncronisationBlock> entryAnimations = new List<SyncronisationBlock>();

    //List of animations to play when Play(Exit) is called.
    public List<SyncronisationBlock> exitAnimations = new List<SyncronisationBlock>();

    public override void Play(UIAnimationState state)
    {
        this.playing = true;
        switch (state)
        {
            case UIAnimationState.Entry:
                StartCoroutine(StartEntryAnimations());
                break;

            case UIAnimationState.Exit:
                StartCoroutine(StartExitAnimations());
                break;
        }
    }

    private IEnumerator StartEntryAnimations()
    {
        foreach (SyncronisationBlock block in entryAnimations)
        {
            switch (block.syncOption)
            {
                case SyncingOption.WaitUntil:
                    yield return new WaitUntil(() => !block.waitFor.playing);
                    break;

                case SyncingOption.WaitForSeconds:
                    yield return new WaitForSeconds(block.delayFor);
                    break;
            }
            block.animationGroup.Play(block.animationToPlay);
        }

        yield return new WaitUntil(() => !entryAnimations[entryAnimations.Count - 1].animationGroup.playing);

        this.playing = false;
    }

    private IEnumerator StartExitAnimations()
    {
        foreach (SyncronisationBlock block in exitAnimations)
        {
            switch (block.syncOption)
            {
                case SyncingOption.WaitUntil:
                    yield return new WaitUntil(() => !block.waitFor.playing);
                    break;

                case SyncingOption.WaitForSeconds:
                    yield return new WaitForSeconds(block.delayFor);
                    break;
            }
            block.animationGroup.Play(block.animationToPlay);
        }

        yield return exitAnimations[exitAnimations.Count - 1].animationGroup.waitUntilNotPlaying;

        this.playing = false;
    }

    // Function called from editor button allows to copy animations from entryAnimations to exitExit in a reverse order.
    public void InvertEntryToExit()
    {
        exitAnimations.Clear();
        for (int i = entryAnimations.Count - 1; i >= 0; i--)
        {
            SyncronisationBlock oldBlock = entryAnimations[i];
            SyncronisationBlock newBlock = new SyncronisationBlock();
            newBlock.animationGroup = oldBlock.animationGroup;
            newBlock.animationToPlay = UIAnimationState.Exit;
            exitAnimations.Add(newBlock);
        }
    }
}

// Class that allows customisation of syncing between animation groups.
[Serializable]
public class SyncronisationBlock
{
    public AnimationGroup animationGroup;
    public UIAnimationState animationToPlay = UIAnimationState.Entry;

    //Syncronisation options (Delay in seconds /  Wait for another AnimationGroup to finish).
    public SyncingOption syncOption = SyncingOption.None;
    public AnimationGroup waitFor;
    public float delayFor;
}
