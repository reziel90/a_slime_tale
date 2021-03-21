using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]private  Transform _player;
    [Range(1,10)] [SerializeField] private float smoothFactor = 1 ; 
    [SerializeField] private float _cameraDistance = 10f;

    public void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(_player.transform.position.x,_player.transform.position.y,-_cameraDistance);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
