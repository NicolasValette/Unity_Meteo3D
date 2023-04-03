using Meteo3D.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteo3d.Meteo
{
    public class TownReader : MonoBehaviour
    {

        public static event Action<string> OnTownSubmitted;

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        {
            //WebRequest.OnCityFound +
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ReadTown(string value)
        {
            OnTownSubmitted?.Invoke(value);
        }
    }

   

}