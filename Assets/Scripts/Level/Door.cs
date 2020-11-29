using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    GameObject closedSprite;
    [SerializeField]
    GameObject openSprite;
    Collider2D collider;

    bool isOpened = false;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void OpenDoor( bool open )
    {
        isOpened = open;
        closedSprite.SetActive(!open);
        openSprite.SetActive(open);
        collider.enabled = !open;
    }

    public void ToggleDoor()
    {
        OpenDoor(!isOpened);
    }
}
