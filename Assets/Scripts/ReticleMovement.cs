using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleMovement : MonoBehaviour
{
    [SerializeField]
    float reticleSpeed = 5f;
    [SerializeField]
    float zOffset = -5f;

    Vector2 movement;
    Vector2 screenBounds;

    Vector3 screenSize;
    Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        //GetScreenSize();
        HideMouse();
    }

    // Update is called once per frame
    void Update()
    {
        TrackMousePosition();
        //GetScreenSize();
        float horizontalMovement = Input.GetAxisRaw("Right Stick Horizontal");
        float verticalMovement = Input.GetAxisRaw("Right Stick Vertical");

        Vector2 moveInput = new Vector2(horizontalMovement, verticalMovement);
        movement = moveInput.normalized * reticleSpeed * Time.deltaTime;

        transform.Translate(movement.x, movement.y, 0);
    }

    void LateUpdate() {

        ClampTarget();

        //Vector3 viewPosition = transform.position;

        //viewPosition.x = Mathf.Clamp(viewPosition.x, -screenBounds.x, screenBounds.x);
        //viewPosition.y = Mathf.Clamp(viewPosition.y, -screenBounds.y, screenBounds.y);
        //viewPosition.x = Mathf.Clamp01(screenBounds.x);
        //viewPosition.y = Mathf.Clamp01(screenBounds.y);

        //transform.position = viewPosition;

    }

    void GetScreenSize() {
        screenSize = new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z);
        //screenBounds = Camera.main.ScreenToWorldPoint(screenSize);
        Vector3 currentPosition = Camera.main.WorldToViewportPoint(transform.position);
        screenBounds = Camera.main.ViewportToWorldPoint(currentPosition);

        Debug.Log(screenBounds.x);
    }

    void ClampTarget() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void TrackMousePosition()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = zOffset;
        this.transform.position = newPosition;
    }

    void HideMouse()
    {
        Cursor.visible = false;
    }

}
