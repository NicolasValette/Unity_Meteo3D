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
                //BRICOLE
                float rayon = hit.point.magnitude;
                //Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);
                //Conversion des coordonnées cartésiennes en coordonnées sphériques

                float latitude = Mathf.Asin(hit.point.y / rayon) * Mathf.Rad2Deg;
                float longitude = Mathf.Atan2(hit.point.z, hit.point.x) * Mathf.Rad2Deg;

                //Des ANGLES
                //Vector3 vectllat = new Vector3(0f, hit.point.y, hit.point.z);
                //Vector3 vectlong = new Vector3(hit.point.x, 0f, hit.point.z);
                //float latitude = Vector3.Angle(vectllat, transform.up);
                //float longitude = Vector3.Angle(vectlong, transform.forward);
                // Affichage des coordonnées

                //AZIZE
                //Vector3 hitPoint = hit.point;
                //Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

                //Vector3 normalizedHitPoint = localHitPoint.normalized;

                //float latitude = Mathf.Asin(normalizedHitPoint.y) * Mathf.Rad2Deg;
                //float longitude = Mathf.Atan2(normalizedHitPoint.z, normalizedHitPoint.x) * Mathf.Rad2Deg;

                Debug.Log("Latitude: " + latitude);
                Debug.Log("Longitude: " + longitude);
            }
        }
    }
}
