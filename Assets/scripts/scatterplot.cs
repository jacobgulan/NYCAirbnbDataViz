using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class scatterplot : MonoBehaviour
{
    // Variables to store data, xy locations and cluster IDs
    private List<float> x_axis = new List<float>();
    private List<float> y_axis = new List<float>();
    private List<float> z_axis = new List<float>();
    private List<string> clusterID = new List<string>();
    public Camera ScatterplotCamera;

    // Use this for initialization
    void Start()
    {
        readData("Assets/Data/normalized_vis_data.csv");
        makePlot();
        ScatterplotCamera = GameObject.Find("ScatterplotCamera").GetComponent<Camera>();
    }
    // reading and parsing CSV file and adding data to appropriate data structures
    public void readData(string filename)
    {
        string[] reader = System.IO.File.ReadAllLines(filename);
        for (int i = 0; i < reader.Length; i++)
        {
            string[] line = reader[i].Split(',');
            x_axis.Add(float.Parse(line[1])); // longitude
            y_axis.Add(float.Parse(line[2])); // price
            z_axis.Add(float.Parse(line[0])); // latitude
            clusterID.Add(line[3]);           // room type
        }

    }
    // creating Unity built-in primitive(sphere) and using it as a dataPoint in scatter-plot
    public void makePlot()
    {
        float scale = 0.03f;
        for (int i = 0; i < x_axis.Count; i++)
        {
            var dataPt = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            dataPt.transform.localPosition = new Vector3(x_axis[i]-(float)74.37, y_axis[i], z_axis[i]+ (float)41.2949);
            dataPt.transform.localRotation = Quaternion.identity;
            dataPt.transform.localScale = new Vector3(scale, scale, scale);
            Material newMaterial = new Material(Shader.Find("VertexLit"));
            newMaterial.color = findColor(clusterID[i]);
            dataPt.GetComponent<Renderer>().material = newMaterial;
            dataPt.gameObject.SetActive(true);

            if (newMaterial.color == Color.green)
            {
                dataPt.name = "SphereGreen";
            }
            else if (newMaterial.color == Color.blue)
            {
                dataPt.name = "SphereBlue";
            }
            else
            {
                dataPt.name = "SphereRed";
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
    void Update() {
        if (ScatterplotCamera.enabled)
        {
            if (Input.GetKey("z"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("SphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

            }


            if (Input.GetKey("x"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("SphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

            }

            if (Input.GetKey("c"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("SphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = false;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereRed"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

            }

            if (Input.GetKey("r"))
            {
                foreach (GameObject sphere in FindGameObjectsWithName("SphereGreen"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereBlue"))
                {
                    sphere.GetComponent<Renderer>().enabled = true;
                }

                foreach (GameObject sphere in FindGameObjectsWithName("SphereRed"))
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
