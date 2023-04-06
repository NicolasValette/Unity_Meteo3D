using Meteo3D.Earth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoordOnClick : MonoBehaviour
{

    public static event Action<float, float> OnClick;
    public static event Action<Vector3> OnClickCoord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        // Raycast pour détecter le point cliqué
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GetCoordOnClick rotEarth = hit.collider.gameObject.GetComponent<GetCoordOnClick>();
            if (rotEarth != null)
            {
                OnClickCoord?.Invoke(hit.point);
                float rayon = hit.point.magnitude;
                Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);

                float latitude = Mathf.Asin(localHitPoint.y / rayon) * Mathf.Rad2Deg;
                float longitude = Mathf.Atan2(localHitPoint.x, -localHitPoint.z) * Mathf.Rad2Deg;

       
                Debug.Log("Latitude: " + latitude);
                Debug.Log("Longitude: " + longitude);
                OnClick?.Invoke(latitude, longitude);
            }
        }
    }
}
