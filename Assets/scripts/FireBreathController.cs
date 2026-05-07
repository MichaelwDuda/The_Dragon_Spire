using UnityEngine;
using UnityEngine.InputSystem;

public class FireBreathController : MonoBehaviour
{
    public ParticleSystem fireBreath;
    public InputActionReference attackAction;
    public GameObject hitBox;

    private void OnEnable()
    {
        attackAction.action.Enable();
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
    }

    private void Update()
    {
        if (!enabled) return;

        if (attackAction.action.IsPressed())
        {
            if (!fireBreath.isPlaying)
            {
                fireBreath.Play();
            }

            hitBox.SetActive(true);
        }
        else
        {
            if (fireBreath.isPlaying)
            {
                fireBreath.Stop();
            }

            hitBox.SetActive(false);
        }
    }
}