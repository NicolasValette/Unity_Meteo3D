using Meteo3D.Request;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteo3D.Earth
{
    public class RotateEarth : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {

        }
        private void OnEnable()
        {
            WebRequest.OnCityFound += RotatePlanet;
        }
        private void OnDisable()
        {
            WebRequest.OnCityFound -= RotatePlanet;
        }
        // Update is called once per frame
        void Update()
        {
           // transform.Rotate(0f, 0.2f, 0f);
        }
        public void RotatePlanet(WebRequest.CityInfo cityInfo)
        {
            Vector3 euleurs = new Vector3(cityInfo.results[0].latitude * -1f, cityInfo.results[0].longitude, 0f);
            transform.eulerAngles= euleurs;
        }


    }
}
