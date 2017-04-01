using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinController : MonoBehaviour {

    float baseAngle = 0.0f;

    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    void OnMouseDown()
    {
        if (gameManager.GetComponent<TouchController>().isOnActivity)
        {
            var dir = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - dir;
            baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        }
    }

    void OnMouseDrag()
    {
        if (gameManager.GetComponent<TouchController>().isOnActivity)
        {
            var dir = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - dir;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - baseAngle;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
