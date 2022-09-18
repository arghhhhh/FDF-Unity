using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MapGenerator_5))]
public class MapGeneratorEditor_5 : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator_5 mapGen = (MapGenerator_5)target;

        if (DrawDefaultInspector()) //if value is changed in inspector
        {
            if (mapGen.autoUpdate)
                mapGen.GenerateMap();
        }

        if (GUILayout.Button("Generate"))
            mapGen.GenerateMap();
    }
}
