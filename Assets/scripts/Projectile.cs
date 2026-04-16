using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float lifetime = 0.5f;          // How long before it disappears
    public Vector3 startScale = Vector3.zero;
    public Vector3 endScale = Vector3.one;
    public AnimationCurve scaleCurve;      // Optional for smooth scaling

    private float timer = 0f;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;

        // Scale over time
        if (scaleCurve != null)
            transform.localScale = Vector3.Lerp(startScale, endScale, scaleCurve.Evaluate(t));
        else
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

        // Destroy when done
        if (timer >= lifetime)
            Destroy(gameObject);
    }
}
