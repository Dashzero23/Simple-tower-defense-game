using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public UnityEngine.UI.Image img;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn());    
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }
    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime * 2f;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }
    IEnumerator FadeOut(string Scene)
    {
        float t = 0f;

        while (t > 1f)
        {
            t -= Time.deltaTime * 2f;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(Scene);
    }
}
