using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugin.Code.Runtime.Data
{
    [CreateAssetMenu(fileName = "GridRegistry", menuName = "Grid System/Grid Registry")]
    public class GridRegistry : ScriptableObject
    {
        [Serializable]
        public sealed class Entry
        {
            public string displayName;
            public GridDefinition definition;
        }

        public List<Entry> entries = new();
    }
}