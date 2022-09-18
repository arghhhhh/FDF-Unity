using UnityEngine;
using Cinemachine;

public class CameraControl_5 : MonoBehaviour
{
    public Material flattenMat;

    public CinemachineVirtualCamera camUp;
    public CinemachineVirtualCamera camLeft;
    public CinemachineVirtualCamera camRight;

    float scrollPos;
    public float intensity = 5f;

    private string direction;

    private void Start()
    {
        direction = "up";
        flattenMat.SetFloat("_Orientation", 0f);
        camUp.Priority = 11;
        camLeft.Priority = 10;
        camRight.Priority = 10;
    }

    void Update()
    {
        scrollPos = Input.mouseScrollDelta.y;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = "up";
            flattenMat.SetFloat("_Orientation", 0f);
            camUp.Priority = 11;
            camLeft.Priority = 10;
            camRight.Priority = 10;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = "left";
            flattenMat.SetFloat("_Orientation", 1f);
            camUp.Priority = 10;
            camLeft.Priority = 11;
            camRight.Priority = 10;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = "right";
            flattenMat.SetFloat("_Orientation", 1f);
            camUp.Priority = 10;
            camLeft.Priority = 10;
            camRight.Priority = 11;
        }
        CameraScroll();
    }

    void CameraScroll()
    {
        if (direction == "up")
        {
            camUp.transform.position += new Vector3(0, scrollPos * intensity, 0);
        }
        else if (direction == "left")
        {
            camLeft.transform.position -= new Vector3(scrollPos * intensity, 0, 0);
        }
        else if (direction == "right")
        {
            camRight.transform.position += new Vector3(scrollPos * intensity, 0, 0);
        }
    }
}
