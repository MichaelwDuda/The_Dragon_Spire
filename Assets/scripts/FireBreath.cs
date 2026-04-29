using UnityEngine;
using UnityEngine.InputSystem;

public class FireBreathController : MonoBehaviour
{
    public ParticleSystem fireBreath;
    public InputActionReference attackAction;

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
        if (attackAction.action.IsPressed())
        {
            if (!fireBreath.isPlaying)
                fireBreath.Play();
        }
        else
        {
            if (fireBreath.isPlaying)
                fireBreath.Stop();
        }
    }
}