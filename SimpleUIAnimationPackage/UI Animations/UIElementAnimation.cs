using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Animation for all UI elements besides text.

[RequireComponent(typeof(CanvasGroup))]
public class UIElementAnimation : AnimationGroup
{
    [SerializeField] private float animationTime;

    private WaitForSeconds waitForAnimationTime;

    #region Move
    public bool move;
    [SerializeField] private AnimationCurve xMoveCurve;
    [SerializeField] private AnimationCurve yMoveCurve;
    public Vector2 startPosition;
    public Vector2 endPosition;
    private RectTransform rectTransform;

    #region Editor Functions
    public void SetStartPosition()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        startPosition = rectTransform.anchoredPosition;
    }
    public void SetEndPosition()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        endPosition = rectTransform.anchoredPosition;
    }

    public void ShowStartPosition()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPosition;
    }
    public void ShowEndPosition()
    {
        rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = endPosition;
    }
    #endregion

    //Position Update
    private void UpdateLocation(Vector2 position)
    {
        float xValue = 1;
        float yValue = 1;
        if (startPosition.x != endPosition.x)
        {
            xValue = (position.x - startPosition.x) / (endPosition.x - startPosition.x);
        }
        if (startPosition.y != endPosition.y)
        {
            yValue = (position.y - startPosition.y) / (endPosition.y - startPosition.y);
        }
        float xPos = startPosition.x + xMoveCurve.Evaluate(xValue) * (endPosition.x - startPosition.x);
        float yPos = startPosition.y + yMoveCurve.Evaluate(yValue) * (endPosition.y - startPosition.y);
        gameObject.transform.localPosition = new Vector2(xPos, yPos);
    }
    #endregion

    #region Scale
    public bool scale;
    [SerializeField] private AnimationCurve xScaleCurve;
    [SerializeField] private AnimationCurve yScaleCurve;
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;

    #region Editor Functions
    public void SetStartScale()
    {
        startScale = gameObject.transform.localScale;
    }
    public void SetEndScale()
    {
        endScale = gameObject.transform.localScale;
    }
    public void ShowStartScale()
    {
        gameObject.transform.localScale = startScale;
    }
    public void ShowEndScale()
    {
        gameObject.transform.localScale = startScale;
    }
    #endregion

    //Scale Update
    private void UpdateScale(Vector3 scale)
    {
        float xValue = 1;
        float yValue = 1;
        if (startScale.x != endScale.x)
        {
            xValue = (scale.x - startScale.x) / (endScale.x - startScale.x);
        }
        if (startScale.y != endScale.y)
        {
            yValue = (scale.y - startScale.y) / (endScale.y - startScale.y);
        }
        float xScale = startScale.x + xScaleCurve.Evaluate(xValue) * (endScale.x - startScale.x);
        float yScale = startScale.y + yScaleCurve.Evaluate(yValue) * (endScale.y - startScale.y);
        gameObject.transform.localScale = new Vector2(xScale, yScale);
    }
    #endregion

    #region Rotate
    public bool rotate;
    [SerializeField] private AnimationCurve zRotationCurve;
    public Quaternion startRotation;
    public Quaternion endRotation;

    #region Editor Functions
    public void SetStartRotation()
    {
        startRotation = gameObject.transform.localRotation;
    }
    public void SetEndRotation()
    {
        endRotation = gameObject.transform.localRotation;
    }
    public void ShowStartRotation()
    {
        gameObject.transform.localRotation = startRotation;
    }
    public void ShowEndRotation()
    {
        gameObject.transform.localRotation = endRotation;
    }
    #endregion

    //Rotation Update
    private void UpdateRotation(float rotation)
    {
        float zValue = 1;
        if (startRotation.z != endRotation.z)
        {
            zValue = (rotation - startRotation.eulerAngles.z) / (endRotation.eulerAngles.z - startRotation.eulerAngles.z);
        }
        float zRot = startRotation.eulerAngles.z + zRotationCurve.Evaluate(zValue) * (endRotation.eulerAngles.z - startRotation.eulerAngles.z);
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, zRot);
    }
    #endregion

    #region AlphaFade
    public bool alphaFade;
    [SerializeField] private float startAlpha;
    [SerializeField] public float endAlpha;
    private CanvasGroup canvasGroup;

    //Update Alpha
    private void UpdateAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    #endregion

    #region ColorChange
    public bool colorChange;
    [SerializeField] private Color startColor;
    [SerializeField] public Color endColor;
    private Image image;

    #region Editor Functions
    public void SetStartColor()
    {
        this.image = GetComponent<Image>();
        startColor = image.color;
    }
    public void SetEndColor()
    {
        this.image = GetComponent<Image>();
        endColor = image.color;
    }
    public void ShowStartColor()
    {
        this.image = GetComponent<Image>();
        image.color = startColor;
    }
    public void ShowEndColor()
    {
        this.image = GetComponent<Image>();
        image.color = endColor;
    }
    #endregion

    private void UpdateColor(Color color)
    {
        image.color = color;
    }
    #endregion

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        rectTransform = gameObject.GetComponent<RectTransform>();

        waitForAnimationTime = new WaitForSeconds(animationTime);
    }

    public override void Play(UIAnimationState state)
    {
        if (gameObject.activeInHierarchy)
        {
            this.playing = true;
            StartCoroutine(PlayingCountdown());
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

    // Used for looping and updating playing bool;
    private IEnumerator PlayingCountdown()
    {
        yield return waitForAnimationTime;
        playing = loop;
        if (loop)
        {
            Play(UIAnimationState.Entry);
        }
    }

    private void PlayEntryAnmation()
    {
        if (move)
        {
            LeanTween.value(gameObject, UpdateLocation, startPosition, endPosition, animationTime);
        }
        if (scale)
        {
            LeanTween.value(gameObject, UpdateScale, startScale, endScale, animationTime);
        }
        if (rotate)
        {
            LeanTween.value(gameObject, UpdateRotation, startRotation.eulerAngles.z, endRotation.eulerAngles.z, animationTime);
        }
        if (alphaFade)
        {
            LeanTween.value(gameObject, UpdateAlpha, startAlpha, endAlpha, animationTime);
        }
        if (colorChange)
        {
            LeanTween.value(gameObject, UpdateColor, startColor, endColor, animationTime);
        }
    }

    private void PlayExitAnmation()
    {
        if (move)
        {
            LeanTween.value(gameObject, UpdateLocation, endPosition, startPosition, animationTime);
        }
        if (scale)
        {
            LeanTween.value(gameObject, UpdateScale, endScale, startScale, animationTime);
        }
        if (rotate)
        {
            LeanTween.value(gameObject, UpdateRotation, endRotation.eulerAngles.z, startRotation.eulerAngles.z, animationTime);
        }
        if (alphaFade)
        {
            LeanTween.value(gameObject, UpdateAlpha, endAlpha, startAlpha, animationTime);
        }
        if (colorChange)
        {
            LeanTween.value(gameObject, UpdateColor, endColor, startColor, animationTime);
        }
    }
}
