using Meteo3d.Meteo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Meteo3D.Request
{
    public class WebRequest : MonoBehaviour
    {

        #region CITY INFO JSON
        [Serializable]
        public class CityInfo
        {
            public List<CityResults> results;
            public float generationtime_ms;
        }
        [Serializable]
        public class CityResults
        {
            public int id;
            public string name;
            public float latitude;
            public float longitude;
            public float elevation;
            public string feature_code;
            public string country_code;
            public int admin1_id;
            public int admin2_id;
            public int admin3_id;
            public int admin4_id;
            public string timezone;
            public int population;
            public string[] postcodes;
            public int country_id;
            public string country;
            public string admin1;
            public string admin2;
            public string admin3;
            public string admin4;
        }
        #endregion
        #region WEATHER INFO JSON
        [Serializable]
        public class CurrentWeather
        {
            public double temperature;
            public double windspeed;
            public double winddirection;
            public int weathercode;
            public string time;
        }

        [Serializable]
        public class Daily
        {
            public List<string> time;
            public List<int> weathercode;
        }

        [Serializable]
        public class DailyUnits
        {
            public string time;
            public string weathercode;
        }

        [Serializable]
        public class RootWeather
        {
            public double latitude;
            public double longitude;
            public double generationtime_ms;
            public int utc_offset_seconds;
            public string timezone;
            public string timezone_abbreviation;
            public double elevation;
            public CurrentWeather current_weather;
            public DailyUnits daily_units;
            public Daily daily;
        }
        #endregion
        private CityInfo city;
        
        UnityWebRequest request;

        public static event Action<CityInfo> OnCityFound;
        public static event Action<RootWeather> OnWeatherFound;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetRequest("https://geocoding-api.open-meteo.com/v1/search?name=Marseille&language=en&count=1&format=json"));
        }

        private void OnEnable()
        {
            TownReader.OnTownSubmitted += GetTown;
        }
        private void OnDisable()
        {
            TownReader.OnTownSubmitted -= GetTown;
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void GetTown(string city)
        {
            
            StartCoroutine(GetRequestCity(city));
            
        }

        public void GetWeather (float lat, float longi)
        {

        }
        public IEnumerator GetRequestCity(string city)
        {
            string cityURI = $"https://geocoding-api.open-meteo.com/v1/search?name={city}&language=en&count=1&format=json";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(cityURI))
            {

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                byte[] result = webRequest.downloadHandler.data;
                string cityJSON = System.Text.Encoding.Default.GetString(result);
                //  string cityJSON = webRequest.downloadHandler.text;
                CityInfo cityRes = JsonUtility.FromJson<CityInfo>(cityJSON);
                Debug.Log("City = " + cityRes.results[0].name + "//Latitude : " + cityRes.results[0].latitude + " / Longitude : " + cityRes.results[0].longitude + "");
                OnCityFound?.Invoke(cityRes);
               
            }
        }
        public IEnumerator GetRequestWeather(float lat, float longi)
        {
            string weatherURI = $"https://api.open-meteo.com/v1/forecast?latitude={lat.ToString(CultureInfo.InvariantCulture)}&longitude={longi.ToString(CultureInfo.InvariantCulture)}&" +
                     $"daily=weathercode&current_weather=true&timezone=Europe%2FLondon";
            using (UnityWebRequest webRequestWeather = UnityWebRequest.Get(weatherURI))
            {
                yield return webRequestWeather.SendWebRequest();


                byte[] resultWeather = webRequestWeather.downloadHandler.data;
                string weatherJSON = System.Text.Encoding.Default.GetString(resultWeather);
                //string cityJSON = webRequest.downloadHandler.text;
                RootWeather weather = JsonUtility.FromJson<RootWeather>(weatherJSON);
                Debug.Log(weather.current_weather.weathercode);
                Debug.Log(weather.current_weather.temperature);
                OnWeatherFound?.Invoke(weather);
            }
        }
        public IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();


                byte[] result = webRequest.downloadHandler.data;
                string cityJSON = System.Text.Encoding.Default.GetString(result);
                //  string cityJSON = webRequest.downloadHandler.text;
                city = JsonUtility.FromJson<CityInfo>(cityJSON);

                string weatherURI = $"https://api.open-meteo.com/v1/forecast?latitude={city.results[0].latitude.ToString(CultureInfo.InvariantCulture)}&longitude={city.results[0].longitude.ToString(CultureInfo.InvariantCulture)}&" +
                       $"daily=weathercode&current_weather=true&timezone=Europe%2FLondon";
                using (UnityWebRequest webRequestWeather = UnityWebRequest.Get(weatherURI))
                {
                    yield return webRequestWeather.SendWebRequest();


                    byte[] resultWeather = webRequestWeather.downloadHandler.data;
                    string weatherJSON = System.Text.Encoding.Default.GetString(resultWeather);
                    //  string cityJSON = webRequest.downloadHandler.text;
                    RootWeather weather = JsonUtility.FromJson<RootWeather>(weatherJSON);
                    Debug.Log(weather.current_weather.weathercode);
                    Debug.Log(weather.current_weather.temperature);
                }
            }
        }
    }
}