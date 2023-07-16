using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject _winScreen;
    private PlayerController _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("DD0");
        if(other.gameObject.tag == "Player")
        {
            _winScreen.SetActive(true);
            _player.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
