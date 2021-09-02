using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public GameObject textPrefab;

    public GameObject[] text;


    public int numOfLabels = 10;
    public int lastLabelNum = 100;

    public float startAngle = -30f;
    public float endAngle = 210f;

    public Image origin;

    public Canvas renderCavas;

    public GameObject car;
    float diff;
    // Start is called before the first frame update
    void Start()
    {
        text = new GameObject[numOfLabels];

        diff = (endAngle - startAngle) / (float)numOfLabels;

        for (int i = 0; i < numOfLabels; i++)
        {
            text[i] = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);
            

            text[i].transform.SetParent(renderCavas.transform);
            text[i].transform.localScale = Vector3.one;

            float a = startAngle - ((float)i * diff) - 180f;
      
            Vector3 pos = origin.transform.position + new Vector3(110f * Mathf.Cos(a * Mathf.Deg2Rad), 110f * Mathf.Sin(a * Mathf.Deg2Rad));
            text[i].transform.position = pos;
            text[i].transform.rotation = Quaternion.Euler(0f, 0f, a);

            float txt = (float)lastLabelNum * ((float)i / (float)numOfLabels);
            text[i].GetComponentInChildren<Text>().text = (txt).ToString("F0");
            text[i].GetComponentInChildren<Text>().gameObject.transform.eulerAngles = Vector3.zero;

            float tval = (txt / (float)lastLabelNum); //(float)lastLabelNum - (lastLabelNum / (float)numOfLabels);
            //if (tval < 0.7f) tval = 0.0f;
            
            //text[i].GetComponentInChildren<Text>().color = Color.Lerp(Color.white, Color.red, tval);
            //textPrefab.SetActive(false);
        
        }
        //text[0] = Instantiate(textPrefab, transform.position, Quaternion.identity);
        //text[0].transform.SetParent(renderCavas.transform);
        //text[0].transform.localScale = Vector3.one;
        
        //Instantiate(text, transform.position, Quaternion.identity);
        
    }
    float angle = 0f;
    // Update is called once per frame
    void Update()
    {
        float max = lastLabelNum;// - (lastLabelNum / (float)numOfLabels);
        float speed = car.GetComponent<CarEngine>()._lateralSpeed;
        float angle = startAngle - ((endAngle - startAngle) * (speed / max));

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
