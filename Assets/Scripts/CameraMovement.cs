using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;
    public GameObject target;
    public float smooth;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    public Camera mainCamera, fullMapCamera;

    private bool fullMapActive;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        target = GameObject.FindWithTag("player");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!fullMapActive)
            {
                ActivateFullMap();
            }
            else
            {
                DeactivateFullMap();
            }
        }
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.transform.position)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth);
        }
    }
    public void ActivateFullMap()
    {
        if (!LevelManager.instance.isPause)
        {
            fullMapActive = true;
            fullMapCamera.enabled = true;
            mainCamera.enabled = false;
            Player.instance.canMove = false;
            Time.timeScale = 0f;
            UIController.instance.mapDisplay.SetActive(false);
        }
    }

    public void DeactivateFullMap()
    {
        if (!LevelManager.instance.isPause)
        {
            fullMapActive = false;
            fullMapCamera.enabled = false;
            mainCamera.enabled = true;
            Player.instance.canMove = true;
            Time.timeScale = 1f;
            UIController.instance.mapDisplay.SetActive(true);
        }
    }
}
