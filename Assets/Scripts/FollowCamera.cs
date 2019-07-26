using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float followSpeed;
    public Transform followLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //make speed slower, not so fast (Number between 0 and 1)
        float t = followSpeed * Time.deltaTime;
        //Move to this position by whatever our speed is (0.16th of the way or something)
        transform.position = Vector3.Lerp(transform.position, followLocation.position, t);
    }
}
