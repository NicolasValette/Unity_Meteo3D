using Meteo3D.Request;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            _text.text= string.Empty;
        }

        private void OnEnable()
        {
            WebRequest.OnWeatherFound += DisplayWeather;
        }
        private void OnDisable()
        {
            WebRequest.OnWeatherFound -= DisplayWeather;
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayWeather(WebRequest.RootWeather weather)
        {
            string weatherInfo = $"Today : {weather.current_weather.ToString()}\nTemperature : {weather.current_weather.temperature}";
            _text.text = weatherInfo;
        }
    }
}