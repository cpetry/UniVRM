﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace UniGLTF
{
    /// <summary>
    /// Material information generated by IMaterialDescriptorGenerator
    /// In recent versions, it is easy to manipulate Materials directly using Actions.
    /// 
    /// Actions の使用例は UniGLTF.UrpGltfMaterialDescriptorGenerator を参照
    /// </summary>
    public sealed class MaterialDescriptor
    {
        public delegate Task MaterialGenerateAsyncFunc(Material m, GetTextureAsyncFunc getTexture, IAwaitCaller awaitCaller);

        /// <summary>
        /// <code>
        /// material.name = matDesc.SubAssetKey.Name;
        /// </code>
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// <code>
        /// material = new Material(matDesc.Shader);
        /// </summary>
        public readonly Shader Shader;

        public readonly int? RenderQueue;
        public readonly IReadOnlyDictionary<string, TextureDescriptor> TextureSlots;
        public readonly IReadOnlyDictionary<string, float> FloatValues;
        public readonly IReadOnlyDictionary<string, Color> Colors;
        public readonly IReadOnlyDictionary<string, Vector4> Vectors;

        /// <summary>
        /// Process and construct the argument Material
        /// <code>
        /// material.SetTexture(prop, texture);
        /// material.SetColor(prop, color);
        /// </code>
        /// </summary>
        public readonly IReadOnlyList<Action<Material>> Actions;

        public readonly IReadOnlyList<MaterialGenerateAsyncFunc> AsyncActions;

        public SubAssetKey SubAssetKey => new SubAssetKey(SubAssetKey.MaterialType, Name);

        public MaterialDescriptor(
            string name,
            Shader shader,
            int? renderQueue,
            IReadOnlyDictionary<string, TextureDescriptor> textureSlots,
            IReadOnlyDictionary<string, float> floatValues,
            IReadOnlyDictionary<string, Color> colors,
            IReadOnlyDictionary<string, Vector4> vectors,
            IReadOnlyList<Action<Material>> actions,
            IReadOnlyList<MaterialGenerateAsyncFunc> asyncActions = null)
        {
            Name = name;
            Shader = shader;
            RenderQueue = renderQueue;
            TextureSlots = textureSlots;
            FloatValues = floatValues;
            Colors = colors;
            Vectors = vectors;
            Actions = actions;
            AsyncActions = asyncActions ?? new List<MaterialGenerateAsyncFunc>();
        }
    }
}