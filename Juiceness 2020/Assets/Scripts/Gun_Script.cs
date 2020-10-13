using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Script : MonoBehaviour
{
    
    public GameObject target = null;
    public float speed;
    void FixedUpdate()
    {
        Vector3 vectorToTarget = target.transform.position - transform.position; //Gets target position
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg; //Gets only 2D position and angle towards target
        Quaternion q = Quaternion.AngleAxis(angle + -90f, Vector3.forward); //Points gun towards target in 2D space
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed); //How fast it can rotate
    }
}
