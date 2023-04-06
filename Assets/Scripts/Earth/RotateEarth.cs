using Meteo3D.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Meteo3D.Earth
{
    public class RotateEarth : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cameraOffset;
        [SerializeField]
        private GameObject _pin;
        [SerializeField]
        private Transform _earthTransform;
        private Vector3 _mousePos;

        public static event Action<Vector3> OnRotate;
        // Start is called before the first frame update
        void Start()
        {

        }
        private void OnEnable()
        {
            WebRequest.OnCityFound += RotatePlanet;
            WebRequest.OnWeatherFound += RotatePlanet;
        }
        private void OnDisable()
        {
            WebRequest.OnCityFound -= RotatePlanet;
            WebRequest.OnWeatherFound -= RotatePlanet;
        }
        // Update is called once per frame
        void Update()
        {
            var ms = Mouse.current;
         
            if (ms.rightButton.isPressed)
            {
                Vector2 delta = ms.delta.ReadValue();
                
                transform.rotation *= Quaternion.Euler(0f, -delta.x, 0f);
                _cameraOffset.transform.rotation *= Quaternion.Euler(0f, 0f, -delta.y);
            }
            // transform.Rotate(0f, 0.2f, 0f);
        }
        public void RotatePlanet(WebRequest.CityInfo cityInfo)
        {
            RotatePlanet(cityInfo.results[0].latitude, cityInfo.results[0].longitude);
        }
        public void RotatePlanet(WebRequest.RootWeather weather)
        {
            RotatePlanet((float)weather.latitude, (float)weather.longitude);
        }
        public void RotatePlanet(float latitude, float longitude)
        {
            Debug.Log("Rotate");
            Vector3 euleursX = new Vector3(latitude * -1f, 0f, 0f);
            Vector3 euleursY = new Vector3(0f, longitude, 0f);
            transform.rotation = Quaternion.identity;
            transform.rotation *= Quaternion.AngleAxis(latitude * -1f, Camera.main.transform.right);
            transform.rotation *= Quaternion.AngleAxis(longitude, Camera.main.transform.up);

            //transform.rotation *= Quaternion.AngleAxis(latitude * -1f, _earthTransform.right);
            //transform.rotation *= Quaternion.AngleAxis(longitude, _earthTransform.up);

            Vector3 dir = transform.position - Camera.main.transform.position;
            Camera.main.transform.parent.rotation = Quaternion.identity;
            if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit))
            {
                OnRotate?.Invoke(hit.point);
            }
            //float radius = GetComponentInChildren<SphereCollider>().radius;
            //Vector3 vect = new Vector3(
            //    x : Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Cos(longitude * Mathf.Deg2Rad),
            //    y : radius * Mathf.Sin(latitude * Mathf.Deg2Rad),
            //    z : radius * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Sin(longitude * Mathf.Deg2Rad));
            

            //OnRotate?.Invoke(vect);
            
        }

        public void turn()
        {
            Input.GetMouseButtonDown(1);
        }

        

    }
}
