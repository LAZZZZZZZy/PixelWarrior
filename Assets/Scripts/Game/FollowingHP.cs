using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingHP : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, 0.5f, 0);

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
        }
    }
}
