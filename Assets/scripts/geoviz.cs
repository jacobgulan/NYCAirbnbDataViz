using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class geoviz : MonoBehaviour
{
    // Variables to store data, xy locations and cluster IDs
    private List<float> x_axis = new List<float>();
    private List<float> y_axis = new List<float>();
    private List<float> z_axis = new List<float>();
    private List<string> clusterID = new List<string>();
    private int current = 0;
    private int wait = 60;
    public Camera MainCamera;
    public Camera MapCamera;
    public Camera ScatterplotCamera;

    

    // Use this for initialization
    void Start()
    {
        readData("Assets/Data/geodata.csv");
        makePlot();
        MainCamera = Camera.main;
        MapCamera = GameObject.Find("MapCamera").GetComponent<Camera>();
        ScatterplotCamera = GameObject.Find("ScatterplotCamera").GetComponent<Camera>();

        MainCamera.enabled = true;
        MapCamera.enabled = false;
        ScatterplotCamera.enabled = false;
    }
    // reading and parsing CSV file and adding data to appropriate data structures
    public void readData(string filename)
    {
        string[] reader = System.IO.File.ReadAllLines(filename);
        for (int i = 0; i < reader.Length; i++)
        {
            string[] line = reader[i].Split(',');
            x_axis.Add(float.Parse(line[1])); // longitude
            y_axis.Add(float.Parse(line[3])); // price
            z_axis.Add(float.Parse(line[0])); // latitude
            clusterID.Add(line[2]);           // room type
        }

    }
    // creating Unity built-in primitive(sphere) and using it as a dataPoint in scatter-plot
    public void makePlot()
    {
        float scale = 0.005f;
        for (int i = 0; i < x_axis.Count; i++)
        {
            var dataPt = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dataPt.transform.localPosition = new Vector3(x_axis[i], 0, z_axis[i]);
            dataPt.transform.localRotation = Quaternion.identity;
            dataPt.name = "GeoSphere";

            // Adjust scale
            
            if ((int)(y_axis[i]) < 100)
            {
                scale = 0.003f;
            } 
            else if ((int)(y_axis[i]) > 100 && (int)(y_axis[i]) < 200)
            {
                scale = 0.004f;
            } 
            else if ((int)(y_axis[i]) > 200 && (int)(y_axis[i]) < 300)
            {
                scale = 0.005f;
            } 
            else if ((int)(y_axis[i]) > 300 && (int)(y_axis[i]) < 400)
            {
                scale = 0.006f;
            }
            else if ((int)(y_axis[i]) > 400 && (int)(y_axis[i]) < 500)
            {
                scale = 0.007f;
            }
            else if ((int)(y_axis[i]) > 500)
            {
                scale = 0.008f;
            }

            dataPt.transform.localScale = new Vector3(scale, scale, scale);
            Material newMaterial = new Material(Shader.Find("VertexLit"));
            newMaterial.color = findColor(clusterID[i]);
            dataPt.GetComponent<Renderer>().material = newMaterial;
            dataPt.gameObject.SetActive(true);

            if (newMaterial.color == Color.green)
            {
                dataPt.name = "GeoSphereGreen";
            }
            else if (newMaterial.color == Color.blue)
            {
                dataPt.name = "GeoSphereBlue";
            }
            else
            {
                dataPt.name = "GeoSphereRed";
            }
        }
    }

    public static Color findColor(string color)
    {
        Color outt = Color.white;
        switch (color)
        {
            case "1":
                outt = Color.green;
                break;
            case "2":
                outt = Color.blue;
                break;
            case "3":
                outt = Color.red;
                break;
            case "4":
                outt = Color.yellow;
                break;
            case "5":
                outt = Color.cyan;
                break;
            case "6":
                outt = Color.magenta;
                break;
            case "7":
                outt = Color.gray;
                break;
        }
        return outt;
    }

    // Update is called once per frame
    void Update() 
    {
        wait += 1;
        if (Input.GetKey("space") && wait > 60)
        {
            wait = 0;
            if (current == 2)
            {
                current = 0;
                MainCamera.enabled = true;
                MapCamera.enabled = false;
                ScatterplotCamera.enabled = false;
            }
            else if (current == 0)
            {
                current = 1;
                MainCamera.enabled = false;
                MapCamera.enabled = true;
                ScatterplotCamera.enabled = false;
            }
            else
            {
                current = 2;
                MainCamera.enabled = false;
                MapCamera.enabled = false;
                ScatterplotCamera.enabled = true;

            }

        }

        if (MapCamera.enabled) {
            if (Input.GetKey("z"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

            }


            if (Input.GetKey("x"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

            }

            if (Input.GetKey("c"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

            }

            if (Input.GetKey("r"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("GeoSphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

            }
        }

    }

    GameObject[] FindGameObjectsWithName(string name)
    {
        int a = GameObject.FindObjectsOfType<GameObject>().Length;
        GameObject[] arr = new GameObject[a];
        int FluentNumber = 0;
        for (int i = 0; i < a; i++)
        {
            if (GameObject.FindObjectsOfType<GameObject>()[i].name == name)
            {
                arr[FluentNumber] = GameObject.FindObjectsOfType<GameObject>()[i];
                FluentNumber++;
            }
        }
        Array.Resize(ref arr, FluentNumber);
        return arr;
    }
}
