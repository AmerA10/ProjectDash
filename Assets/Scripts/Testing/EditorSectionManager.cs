
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[ExecuteInEditMode]

public class EditorSectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    SectionManager sm;
    [SerializeField] Vector2 xCLamp;
    [SerializeField] Vector2 yCLamp;
    [SerializeField] CameraController cam;

    void Start()
    {
        sm = GetComponent<SectionManager>();
        cam = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ApplyClamps()
    {
        sm.SetClamps(xCLamp, yCLamp);
    }

    public void ApplyClampsToCam()
    {
        cam = FindObjectOfType<CameraController>();
        cam.SetClamp(xCLamp, yCLamp);
    }
    public void ResetClamps()
    {
        xCLamp.x = this.transform.position.x;
        xCLamp.y = this.transform.position.x;
        yCLamp = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
 
        float YLineX = (xCLamp.y + xCLamp.x) / 2;
        Vector2 YlineStart = new Vector2(YLineX, yCLamp.y);
        Vector2 YLineEnd = new Vector2(YLineX, yCLamp.x);
        float XLineY = (yCLamp.y + yCLamp.x) / 2;
        Vector2 XlineStart = new Vector2(xCLamp.x, XLineY);
        Vector2 XLineEnd = new Vector2(xCLamp.y, XLineY);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(YlineStart , YLineEnd);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(XlineStart, XLineEnd);

    }
}

[CustomEditor(typeof(EditorSectionManager))]
public class customeInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorSectionManager esm = (EditorSectionManager)target;
        if(GUILayout.Button("Apply Clamps to Section"))
        {
            esm.ApplyClamps();
        }
        if (GUILayout.Button("Apply Clamps to Camera"))
        {
            esm.ApplyClampsToCam();
        }
        if(GUILayout.Button("Reset Clamps"))
        {
            esm.ResetClamps();
        }

    }



}

#endif