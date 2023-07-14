using UnityEngine;
using UnityEngine.UI;

public enum ParameterType
{
    None,
    ThisTransform,
    ThisButton,
    Int,
    Float,
    String,
    Bool,
    Transform,
    Component,
}
public class BtnListener : MonoBehaviour
{
    public BtnListenerAction BtnAction;
    public ParameterType Type;
    public int TypeInt;
    public float TypeFloat;
    public string TypeString;
    public bool TypeBool;
    public Transform TypeTransform;
    public Component TypeComponent;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ClickAct);
    }

    void ClickAct()
    {
        switch (Type)
        {
            case ParameterType.None:
                BtnActions.NoneActs[BtnAction]();
                break;
            case ParameterType.ThisTransform:
                BtnActions.TransformActs[BtnAction](transform);
                break;
            case ParameterType.ThisButton:
                BtnActions.TransformActs[BtnAction](transform);
                break;
            case ParameterType.Int:
                BtnActions.IntActs[BtnAction](TypeInt);
                break;
            case ParameterType.Float:
                BtnActions.FloatActs[BtnAction](TypeFloat);
                break;
            case ParameterType.String:
                BtnActions.StringActs[BtnAction](TypeString);
                break;
            case ParameterType.Bool:
                BtnActions.BoolActs[BtnAction](TypeBool);
                break;
            case ParameterType.Transform:
                BtnActions.TransformActs[BtnAction](TypeTransform);
                break;
            case ParameterType.Component:
                BtnActions.ComponentActs[BtnAction](TypeComponent);
                break;
        }
    }
}


