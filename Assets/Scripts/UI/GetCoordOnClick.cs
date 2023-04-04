using Meteo3D.Earth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoordOnClick : MonoBehaviour
{
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
        Debug.Log("click");
        // Raycast pour détecter le point cliqué
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GetCoordOnClick rotEarth = hit.collider.gameObject.GetComponent<GetCoordOnClick>();
            if (rotEarth != null)
            {
                //Vector3 vect = transform.InverseTransformPoint(hit.point);

                // Conversion des coordonnées cartésiennes en coordonnées sphériques

                float latitude = Mathf.Acos(hit.point.y) * Mathf.Rad2Deg;
                float longitude = Mathf.Atan2(hit.point.z, hit.point.x) * Mathf.Rad2Deg;

                // Affichage des coordonnées

                Vector3 hitPoint = hit.point;
                Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

                Vector3 normalizedHitPoint = localHitPoint.normalized;

                //float latitude = Mathf.Asin(normalizedHitPoint.y) * Mathf.Rad2Deg;
                //float longitude = Mathf.Atan2(normalizedHitPoint.z, normalizedHitPoint.x) * Mathf.Rad2Deg;

                Debug.Log("Latitude: " + latitude);
                Debug.Log("Longitude: " + longitude);
            }
        }
    }
}
