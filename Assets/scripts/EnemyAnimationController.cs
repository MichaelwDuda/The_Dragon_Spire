using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator animator;
    public Transform visualRoot;
    public string speedParameter = "Speed";
    public string hitTrigger = "Hit";
    public string attackTrigger = "Attack";
    public string deathTrigger = "Die";
    public bool useRunWhenFast = true;
    public float runSpeedThreshold = 4f;
    public bool useProceduralFallback = true;
    public float bobHeight = 0.12f;
    public float bobSpeed = 8f;
    public float tiltAmount = 8f;
    public float hitPulseScale = 1.15f;
    public float hitPulseDuration = 0.15f;
    public float proceduralDeathDuration = 0.75f;

    private Enemy_move movement;
    private Vector3 visualStartLocalPosition;
    private Vector3 visualStartLocalScale;
    private Quaternion visualStartLocalRotation;
    private float hitTimer;
    private float deathTimer;
    private bool isProceduralDeathPlaying;
    public bool HasPlayableAnimator => CanAnimate();

    void Awake()
    {
        movement = GetComponent<Enemy_move>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (visualRoot == null && transform.childCount > 0)
        {
            visualRoot = transform.GetChild(0);
        }

        if (visualRoot != null)
        {
            visualStartLocalPosition = visualRoot.localPosition;
            visualStartLocalScale = visualRoot.localScale;
            visualStartLocalRotation = visualRoot.localRotation;
        }
    }

    void Update()
    {
        if (CanAnimate() && movement != null)
        {
            float normalizedSpeed = movement.CurrentSpeed;

            if (useRunWhenFast)
            {
                normalizedSpeed = movement.CurrentSpeed >= runSpeedThreshold ? 2f : movement.IsMoving ? 1f : 0f;
            }

            animator.SetFloat(speedParameter, normalizedSpeed);
            return;
        }

        if (!useProceduralFallback || visualRoot == null)
            return;

        UpdateProceduralAnimation();
    }

    void UpdateProceduralAnimation()
    {
        if (isProceduralDeathPlaying)
        {
            deathTimer += Time.deltaTime;
            float t = Mathf.Clamp01(deathTimer / proceduralDeathDuration);
            visualRoot.localPosition = visualStartLocalPosition + Vector3.down * t;
            visualRoot.localScale = Vector3.Lerp(visualStartLocalScale, Vector3.zero, t);
            visualRoot.localRotation = visualStartLocalRotation * Quaternion.Euler(0f, 180f * t, 35f * t);
            return;
        }

        bool isMoving = movement != null && movement.IsMoving;
        float bob = isMoving ? Mathf.Sin(Time.time * bobSpeed) * bobHeight : 0f;
        float tilt = isMoving ? Mathf.Sin(Time.time * bobSpeed) * tiltAmount : 0f;

        hitTimer = Mathf.Max(0f, hitTimer - Time.deltaTime);
        float hitT = hitPulseDuration > 0f ? hitTimer / hitPulseDuration : 0f;
        float hitScale = Mathf.Lerp(1f, hitPulseScale, hitT);

        visualRoot.localPosition = visualStartLocalPosition + Vector3.up * bob;
        visualRoot.localRotation = visualStartLocalRotation * Quaternion.Euler(0f, 0f, tilt);
        visualRoot.localScale = visualStartLocalScale * hitScale;
    }

    public bool PlayHit()
    {
        if (CanAnimate())
        {
            animator.SetTrigger(hitTrigger);
            return true;
        }

        if (useProceduralFallback && visualRoot != null)
        {
            hitTimer = hitPulseDuration;
            return true;
        }

        return false;
    }

    public bool PlayAttack()
    {
        if (CanAnimate())
        {
            animator.SetTrigger(attackTrigger);
            return true;
        }

        return false;
    }

    public bool PlayDeath()
    {
        if (CanAnimate())
        {
            animator.SetTrigger(deathTrigger);
            return true;
        }

        if (useProceduralFallback && visualRoot != null)
        {
            isProceduralDeathPlaying = true;
            deathTimer = 0f;
            return true;
        }

        return false;
    }

    bool CanAnimate()
    {
        return animator != null && animator.runtimeAnimatorController != null;
    }
}
