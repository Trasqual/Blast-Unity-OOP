using BlastGame.BoardItems.Data;
using BlastGame.LevelManagement.Datas;
using UnityEditor;
using UnityEngine;

namespace BlastGame
{
    [CustomEditor(typeof(LevelData))]
    public class LevelDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            base.OnInspectorGUI();

            SerializedProperty gridDataProperty = serializedObject.FindProperty("GridData");

            LevelData data = (LevelData)target;

            SerializedProperty boardData = serializedObject.FindProperty("BoardData");
            SerializedProperty rows = boardData.FindPropertyRelative("Rows");
            SerializedProperty columns = boardData.FindPropertyRelative("Columns");



            if (data.GridData == null || data.GridData.Length != columns.intValue * rows.intValue)
            {
                data.GridData = new ItemData[columns.intValue * rows.intValue];
            }

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < columns.intValue; i++)
            {
                EditorGUILayout.BeginVertical();
                for (int j = rows.intValue - 1; j >= 0; j--)
                {
                    if (i * rows.intValue + j >= gridDataProperty.arraySize)
                    {
                        return;
                    }
                    SerializedProperty elementProperty = gridDataProperty.GetArrayElementAtIndex(i * rows.intValue + j);
                    EditorGUILayout.PropertyField(elementProperty, GUIContent.none);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Fill Random"))
            {
                for (int i = 0; i < columns.intValue; i++)
                {
                    for (int j = 0; j < rows.intValue; j++)
                    {
                        int randomIndex = Random.Range(0, data.PossibleItems.Count);
                        gridDataProperty.GetArrayElementAtIndex(i * rows.intValue + j).objectReferenceValue = data.PossibleItems[randomIndex];
                        SerializedProperty elementProperty = gridDataProperty.GetArrayElementAtIndex(i * rows.intValue + j);
                        EditorGUILayout.PropertyField(elementProperty, GUIContent.none);
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(data);
        }
    }
}
