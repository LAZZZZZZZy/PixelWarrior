using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    public BoxCollider2D boundbox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private float halfHeight;
    private float halfWidth;
    private Camera theCamera;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 3;
        DontDestroyOnLoad(this);

        minBounds = boundbox.bounds.min;
        maxBounds = boundbox.bounds.max;
        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    public void setBounds(BoxCollider2D box)
    {
        boundbox = box;
        minBounds = boundbox.bounds.min;
        maxBounds = boundbox.bounds.max;
    }
}
