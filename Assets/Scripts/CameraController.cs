using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 prevPos;
    float angle = -30 * Mathf.Deg2Rad;
    float angleVelocity = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prevPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPos = Input.mousePosition;
            angleVelocity = (prevPos.x - currentPos.x) / 10000;            
        }
        else
        {
            angleVelocity /= 1.1f;
        }
        angle += angleVelocity;


        Vector3 camPos = new Vector3(6 * Mathf.Cos(angle), 7, 6 * Mathf.Sin(angle));
        transform.position = camPos;

        transform.eulerAngles = new Vector3(45, -90 - angle * 360 / (2 * Mathf.PI), 0);
        //transform.eulerAngles = new Vector3(45, -90 - angle * Mathf.Rad2Deg, 0);ŒÊ“x–@
    }
}
