using UnityEngine;

public class SpaceshipController_2 : MonoBehaviour
{
    public float speed;

    void Update()
    {
        Vector3 prevPos = transform.position;
        //transform.position = prevPos - new Vector3(0, speed * Time.deltaTime, 0);

        transform.Rotate(-0.5f * speed * Time.deltaTime, speed * Time.deltaTime, -0.5f * speed * Time.deltaTime);
    }
}
