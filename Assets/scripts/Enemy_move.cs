using UnityEngine;

public class Enemy_move : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public HomeTowerHealth homeTower;
    public int damageToHome = 1;

    private int currentWaypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (currentWaypointIndex >= waypoints.Length)
            return;

        Transform target = waypoints[currentWaypointIndex];

        // Move toward waypoint
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Rotate to face movement direction (optional)
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        // Check if reached waypoint
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.2f)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachHome();
        }

        void ReachHome()
        {
            if (homeTower != null)
            {
                homeTower.TakeDamage(damageToHome);
            }
            Destroy(gameObject);
        }
    }
}
