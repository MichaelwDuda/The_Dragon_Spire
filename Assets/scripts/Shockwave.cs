using UnityEngine;

public class Shockwave : MonoBehaviour
{
    public float duration = 0.5f;
    public float maxScale = 5f;
    public AnimationCurve scaleCurve;
    public AnimationCurve alphaCurve;

    private SpriteRenderer sr;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / duration;

        // Scale outward
        float scale = Mathf.Lerp(0f, maxScale, scaleCurve.Evaluate(t));
        transform.localScale = new Vector3(scale, scale, scale);

        // Fade out
        if (sr != null)
        {
            Color c = sr.color;
            c.a = alphaCurve.Evaluate(t);
            sr.color = c;
        }

        // Destroy when done
        if (timer >= duration)
            Destroy(gameObject);
    }
}
