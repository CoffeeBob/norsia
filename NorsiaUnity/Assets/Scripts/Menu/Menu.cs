using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public enum MenuTypes { Main, Options }
    public enum TransitionTypes { Instant, Fade }
    public MenuTypes MenuType;
    public TransitionTypes transitionIn;
    public TransitionTypes transitionOut;

    public void Show()
    {
        gameObject.SetActive(true);
        switch (transitionOut)
        {
            case TransitionTypes.Instant:
                InstantShow();
                break;

            case TransitionTypes.Fade:
                StartCoroutine(FadeIn());
                break;

            default:
                InstantShow();
                break;
        }
    }

    public void Hide()
    {
        switch (transitionOut)
        {
            case TransitionTypes.Instant:
                InstantHide();
                break;

            case TransitionTypes.Fade:
                StartCoroutine(FadeOut());
                break;

            default:
                InstantHide();
                break;
        }
    }

    public void InstantShow()
    {
        gameObject.SetActive(true);
        SetAlphaValue(1f);
    }
    public void InstantHide()
    {
        gameObject.SetActive(false);
    }

    private void SetAlphaValue(float alphaValue)
    {
        tk2dSprite[] buttons = GetComponentsInChildren<tk2dSprite>();
        tk2dTextMesh[] text = GetComponentsInChildren<tk2dTextMesh>();

        for (int i = 0; i < buttons.Length; i++)
        {
            Color c = buttons[i].color;
            c.a = alphaValue;
            buttons[i].color = c;
        }

        for (int i = 0; i < text.Length; i++)
        {
            Color c = text[i].color;
            c.a = alphaValue;
            text[i].color = c;
            text[i].Commit();
        }
    }

    private IEnumerator FadeIn()
    {
        float alphaValue = 0;

        // Fade in
        while (alphaValue < 1)
        {
            SetAlphaValue(alphaValue);
            alphaValue += Time.deltaTime / 0.1f;
            yield return null;
        }

        // Set alpha to 1 to make sure its fully displayed
        SetAlphaValue(1);
        yield return null;
    }
    private IEnumerator FadeOut()
    {
        float alphaValue = 1;

        // Fade out
        while (alphaValue > 0)
        {
            SetAlphaValue(alphaValue);
            alphaValue -= Time.deltaTime / 0.1f;
            yield return null;
        }

        // Set alpha to 0 to make sure its fully hidden
        SetAlphaValue(0);
        gameObject.SetActive(false);
        yield return null;
    }
}
