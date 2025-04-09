using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransition : MonoBehaviour
{
    public Vector2 cameraChange;    // how much to move camera
    public Vector3 playerChange;    // how much to move player
    private CameraMovement cam;
    public bool needText;
    public string locationName;
    public GameObject text;
    public Text placeText;


    void Start() {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) // "if Player is inside the trigger zone..."
        {
            cam.minPosition += cameraChange;            // update cam's min bounds
            cam.maxPosition += cameraChange;            // update cam's max bounds
            other.transform.position += playerChange;   // move the character a bit; the camera should then follow the player

            // "if location needs text..."
            if (needText) {
                StartCoroutine(DisplayLocationName());
            }
        }
    }

    private IEnumerator DisplayLocationName() 
    {
        text.SetActive(true);
        placeText.text = locationName;
        yield return new WaitForSeconds(3f);
        text.SetActive(false);
    }
}
