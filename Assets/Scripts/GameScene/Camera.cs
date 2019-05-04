using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform target;
    public float offsetY;
    public float smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
        
    }
    private void LateUpdate()
    {
        if(target != null)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y + offsetY, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        }
    }


}
