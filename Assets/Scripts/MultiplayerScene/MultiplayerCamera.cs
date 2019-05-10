using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerCamera : MonoBehaviour
{
    public float offsetZ;
    public float smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    private Transform firstPlayer;
    private Transform secondPlayer;

    private float posX;
    private float posZ;
    private float minZ;


    // Start is called before the first frame update
    void Start()
    {
        if (firstPlayer == null) firstPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        if (secondPlayer == null) secondPlayer = GameObject.FindGameObjectWithTag("Enemy").transform;
        minZ = transform.position.z;
        posZ = transform.position.z;
        posX = transform.position.x;

        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(new Vector3(firstPlayer.position.x,0.0f,0.0f), new Vector3(secondPlayer.position.x,0.0f,0.0f));
        float midDistance = distance / 2.0f;
        if(firstPlayer.position.x < secondPlayer.position.x)
        {
            posX = firstPlayer.position.x + midDistance;
        }else if (secondPlayer.position.x < firstPlayer.position.x)
        {
            posX = secondPlayer.position.x + midDistance;
        }


        posZ = firstPlayer.position.z + offsetZ - midDistance + 2.0f;
        if(posZ > minZ)
        {
            posZ = minZ;
        }

        
    }
    private void LateUpdate()
    {

        Vector3 targetPosition = new Vector3(posX, transform.position.y, posZ);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);
        
    }


}
