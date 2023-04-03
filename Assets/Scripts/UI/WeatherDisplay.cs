using Meteo3D.Request;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Meteo3D.UI
{
    public class WeatherDisplay : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text _text;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayWeather(WebRequest.RootWeather weather)
        {
        //    weather.current_weather
        }
    }
}