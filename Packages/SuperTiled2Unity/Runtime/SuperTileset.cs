﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SuperTiled2Unity
{
    public class SuperTileset : ScriptableObject
    {
        [ReadOnly]
        public int m_TileWidth;

        [ReadOnly]
        public int m_TileHeight;

        [ReadOnly]
        public int m_Spacing;

        [ReadOnly]
        public int m_Margin;

        [ReadOnly]
        public int m_TileCount;

        [ReadOnly]
        public int m_TileColumns;

        [ReadOnly]
        public Vector2 m_TileOffset;

        [ReadOnly]
        public GridOrientation m_GridOrientation;

        [ReadOnly]
        public Vector2 m_GridSize;

        [ReadOnly]
        public bool m_IsInternal;

        [ReadOnly]
        public float m_PixelsPerUnit;

        [ReadOnly]
        public bool m_IsImageCollection;

        [ReadOnly]
        public ObjectAlignment m_ObjectAlignment;

        [ReadOnly]
        public TileRenderSize m_TileRenderSize;

        [ReadOnly]
        public FillMode m_FillMode;

        public List<CustomProperty> m_CustomProperties;

        [ReadOnly]
        public List<SuperTile> m_Tiles = new List<SuperTile>();

        public bool TryGetTile(int tileId, out SuperTile tile)
        {
            tile = m_Tiles.FirstOrDefault(t => t.m_TileId == tileId);
            return tile != null;
        }
    }
}
