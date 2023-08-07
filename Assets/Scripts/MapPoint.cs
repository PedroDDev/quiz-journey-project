using System.Linq;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    [SerializeField] private int mapPointIndex;
    [SerializeField] public WayPoint mapPointsAvaliable;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite lockedSprite;

    public bool isLocked;

    private PlayerMovement _player;
    private CircleCollider2D _col;
    private SpriteRenderer _spr;
    private bool _isMouseOn;

    // Start is called before the first frame update
    void Start()
    {
        _isMouseOn = false;
        _player = GameObject.FindObjectOfType<PlayerMovement>();
        _col = GetComponent<CircleCollider2D>();
        _spr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMouseOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isLocked)
                {
                    foreach (var item in _player.currentMapPointsAvaliable.waypoints)
                    {
                        if (item.index == mapPointIndex)
                        {
                            _player.currentWaypoints = item.waypoint;
                            _player.currentMapPointIndex = mapPointIndex;
                            _player.currentMapPointsAvaliable = mapPointsAvaliable;
                        }
                    }
                }
                else
                {
                    Debug.Log("fase está travada.");
                }
            }
        }

        if (_player.currentMapPointIndex == mapPointIndex || _player.currentWaypoints.Length > 0 || !(_player.currentMapPointsAvaliable.waypoints.Any(x => x.index == mapPointIndex))) _col.enabled = false;
        else _col.enabled = true;

        if (isLocked) _spr.sprite = lockedSprite;
        else _spr.sprite = unlockedSprite;
    }

    void OnMouseEnter()
    {
        _isMouseOn = true;
    }

    void OnMouseExit()
    {
        _isMouseOn = false;
    }
}
