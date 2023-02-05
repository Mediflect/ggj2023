using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornMaterialHelper : MonoBehaviour
{
    private static ThornMaterialHelper Instance;

    [ColorUsage(true, true)]
    public Color defaultThornColor;
    public Material thornMaterial;

    private const string EMISSION_PROP = "_EmissionColor";

    public static void ResetThornColor()
    {
        Instance.InternalResetThornColor();
    }

    private void InternalResetThornColor()
    {
        thornMaterial.SetColor(EMISSION_PROP, defaultThornColor);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        InternalResetThornColor();
    }

    private void OnDestroy()
    {
        if (Instance != this)
        {
            return;
        }
        InternalResetThornColor();
    }
}
