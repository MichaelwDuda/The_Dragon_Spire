using UnityEngine;

public class Enemy_move : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 3f;
    public HomeTowerHealth homeTower;
    public int damageToHome = 1;

    private int currentWaypointIndex = 0;
    private Vector3 lastPosition;
    private bool hasReachedHome;

    public float CurrentSpeed { get; private set; }
    public bool IsMoving => enabled && !hasReachedHome && CurrentSpeed > 0.05f;

    public void Configure(Transform[] pathWaypoints, HomeTowerHealth targetHomeTower)
    {
        waypoints = pathWaypoints;
        homeTower = targetHomeTower;
        currentWaypointIndex = 0;
        hasReachedHome = false;
        lastPosition = transform.position;
    }

    void Awake()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning($"{name} cannot move because no waypoints are assigned.", this);
            enabled = false;
            return;
        }

        if (currentWaypointIndex >= waypoints.Length)
            return;

        if (waypoints[currentWaypointIndex] == null)
        {
            Debug.LogWarning($"{name} found a missing waypoint and will skip it.", this);
            currentWaypointIndex++;
            return;
        }

        Transform target = waypoints[currentWaypointIndex];

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        CurrentSpeed = Time.deltaTime > 0f
            ? Vector3.Distance(transform.position, lastPosition) / Time.deltaTime
            : 0f;
        lastPosition = transform.position;

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        float distance = Vector3.Distance(transform.position, target.position);
        if (distance < 0.2f)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= waypoints.Length)
        {
            ReachHome();
        }
    }

    void ReachHome()
    {
        hasReachedHome = true;

        if (homeTower != null)
        {
            homeTower.TakeDamage(damageToHome);
        }
        else
        {
            Debug.LogWarning($"{name} reached the end but no HomeTowerHealth is assigned.", this);
        }

        Destroy(gameObject);
    }
}
