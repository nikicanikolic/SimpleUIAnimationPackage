using System.Collections;
using TMPro;
using UnityEngine;

public enum TextAnimationType { LetterByLetter, Fade }

// Animation group that animates TextMeshPro Text.
public class TextAnimationGroup : AnimationGroup
{
    [SerializeField] private float animationTime;
    [SerializeField] private TextAnimationType animationType;
    private TMP_Text textObjectToAnimate;

    private void Awake()
    {
        textObjectToAnimate = GetComponent<TMP_Text>();
    }

    public override void Play(UIAnimationState state)
    {
        if (gameObject.activeInHierarchy)
        {
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
    }

    private void PlayEntryAnmation()
    {
        switch (animationType)
        {
            case TextAnimationType.LetterByLetter:
                StartCoroutine(LetterByLetterEntryAnimation());
                break;
            case TextAnimationType.Fade:
                StartCoroutine(FadeEntryAnimation());
                break;
        }
    }
    private void PlayExitAnmation()
    {
        switch (animationType)
        {
            case TextAnimationType.LetterByLetter:
                StartCoroutine(LetterByLetterExitAnimation());
                break;
            case TextAnimationType.Fade:
                StartCoroutine(FadeExitAnimation());
                break;
        }
    }


    #region LetterByLetter    
    private IEnumerator LetterByLetterEntryAnimation()
    {
        int textIndex = 0;
        while (textIndex <= textObjectToAnimate.text.Length)
        {
            textIndex += 2;
            textObjectToAnimate.maxVisibleCharacters = textIndex;
            yield return new WaitForSeconds(animationTime / textObjectToAnimate.text.Length);
        }
        this.playing = false;
    }

    private IEnumerator LetterByLetterExitAnimation()
    {
        LeanTween.value(textObjectToAnimate.gameObject, UpdateTextAlpha, 1f, 0f, animationTime);
        yield return new WaitForSeconds(animationTime);
        textObjectToAnimate.maxVisibleCharacters = 0;
        yield return null;
        textObjectToAnimate.color = new Color(textObjectToAnimate.color.r, textObjectToAnimate.color.g, textObjectToAnimate.color.b, 1);
        this.playing = false;
    }
    #endregion

    #region Fade
    private IEnumerator FadeEntryAnimation()
    {
        LeanTween.value(textObjectToAnimate.gameObject, UpdateTextAlpha, 0f, 1f, animationTime);
        yield return new WaitForSeconds(animationTime);
        this.playing = false;
    }

    private IEnumerator FadeExitAnimation()
    {
        LeanTween.value(textObjectToAnimate.gameObject, UpdateTextAlpha, 1f, 0f, animationTime);
        yield return new WaitForSeconds(animationTime);
        this.playing = false;
    }

    private void UpdateTextAlpha(float value)
    {
        textObjectToAnimate.color = new Color(textObjectToAnimate.color.r, textObjectToAnimate.color.g, textObjectToAnimate.color.b, value);
    }

    #endregion
}
