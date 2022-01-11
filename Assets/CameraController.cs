using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 _xCamClamp;
    [SerializeField] Vector2 _yCamClamp;

    [SerializeField]
    Transform _player;

    [Range(0f, 1f)] public float speed;

    void Awake()
    {
        if (!_player) _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        Reset();
    }

    void LateUpdate()
    {
        LerpToPlayer();
        ClampCameraPosition();
    }

    private void Reset()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        ClampCameraPosition();
    }

    void LerpToPlayer()
    {
        Vector3 targetPos = new Vector3(_player.position.x, _player.position.y, transform.position.z);
        Vector3 newPos = Vector3.Lerp(transform.position, targetPos, speed);
        transform.position = newPos;
    }

    void ClampCameraPosition()
    {
        Vector3 target = new Vector3(Mathf.Clamp(transform.position.x, _xCamClamp.x, _xCamClamp.y), Mathf.Clamp(transform.position.y, _yCamClamp.x, _yCamClamp.y), transform.position.z);
        transform.position = target;
    }


    public void SetClamp(Vector2 xCamClamp, Vector2 yCamClamp)
    {
        _xCamClamp = xCamClamp;
        _yCamClamp = yCamClamp;
    }
}
