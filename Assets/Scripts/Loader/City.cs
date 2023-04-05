using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class City
{
    public string country;
    public string name;
    public string lat;
    public string lng;
}
[Serializable]
public class Cities
{
    public List<City> City;
}


