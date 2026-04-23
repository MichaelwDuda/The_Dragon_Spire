using UnityEngine;

public class earthSpike : MonoBehaviour
{
   public float lifetime = 0.5f;
    public float startHeight = 0f;     // Y scale at start
    public float endHeight = 1f;       // Y scale at full size
    public AnimationCurve scaleCurve;
    public float spawnYOffset = -0.5f;


    private float timer = 0f;
    private Vector3 baseScale;

    void Start()
    {
         // Move the projectile downward so it starts at ground level
        transform.position += new Vector3(0, spawnYOffset, 0);
        // Store original X/Z scale so only Y changes
        baseScale = transform.localScale;
        // Start flat on the ground
        transform.localScale = new Vector3(baseScale.x, startHeight, baseScale.z);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / lifetime;

        float height = scaleCurve != null
            ? Mathf.Lerp(startHeight, endHeight, scaleCurve.Evaluate(t))
            : Mathf.Lerp(startHeight, endHeight, t);

        // Only scale Y
        transform.localScale = new Vector3(baseScale.x, height, baseScale.z);

        if (timer >= lifetime)
            Destroy(gameObject);
    }
}
