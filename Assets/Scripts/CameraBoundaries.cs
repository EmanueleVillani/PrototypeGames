using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    static Camera camera;
    public static float N, S, W, E;
    // Start is called before the first frame update
    void Start()
    {
        int width = Screen.width;
        int heigth = Screen.height;
        camera= GetComponent<Camera>();
        camera.orthographicSize = ((float)heigth / (float)width) * 115f;
        Vector3 poisition= transform.position + transform.up * camera.orthographicSize;
        N = (Quaternion.Euler(90- transform.rotation.eulerAngles.x, 0, 0) * (transform.position + transform.forward * 600 + transform.up * camera.orthographicSize)).z;
        S = (Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0, 0) * (transform.position + transform.forward * 600 - transform.up * camera.orthographicSize)).z;
        W = (Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0, 0) * (transform.position + transform.forward * 600 + transform.right * (camera.orthographicSize / Screen.height) * Screen.width)).x;
        E = (Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0, 0) * (transform.position + transform.forward * 600 - transform.right * (camera.orthographicSize / Screen.height) * Screen.width)).x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Vector4*Vector3
        //q1(x,y,z,w)*p1(x,y,z)=p2(x,y,z)
        camera = GetComponent<Camera>();
        Gizmos.DrawSphere(Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0,0)*(transform.position+transform.forward * 600 + transform.up * camera.orthographicSize), 10f);
        Gizmos.DrawSphere(Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0,0)*(transform.position+transform.forward * 600 - transform.up * camera.orthographicSize), 10f);
        Gizmos.DrawSphere(Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0,0)*(transform.position+transform.forward * 600 + transform.right * (camera.orthographicSize / Screen.height) * Screen.width), 10f);
        Gizmos.DrawSphere(Quaternion.Euler(90 - transform.rotation.eulerAngles.x, 0,0)*(transform.position+transform.forward * 600 - transform.right * (camera.orthographicSize / Screen.height) * Screen.width), 10f);

        Vector3[] frustumCorners = new Vector3[4];
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
            Debug.DrawRay(camera.transform.position, worldSpaceCorner, Color.blue);
        }

        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), transform.position.z-(-1.3f), Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = camera.transform.TransformVector(frustumCorners[i]);
            Debug.DrawRay(camera.transform.position, worldSpaceCorner, Color.red);
        }



    }
}
