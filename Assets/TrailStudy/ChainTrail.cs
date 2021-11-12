using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTrail : MonoBehaviour
{
    // Start is called before the first frame update
    public int length;
    public LineRenderer lineRend;

    public Vector3[] segmentPoses;

    private Vector3[] segmentV;
    public float smoothSpeed;
    public Transform targetDir;
    public float targetDist;

    public Transform tailEnd;

    private bool canCompute = false;


    private void Awake()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentV = new Vector3[length]; 
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if(canCompute)
        ComputeTrail();

    }

    private void ComputeTrail()
    {
        segmentPoses[0] = targetDir.position;
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            targetDist = (tailEnd.position - segmentPoses[0]).magnitude / length; 
            Vector3 targetPos = segmentPoses[i - 1] + (tailEnd.position - targetDir.position).normalized * targetDist;
            segmentPoses[i] = Vector3.Lerp(segmentPoses[i], targetPos, smoothSpeed);

        }

        segmentPoses[segmentPoses.Length - 1] = tailEnd.position;
        lineRend.SetPositions(segmentPoses);
        
    }
    public void StopComputing()
    {
/*        //segmentPoses[0] = Vector2.zero;
   
        for(int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.zero;
        }
        lineRend.SetPositions(segmentPoses);
        canCompute = false;*/
    }

    public void StartComputing()
    {
        canCompute = true;
    }


}
