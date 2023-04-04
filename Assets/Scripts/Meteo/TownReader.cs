using Meteo3D.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Meteo3D.Request.WebRequest;
using static UnityEngine.Rendering.DebugUI;

namespace Meteo3d.Meteo
{
    public class TownReader : MonoBehaviour
    {
        [SerializeField]
        private InputField _inputField;

        public static event Action<string> OnTownSubmitted;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            WebRequest.OnCityFound += FillTown;
        }
        private void OnDisable()
        {
            WebRequest.OnCityFound -= FillTown;
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void ReadTown(string value)
        {
            OnTownSubmitted?.Invoke(value);
        }
        public void FillTown(CityInfo city)
        {
            Debug.Log("Result");
            if (city.results.Count > 0)
            {
                _inputField.text = city.results[0].name;
                Debug.Log("Result : " + city.results[0].name);
            }
        }

        public void AutoComplete (string value)
        {
            Debug.Log("Auto complete : " + value);
            OnTownSubmitted?.Invoke(value);
        }
    }

   

}