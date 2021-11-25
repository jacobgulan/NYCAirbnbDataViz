using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotate : MonoBehaviour
{

    public GameObject target;
    private float speedMod = 5.0f;
    private Vector3 point;
    public Camera ScatterplotCamera;
    Ray ray;
    RaycastHit hit;
    


    // Start is called before the first frame update
    void Start()
    {
        point = target.transform.position;
        ScatterplotCamera = GameObject.Find("ScatterplotCamera").GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if (ScatterplotCamera.enabled)
        {
            ray = ScatterplotCamera.ScreenPointToRay(Input.mousePosition);

            // Rotate camera if user presses left or right
            if (Input.GetKey("a") || Input.GetKey("left"))
            {
                transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * speedMod);
            }

            if (Input.GetKey("d") || Input.GetKey("right"))
            {
                transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), -20 * Time.deltaTime * speedMod);
            }
        }


    }
}
