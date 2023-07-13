using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed;
    private Vector3 _moveDir;
    public Vector3 offset;
    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _moveDir = new Vector3(_player.position.x, _player.position.y, _player.position.z);
        _moveDir += offset;

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _moveDir, _speed * Time.deltaTime);
    }
}
