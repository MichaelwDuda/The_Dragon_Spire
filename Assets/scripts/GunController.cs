using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletSpeed = 30;
    public float lifeTime = 3;
    public InputActionReference attackAction;


    private void OnEnable()
    {
        attackAction.action.Enable();
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
    }


    // Update is called once per frame
    void Update()
    {
        if (!enabled) return;

        if (attackAction.action.WasPressedThisFrame())
        {
            Fire();
        }
    }

    private void Fire()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = bulletSpawn.forward * bulletSpeed;
            //Debug.Log(bulletSpawn.forward);
        }

        Destroy(bullet, lifeTime);
    }

    private IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
