using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Transform playerToFollow;
    public float cameraSmoothSpeed;
    Vector3 desiredLocation;
    Vector3 velocity = Vector3.zero;
    public Vector3 cameraOffset;

    public Transform cloud1_l;
    public Transform cloud1_m;

    public Transform sky;
    public Transform planet;
    public Transform stars;

    private Vector3 cloudStartPos;
    private Vector3 cloudEndPos;

    public float cloud1Speed = 0.01f;
    public float cloud2Speed = 0.05f;
    Vector3 cloudOffset = new Vector3(0, 0, 3f);


    public void Start()
    {
        

        cloud1_l.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (ushort)EnumClass.LayerIDEnum.CLOUDLAYER;
        cloud1_m.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (ushort)EnumClass.LayerIDEnum.CLOUDLAYER;

        sky.   gameObject.GetComponent<SpriteRenderer>().sortingOrder = (ushort)EnumClass.LayerIDEnum.SKYSHADERLAYER;
        planet.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (ushort)EnumClass.LayerIDEnum.PLANETLAYER;
        stars. gameObject.GetComponent<SpriteRenderer>().sortingOrder = (ushort)EnumClass.LayerIDEnum.STARSLAYER;

        

        cloudStartPos = cloud1_l.localPosition + cloudOffset;
        cloudEndPos = new Vector3(cloud1_m.localPosition.x + 50, cloudStartPos.y, 3f);
    }

    private void Update()
    {
        

    }

    public void FixedUpdate()
    {
        if (playerToFollow)
        {
            desiredLocation = playerToFollow.localPosition + cameraOffset;
            this.transform.localPosition = Vector3.SmoothDamp(transform.localPosition, desiredLocation, ref velocity, cameraSmoothSpeed);
        }

        if (cloud1_l.localPosition.x >= cloudEndPos.x) cloud1_l.transform.localPosition = new Vector2(cloud1_m.localPosition.x - 50, cloud1_m.localPosition.y);
        if (cloud1_m.localPosition.x >= cloudEndPos.x) cloud1_m.transform.localPosition = new Vector2(cloud1_l.localPosition.x - 50, cloud1_l.localPosition.y);

        cloud1_l.transform.localPosition = new Vector3(cloud1_l.localPosition.x + cloud1Speed, cloud1_l.localPosition.y, 3f);
        cloud1_m.transform.localPosition = new Vector3(cloud1_m.localPosition.x + cloud1Speed, cloud1_m.localPosition.y, 3f);

    }

    public void SetFocus(Transform focus)
    {
        playerToFollow = focus;
    }

}
