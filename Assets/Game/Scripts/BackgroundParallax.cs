using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Transform cameraTransform;
    float Distance;
    Vector3 CamPositionStart;

    int BackgroundCount = 0;
    float[] BackgroundSpeed;
    GameObject[]  Background;
    Material[] BackgroundMaterials;
   // public Material BackMaterials;
    float BackgroundFar;
    [Range (0.01f,0.05f)]
    public float Speed;
    [Range (0,100)]
    public float SpeedCam;    
    // Start is called before the first frame update
    void Start()
    {
        //cameraTransform = Camera.main.transform;
        CamPositionStart = cameraTransform.position;
        BackgroundCount = transform.childCount;
        BackgroundMaterials = new Material[BackgroundCount];
        BackgroundSpeed = new float[BackgroundCount];
        Background = new GameObject[BackgroundCount];
        for (int i = 0; i < BackgroundCount; i++)
        {
            Background[i] = transform.GetChild(i).gameObject;
            BackgroundMaterials[i] = Background[i].GetComponent<Renderer>().material;

        }
        CalculateSpeed();
    }
    void Update()
    {
        cameraTransform.position += new Vector3(SpeedCam * Time.deltaTime,0,0);
       // Distance += SpeedCam * Time.deltaTime;
       // BackMaterials.SetTextureOffset("_MainTex",Vector2.right * Distance);
    }
    public void CalculateSpeed()
    {
        for (int i = 0; i < BackgroundCount; i++)
        {
            if( (Background[i].transform.position.z - cameraTransform.position.z) > BackgroundFar)
            {
                BackgroundFar = Background[i].transform.position.z - cameraTransform.position.z;
            }
        }

        for (int i = 0; i < BackgroundCount; i++)
        {
            BackgroundSpeed[i] = 1 - (Background[i].transform.position.z - cameraTransform.position.z) / BackgroundFar;
        }        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Distance = cameraTransform.position.x - CamPositionStart.x;
        transform.position = new Vector3(cameraTransform.position.x ,transform.position.y,0);

        for (int i = 0; i < BackgroundCount; i++)
        {
            float speed = BackgroundSpeed[i] * Speed;
            BackgroundMaterials[i].SetTextureOffset("_MainTex",new Vector2(Distance,0) * speed);
        }

    }
}
