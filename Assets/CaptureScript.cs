using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class CaptureScript : MonoBehaviour
{
    CameraController[] cams;
    bool captured = false;

    // Start is called before the first frame update
    void Start()
    {
        cams = GameObject.FindObjectsOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!captured)
            StartCoroutine(SaveFrame());

        captured = true;
    }

    private IEnumerator SaveFrame()
    {
        yield return new WaitForEndOfFrame();

        int index = 0;
        foreach (CameraController cam in cams)
        {
            Texture2D image = cam.GetScreenShot();
            byte[] imgBytes = image.EncodeToPNG();
            string img_path = string.Format("{0}.png", index);
            System.IO.File.WriteAllBytes(img_path, imgBytes);

            Camera test = cam.transform.gameObject.GetComponent<Camera>();
            var foo = test.projectionMatrix;

            CameraInfo camInfo = cam.GetCameraInfo();
            string img_json = string.Format("{0}.json", index);
            var camInfoJson = JsonUtility.ToJson(camInfo, true);
            System.IO.File.WriteAllText(img_json,camInfoJson);
            index++;
        }
    }
}
