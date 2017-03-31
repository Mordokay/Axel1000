using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public bool isMoving;
    public bool isOnActivity;

    public float CameraZoomIn;
    public float CameraZoomOut;

    float t;
    public float moveSpeed = 1.0f;
    Vector3 startPos;

    void Start()
    {
        isMoving = false;
        isOnActivity = false;
    }

	void Update () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase.Equals(TouchPhase.Began) && !isMoving) {
            if (!isOnActivity)
            {
                Vector2 test = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D hit = Physics2D.Raycast(test, Input.GetTouch(0).position, Mathf.Infinity, 1 << LayerMask.NameToLayer("Activity"));
                if (hit.collider)
                {
                    Debug.Log(hit.collider.gameObject.name);

                    StartCoroutine(moveCameraToActivity(new Vector3(hit.collider.gameObject.transform.position.x,
                        hit.collider.gameObject.transform.position.y, -1.0f)));
                }
            }
            else {
                StartCoroutine(moveCameraFromActivity(new Vector3(0.0f, 0.0f, -10.0f)));
            }
        }
    }

    public IEnumerator moveCameraFromActivity(Vector3 endPos)
    {
        isMoving = true;
        startPos = Camera.main.transform.position;
        t = 0;

        endPos = new Vector3(endPos.x, endPos.y, endPos.z);

        while (t <= 1f)
        {
            t += Time.deltaTime * moveSpeed;
            Camera.main.transform.position = Vector3.Lerp(startPos, endPos, t);
            Camera.main.orthographicSize = Mathf.Lerp(CameraZoomIn, CameraZoomOut, t);
            yield return null;
        }

        isMoving = false;
        isOnActivity = false;
        yield return 0;
    }

    public IEnumerator moveCameraToActivity(Vector3 endPos)
    {
        isMoving = true;
        startPos = Camera.main.transform.position;
        t = 0;

        endPos = new Vector3(endPos.x, endPos.y, endPos.z);

        while (t <= 1f)
        {
            t += Time.deltaTime * moveSpeed;
            Camera.main.transform.position = Vector3.Lerp(startPos, endPos, t);
            Camera.main.orthographicSize = Mathf.Lerp(CameraZoomOut, CameraZoomIn, t);
            yield return null;
        }

        isMoving = false;
        isOnActivity = true;
        yield return 0;
    }
}
