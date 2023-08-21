using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public Transform[] currentWaypoints;
    [HideInInspector] public int currentWaypointIndex;
    [HideInInspector] public int currentMapPointIndex;
    [HideInInspector] public WayPoint currentMapPointsAvaliable;

    [SerializeField] private float moveSpeed;

    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        if (currentWaypoints.Length > 0) transform.position = currentWaypoints[currentWaypointIndex].transform.position;
        
        currentMapPointIndex = 1;

        currentMapPointsAvaliable = GameObject.Find("Map_Point_1").GetComponent<MapPoint>().mapPointsAvaliable;

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWaypoints.Length > 0)
        {
            _anim.SetBool("isMoving", true);

            if (currentWaypointIndex < currentWaypoints.Length)
            {
                transform.position = Vector2.MoveTowards(transform.position, currentWaypoints[currentWaypointIndex].transform.position, moveSpeed * Time.deltaTime);
                if (transform.position == currentWaypoints[currentWaypointIndex].transform.position) currentWaypointIndex += 1;
            }
            if (currentWaypointIndex == currentWaypoints.Length)
            {
                currentWaypointIndex = 0;
                currentWaypoints = new Transform[0];
            }
        }
        else
        {
            _anim.SetBool("isMoving", false);
        }
    }
}
