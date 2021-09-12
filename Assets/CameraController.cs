using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectionInfo
{
    public Matrix4x4 projection_matrix;// { get; set; }
}

[Serializable]
public class CameraOutputResolution
{
    public int Width;// { get; set; }
    public int Height;// { get; set; }
}

[Serializable]
public class CameraInfo
{
    public Vector3 position; // { get; set; }
    public Quaternion rotation_quat;// { get; set; }
    public Vector3 rotation;// { get; set; }
    public float field_of_view;// { get; set; }
    public ProjectionInfo projection_info;// { get; set; }
    public CameraOutputResolution output_resolution;// { get; set; }
    public Matrix4x4 camToWorld;
    public Matrix4x4 worldToCam;
};

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private int width = 0;
    [SerializeField]
    private int height = 0;

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public CameraInfo GetCameraInfo()
    {
        var camera = this.GetComponent<Camera>();
        return new CameraInfo
        {
            position = camera.transform.position,
            rotation = camera.transform.eulerAngles,
            rotation_quat = camera.transform.rotation,
            field_of_view = camera.fieldOfView,
            worldToCam = camera.worldToCameraMatrix,
            camToWorld = camera.cameraToWorldMatrix,
            projection_info = new ProjectionInfo
            {
                projection_matrix = camera.projectionMatrix
            },
            output_resolution = this.Width == 0 || this.Height == 0 ? null :
                new CameraOutputResolution
                {
                    Width = this.Width,
                    Height = this.Height
                },

        };
    }

    public Texture2D GetScreenShot()
    {
        Camera camera = this.GetComponent<Camera>();
        
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        var cameraRT = camera.targetTexture;

        RenderTexture rt = new RenderTexture(width, height, 24);
 
        camera.targetTexture = rt;
       
        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(width, height);
        RenderTexture.active = rt;
        image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

        // Revert everything back.
        RenderTexture.active = currentRT;
        camera.targetTexture = cameraRT;
        rt.Release();
        return image;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
