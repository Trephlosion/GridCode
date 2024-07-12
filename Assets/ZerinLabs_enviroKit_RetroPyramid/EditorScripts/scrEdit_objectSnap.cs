using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class scrEdit_objectSnap : ScriptableWizard
{
    public float movGridStep = 1.0f;
    public float rotationStep = 90.0f;

    //public int selGridInt = 0;
    //public string[] selStrings = new string[] { "XZ", "YZ", "XY" };
    bool togglePlane_XZ = true;
    bool togglePlane_YZ = false;
    bool togglePlane_XY = false;

    bool rotAxis_X = false;
    bool rotAxis_Y = true;
    bool rotAxis_Z = false;

    // add menu item
    [MenuItem("ZerinLabs/Object snap utility")]

    //-----------------------------------------------------------------------------------------------------------------
    static void Init()
    {
        scrEdit_objectSnap window = (scrEdit_objectSnap)EditorWindow.GetWindow(typeof(scrEdit_objectSnap));
        window.Show();
    }

    //-----------------------------------------------------------------------------------------------------------------
    void OnGUI()
    {
        float offset_Vmov = 290.0f;
        float offset_Hmov = 16.0f;

        GUILayout.Label("Movement parameters", EditorStyles.boldLabel);

        movGridStep = EditorGUILayout.FloatField("Move grid step (units):", movGridStep);

        GUILayout.Label("Movement plane:");

        if (GUILayout.Toggle(togglePlane_XZ, "XZ plane"))
        {
            toggle_plane("XZ");
        }
        if(GUILayout.Toggle(togglePlane_YZ, "YZ plane"))
        {
            toggle_plane("YZ");
        }
        if(GUILayout.Toggle(togglePlane_XY, "XY plane"))
        {
            toggle_plane("XY");
        }

        GUILayout.Space(10);

        GUILayout.Label("Rotation parameters", EditorStyles.boldLabel);

        rotationStep = EditorGUILayout.FloatField("Rotation step (degrees):", rotationStep);

        GUILayout.Label("Rotation axis");

        if (GUILayout.Toggle(rotAxis_X, "X axis"))
        {
            toggle_axis("X");
        }
        if(GUILayout.Toggle(rotAxis_Y, "Y axis"))
        {
            toggle_axis("Y");
        }
        if(GUILayout.Toggle(rotAxis_Z, "Z axis"))
        {
            toggle_axis("Z");
        }

        GUILayout.Space(10);

        GUILayout.Label("Shortcut handlers", EditorStyles.boldLabel);

        GUILayout.BeginArea(new Rect(12, 270, 290, 32));

        GUILayout.BeginHorizontal();
        GUILayout.Label("Movement handlers");
        GUILayout.Label("Rotation handlers");
        GUILayout.EndHorizontal();

        GUILayout.EndArea();

        //movement handlers
        if (GUI.Button(new Rect(offset_Hmov + 32, offset_Vmov, 32, 32), "UP"))
        {
            moveObjects("UP");
        }
        if (GUI.Button(new Rect(offset_Hmov, offset_Vmov + 32, 32, 32), "LF"))
        {
            moveObjects("LF");
        }
        if (GUI.Button(new Rect(offset_Hmov + 64, offset_Vmov + 32, 32, 32), "RT"))
        {
            moveObjects("RT");
        }
        if (GUI.Button(new Rect(offset_Hmov + 32, offset_Vmov + 64, 32, 32), "DN"))
        {
            moveObjects("DN");
        }


        //rotation handlers
        float offset_Hrot = 160.0f;

        if (GUI.Button(new Rect(offset_Hmov + offset_Hrot, offset_Vmov + 32, 32, 32), "<"))
        {
            rotateObjects("LF");
        }
        if (GUI.Button(new Rect(offset_Hmov + offset_Hrot + 48, offset_Vmov + 32, 32, 32), ">"))
        {
            rotateObjects("RT");
        }
    }

    //-----------------------------------------------------------------------------------------------------------------
    void toggle_plane(string plane)
    {
        togglePlane_XZ = false;
        togglePlane_YZ = false;
        togglePlane_XY = false;

        switch (plane)
        {
            case "XZ":
                togglePlane_XZ = true;
                break;
            case "YZ":
                togglePlane_YZ = true;
                break;
            case "XY":
                togglePlane_XY = true;
                break;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------
    void toggle_axis(string axis)
    {
        rotAxis_X = false;
        rotAxis_Y = false;
        rotAxis_Z = false;

        switch (axis)
        {
            case "X":
                rotAxis_X = true;
                break;
            case "Y":
                rotAxis_Y = true;
                break;
            case "Z":
                rotAxis_Z = true;
                break;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------
    void moveObjects(string dir)
    {
        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            GameObject go = Selection.transforms[i].gameObject;

            Vector3 shift = new Vector3(movGridStep, 0f, 0f);

            float stepVal = movGridStep;

            if (togglePlane_XZ == true)
            {
                switch (dir)
                {
                    case "RT":
                        shift = new Vector3(stepVal, 0f,0f);
                        break;
                    case "LF":
                        shift = new Vector3(stepVal, 0f, 0f) * -1f;
                        break;
                    case "UP":
                        shift = new Vector3(0f, 0f, stepVal);
                        break;
                    case "DN":
                        shift = new Vector3(0f, 0f, stepVal) * -1f;
                        break;
                }
            }

            if (togglePlane_YZ == true)
            {
                switch (dir)
                {
                    case "RT":
                        shift = new Vector3(0f, 0f, stepVal);
                        break;
                    case "LF":
                        shift = new Vector3(0f, 0f, stepVal) * -1f;
                        break;
                    case "UP":
                        shift = new Vector3(0f, stepVal, 0f);
                        break;
                    case "DN":
                        shift = new Vector3(0f, stepVal, 0f) * -1f;
                        break;
                }
            }

            if (togglePlane_XY == true)
            {
                switch (dir)
                {
                    case "RT":
                        shift = new Vector3(0f, 0f, stepVal);
                        break;
                    case "LF":
                        shift = new Vector3(0f, 0f, stepVal) * -1f;
                        break;
                    case "UP":
                        shift = new Vector3(0f, stepVal, 0f);
                        break;
                    case "DN":
                        shift = new Vector3(0f, stepVal, 0f) * -1f;
                        break;
                }
            }

            Undo.RegisterCompleteObjectUndo(go, "Changed position");

            go.transform.position = go.transform.position + shift;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------
    void rotateObjects(string dir)
    {
        Vector3 axisPivot = new Vector3(0f, 0f, 0f);

        if (Selection.transforms.Length > 1)
        {
            for (int i = 0; i < Selection.transforms.Length; i++)
            {
                GameObject go = Selection.transforms[i].gameObject;

                axisPivot += go.transform.position;
            }

            axisPivot = axisPivot / Selection.transforms.Length;
        }        

        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            GameObject go = Selection.transforms[i].gameObject;

            Vector3 axisVec = new Vector3(1f, 0f, 0f);

            if (rotAxis_X == true)
            {
                axisVec = new Vector3(1f, 0f, 0f);
            }

            if (rotAxis_Y == true)
            {
                axisVec = new Vector3(0f, 1f, 0f);
            }

            if (rotAxis_Z == true)
            {
                axisVec = new Vector3(0f, 0f, 1f);
            }

            Undo.RegisterCompleteObjectUndo(go, "Changed rotation");

            if (Selection.transforms.Length == 1)
            {
                axisPivot = go.transform.position;
            }

            switch (dir)
            {
                case "RT":
                    go.transform.RotateAround(axisPivot, axisVec, rotationStep);
                    break;
                case "LF":
                    go.transform.RotateAround(axisPivot, axisVec, rotationStep * -1f);
                    break;
            }
        }
    }
}
#endif