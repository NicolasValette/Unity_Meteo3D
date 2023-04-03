using Meteo3D.WeatherInfo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Meteo3D.Request
{
    public class WebRequest : MonoBehaviour
    {
        UnityWebRequest request;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GetRequest("https://geocoding-api.open-meteo.com/v1/search?name=Marseille&language=en&count=10&format=json"));
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string cityJSON = webRequest.downloadHandler.text;
                CityInfo city = JsonUtility.FromJson<CityInfo>(cityJSON);
                Debug.Log(city.results[0].name);
            }

        }
    }
    }