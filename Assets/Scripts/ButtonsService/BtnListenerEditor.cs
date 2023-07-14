using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
[CustomEditor(typeof(BtnListener))]
public class BtnListenerEditor : Editor
{
    BtnListener btn;

    private void OnEnable()
    {
        btn = (BtnListener)target;
    }

    public override void OnInspectorGUI()
    {


        EditorGUILayout.BeginVertical();
        btn.BtnAction = (BtnListenerAction)EditorGUILayout.EnumPopup("BtnListenerAction", btn.BtnAction);
        btn.Type = (ParameterType)EditorGUILayout.EnumPopup("Type", btn.Type);
        switch (btn.Type)
        {
            case ParameterType.Int:
                btn.TypeInt = EditorGUILayout.IntField("Int", btn.TypeInt);
                break;
            case ParameterType.Float:
                btn.TypeFloat = EditorGUILayout.FloatField("Float", btn.TypeFloat);
                break;
            case ParameterType.String:
                btn.TypeString = EditorGUILayout.TextField("String", btn.TypeString);
                break;
            case ParameterType.Bool:
                btn.TypeBool = EditorGUILayout.Toggle("Bool", btn.TypeBool);
                break;
            case ParameterType.Transform:
                btn.TypeTransform = EditorGUILayout.ObjectField("Transform", btn.TypeTransform, typeof(Transform)) as Transform;
                break;
            case ParameterType.Component:
                btn.TypeComponent = EditorGUILayout.ObjectField("Component", btn.TypeComponent, typeof(Component)) as Component;
                break;
        }
        EditorGUILayout.EndVertical();
    }
}
#endif
