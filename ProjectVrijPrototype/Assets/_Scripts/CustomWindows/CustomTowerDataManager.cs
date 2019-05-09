
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(TowerDataManager))]
[CanEditMultipleObjects]
public class CustomTowerDataManagerEditor : MonoBehaviour
{
    SerializedProperty towerDatas;
    /*
    void OnEnable()
    {
        towerDatas = serializedObject.Find
        towerDatas = serializedObject.FindProperty("TowerDatas");
    }
​
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(lookAtPoint);
        serializedObject.ApplyModifiedProperties();
    }*/
}
