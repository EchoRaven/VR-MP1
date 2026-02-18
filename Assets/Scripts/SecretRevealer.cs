using UnityEngine;
using System.Collections;

public class SecretRevealer : MonoBehaviour
{
    [Header("Objects to Control")]
    [SerializeField] private GameObject[] objectsToShow;
    [SerializeField] private GameObject[] objectsToHide;

    [Header("Animation Settings")]
    [SerializeField] private bool useAnimation = true;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float scaleUpDuration = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource revealSound;

    private bool isRevealed = false;

    public void Reveal()
    {
        if (isRevealed) return;

        isRevealed = true;

        if (revealSound != null)
        {
            revealSound.Play();
        }

        foreach (var obj in objectsToHide)
        {
            if (obj != null)
            {
                if (useAnimation)
                    StartCoroutine(FadeOut(obj));
                else
                    obj.SetActive(false);
            }
        }

        foreach (var obj in objectsToShow)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                if (useAnimation)
                    StartCoroutine(ScaleIn(obj));
            }
        }
    }

    public void Hide()
    {
        if (!isRevealed) return;

        isRevealed = false;

        foreach (var obj in objectsToShow)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        foreach (var obj in objectsToHide)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }

    private IEnumerator FadeOut(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material mat = renderer.material;
            Color startColor = mat.color;
            float elapsed = 0f;

            while (elapsed < fadeInDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeInDuration);
                mat.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }
        }

        obj.SetActive(false);
    }

    private IEnumerator ScaleIn(GameObject obj)
    {
        Vector3 targetScale = obj.transform.localScale;
        obj.transform.localScale = Vector3.zero;
        float elapsed = 0f;

        while (elapsed < scaleUpDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / scaleUpDuration;
            t = 1f - Mathf.Pow(1f - t, 3f);
            obj.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);
            yield return null;
        }

        obj.transform.localScale = targetScale;
    }

    public bool IsRevealed() => isRevealed;
}
