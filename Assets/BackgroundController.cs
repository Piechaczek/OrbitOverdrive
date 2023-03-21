using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{

    public GameObject backgroundScrollerPrefab;

    private float bgSize = 100.0f;
    private GameObject bgTR;
    private GameObject bgTL;
    private GameObject bgBR;
    private GameObject bgBL;
    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        bgTR = transform.GetChild(0).gameObject;
        bgTL = Instantiate(backgroundScrollerPrefab, new Vector3(-bgSize, 0, 2), Quaternion.identity, transform);
        bgBR = Instantiate(backgroundScrollerPrefab, new Vector3(0, -bgSize, 2), Quaternion.identity, transform);
        bgBL = Instantiate(backgroundScrollerPrefab, new Vector3(-bgSize, -bgSize, 2), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (bgTR.transform.position.x > bgSize) {
            bgTR.transform.position -= new Vector3(2.0f* bgSize, 0, 0);
            bgBR.transform.position -= new Vector3(2.0f* bgSize, 0, 0);

            GameObject tmp1 = bgTR;
            GameObject tmp2 = bgBR;

            bgTR = bgTL;
            bgBR = bgBL;
            bgTL = tmp1;
            bgBL = tmp2;
        }

        if (bgTR.transform.position.y > bgSize) {
            bgTR.transform.position -= new Vector3(0, 2.0f* bgSize, 0);
            bgTL.transform.position -= new Vector3(0, 2.0f* bgSize, 0);

            GameObject tmp1 = bgTR;
            GameObject tmp2 = bgTL;

            bgTR = bgBR;
            bgTL = bgBL;
            bgBR = tmp1;
            bgBL = tmp2;
        }

        bgTR.transform.position += new Vector3(speed, speed, 0);
        bgTL.transform.position += new Vector3(speed, speed, 0);
        bgBR.transform.position += new Vector3(speed, speed, 0);
        bgBL.transform.position += new Vector3(speed, speed, 0);


    }
}
