using Plugin.Code.Runtime.Data;
using UnityEditor;
using UnityEngine;

namespace Plugin.Code.Editor
{
    public sealed class GridToolsWindow : EditorWindow
    {
        private GridRegistry _registry;

        private int _selectedIndex = 0;
        private UnityEditor.Editor _definitionEditor;
        private Vector2 _scroll;

        [MenuItem("Tools/GridTools")]
        public static void Open()
        {
            var window = GetWindow<GridToolsWindow>("GridTools");
            window.minSize = new Vector2(320, 400);
        }

        private void OnGUI()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);

            EditorGUILayout.LabelField("Grid Tools", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            _registry = (GridRegistry)EditorGUILayout.ObjectField("Registry", _registry, typeof(GridRegistry), false);

            if (!_registry)
            {
                EditorGUILayout.HelpBox("Assign a GridRegistry asset to begin.", MessageType.Info);
                EditorGUILayout.EndScrollView();
                return;
            }

            if (_registry.entries == null || _registry.entries.Count == 0)
            {
                EditorGUILayout.HelpBox("Registry has no entries.", MessageType.Warning);
                EditorGUILayout.EndScrollView();
                return;
            }

            EditorGUILayout.Space();

            string[] names = BuildDropdownNames();
            _selectedIndex = EditorGUILayout.Popup("Grid Type", _selectedIndex, names);
            _selectedIndex = Mathf.Clamp(_selectedIndex, 0, _registry.entries.Count - 1);

            var entry = _registry.entries[_selectedIndex];
            var definition = entry.definition;

            EditorGUILayout.Space();

            if (!definition)
                EditorGUILayout.HelpBox("This entry has no definition assigned.", MessageType.Error);
            else
                DrawDefinitionInspector(definition);

            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(!definition))
                if (GUILayout.Button("Create Grid", GUILayout.Height(32))) 
                    GridBuilder.Build(definition);

            EditorGUILayout.EndScrollView();
        }

        private string[] BuildDropdownNames()
        {
            var names = new string[_registry.entries.Count];
            for (int i = 0; i < names.Length; i++)
            {
                var e = _registry.entries[i];
                names[i] = string.IsNullOrEmpty(e.displayName) ? $"Entry {i}" : e.displayName;
            }
            return names;
        }

        private void DrawDefinitionInspector(GridDefinition definition)
        {
            if (!_definitionEditor || _definitionEditor.target != definition)
            {
                if (_definitionEditor) DestroyImmediate(_definitionEditor);
                _definitionEditor = UnityEditor.Editor.CreateEditor(definition);
            }

            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            _definitionEditor.OnInspectorGUI();
        }
        
        private void TryAutoLoadRegistry()
        {
            var guids = AssetDatabase.FindAssets("t:GridRegistry");
            if (guids.Length > 0)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                _registry = AssetDatabase.LoadAssetAtPath<GridRegistry>(path);
            }
        }
        
        private void OnEnable()
        {
            if (!_registry) TryAutoLoadRegistry();
        }

        private void OnDisable()
        {
            if (_definitionEditor) DestroyImmediate(_definitionEditor);
        }
    }
}