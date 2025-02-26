﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{
[RequireComponent(typeof(Graphic))]
// Doesn't seem to cause problem any more. Hmm
// [DisallowMultipleComponent]
[HelpURL("https://leloctai.com/trueshadow/docs/articles/customize.html")]
[ExecuteAlways]
public partial class TrueShadow : UIBehaviour, IMeshModifier, ICanvasElement
{
    static readonly Color DEFAULT_COLOR = new Color(0, 0, 0, .3f);

    [Tooltip("Size of the shadow")]
    [SerializeField] float size = 32;

    [Tooltip("Direction to offset the shadow toward")]
    [Knob]
    [SerializeField] float offsetAngle = 90;

    [Tooltip("How far to offset the shadow")]
    [SerializeField] float offsetDistance = 8;

    [SerializeField] Vector2 offset = Vector2.zero;

    [Tooltip("Tint the shadow")]
    [SerializeField] Color color = DEFAULT_COLOR;

    [Tooltip("Inset shadow")]
    [InsetToggle]
    [SerializeField] bool inset = false;

    [Tooltip("Blend mode of the shadow")]
    [SerializeField] BlendMode blendMode;

    [FormerlySerializedAs("multiplyCasterAlpha")]
    [Tooltip("Allow shadow to cross-fade with caster")]
    [SerializeField] bool useCasterAlpha = true;

    [Tooltip("Ignore the shadow caster's color, so you can choose specific color for your shadow")]
    [SerializeField] bool ignoreCasterColor = false;

    [Tooltip(
        "How to obtain the color of the area outside of the source image. Automatically set based on Blend Mode. You should only change this setting if you are using some very custom UI that require it")]
    [SerializeField] ColorBleedMode colorBleedMode;

    [Tooltip("Position the shadow GameObject as previous sibling of the UI element")]
    [SerializeField] bool shadowAsSibling;

    [Tooltip("Cut the source image from the shadow to avoid shadow showing behind semi-transparent UI")]
    [SerializeField] bool cutout;

#pragma warning disable 0649
    [Tooltip(
        "Bake the shadow to a sprite to reduce CPU and GPU usage at runtime, at the cost of storage, memory and flexibility")]
    [SerializeField] bool baked;
#pragma warning restore 0649

    [SerializeField] bool modifiedFromInspector = false;

    public float Size
    {
        get => size;
        set
        {
            var newSize = Mathf.Max(0, value);
            if (modifiedFromInspector || !Mathf.Approximately(size, newSize))
            {
                modifiedFromInspector = false;

                SetLayoutDirty();
                SetTextureDirty();
            }

            size = newSize;
            if (Inset && OffsetDistance > Size)
            {
                OffsetDistance = Size;
            }
        }
    }

    public float OffsetAngle
    {
        get => offsetAngle;
        set
        {
            var newValue = (value + 360f) % 360f;
            if (modifiedFromInspector || !Mathf.Approximately(offsetAngle, newValue))
            {
                modifiedFromInspector = false;

                SetLayoutDirty();
                if (Cutout)
                    SetTextureDirty();
            }

            offsetAngle = newValue;
            offset      = Math.AngleDistanceVector(offsetAngle, offset.magnitude, Vector2.right);
        }
    }

    public float OffsetDistance
    {
        get => offsetDistance;
        set
        {
            // Limit offset distance for now.
            // In order to properly render larger offset, imprint have to be rendered twice.
            // TODO: Implement if no one complain about perf
            var newValue = value;
            if (Inset)
                newValue = Mathf.Clamp(newValue, 0, Size);
            else
                newValue = Mathf.Max(0, newValue);
            if (modifiedFromInspector || !Mathf.Approximately(offsetDistance, newValue))
            {
                modifiedFromInspector = false;

                SetLayoutDirty();
                if (Cutout)
                    SetTextureDirty();
            }

            offsetDistance = newValue;
            offset = offset.sqrMagnitude < 1e-6f
                         ? Math.AngleDistanceVector(offsetAngle, offsetDistance, Vector2.right)
                         : offset.normalized * offsetDistance;
        }
    }

    public Color Color
    {
        get => color;
        set
        {
            if (modifiedFromInspector || value != color)
            {
                modifiedFromInspector = false;

                SetLayoutDirty();
            }

            color = value;
        }
    }

    /// <summary>
    /// Allow shadow to cross-fade with caster
    /// </summary>
    public bool UseCasterAlpha
    {
        get => useCasterAlpha;
        set
        {
            if (modifiedFromInspector || value != useCasterAlpha)
            {
                modifiedFromInspector = false;

                SetLayoutDirty();
            }

            useCasterAlpha = value;
        }
    }

    /// <summary>
    /// Ignore the shadow caster's color, so you can choose specific color for your shadow.
    /// When false, <see cref="Color"/> is multiplied with caster's color.
    /// </summary>
    public bool IgnoreCasterColor
    {
        get => ignoreCasterColor;
        set
        {
            if (modifiedFromInspector || value != ignoreCasterColor)
            {
                modifiedFromInspector = false;

                SetTextureDirty();
            }

            ignoreCasterColor = value;
        }
    }

    public bool Inset
    {
        get => inset;
        set
        {
            if (modifiedFromInspector || value != inset)
            {
                modifiedFromInspector = false;

                SetTextureDirty();
            }

            inset = value;

            if (Inset && OffsetDistance > Size)
            {
                OffsetDistance = Size;
            }
        }
    }

    public BlendMode BlendMode
    {
        get => blendMode;
        set
        {
            // Work around for Unity bug causing references loss
            if (!Graphic || !CanvasRenderer)
                return;

            blendMode = value;
            shadowRenderer.UpdateMaterial();

            switch (blendMode)
            {
            case BlendMode.Normal:
                ColorBleedMode = ColorBleedMode.ImageColor;
                break;
            case BlendMode.Additive:
                ColorBleedMode = ColorBleedMode.Black;
                break;
            case BlendMode.Multiply:
                ColorBleedMode = ColorBleedMode.White;
                break;
            default:
                ColorBleedMode = ColorBleedMode.ImageColor;
                throw new ArgumentOutOfRangeException(nameof(blendMode), blendMode, null);
            }
        }
    }

    /// <summary>
    /// How to obtain the color of the area outside of the source image. Automatically set based on Blend Mode. You should only change this setting if you are using some very custom UI that require it.
    ///
    /// <seealso cref="ClearColor"/>
    /// </summary>
    public ColorBleedMode ColorBleedMode
    {
        get => colorBleedMode;
        set
        {
            if (modifiedFromInspector || colorBleedMode != value)
            {
                modifiedFromInspector = false;

                colorBleedMode = value;
                SetTextureDirty();
            }
        }
    }

    /// <summary>
    /// The area where the alpha channel = 0 can be either 0, or the color of the edge of the texture, depend on how the texture was authored.
    /// Normally this is not visible, but when blurred, the alpha in these area will become greater than 0
    /// Depend on the blendmode, different color for this clear area may be desired.
    ///
    /// You can provide custom clear color by implementing <see cref="PluginInterfaces.ITrueShadowCasterClearColorProvider"/>, and set this to Plugin
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Color ClearColor
    {
        get
        {
            if (IgnoreCasterColor)
                return new Color(1, 1, 1, 0);

            switch (colorBleedMode)
            {
            case ColorBleedMode.ImageColor:
                return Graphic.color.WithA(0);
            case ColorBleedMode.ShadowColor:
                return Color.WithA(0);
            case ColorBleedMode.Black:
                return Color.clear;
            case ColorBleedMode.White:
                return new Color(1, 1, 1, 0);
            case ColorBleedMode.Plugin:
                return casterClearColorProvider?.GetTrueShadowCasterClearColor() ?? Color.clear;
            default:
                throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Can't be implemented due to <see href="https://issuetracker.unity3d.com/issues/prefab-instances-sibling-index-is-not-updated-when-a-lower-index-sibling-is-deleted">Unity's bug 1280465</see>. Do not use!
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool ShadowAsSibling
    {
        get => shadowAsSibling;
        set
        {
            shadowAsSibling = value;
            ShadowRenderer.ClearMaskMaterialCache();
            if (shadowAsSibling)
            {
                ShadowSorter.Instance.Register(this);
            }
            else
            {
                ShadowSorter.Instance.UnRegister(this);
                if (shadowRenderer) // defensive. undo & prefab make state weird sometime
                {
                    var rendererTransform = shadowRenderer.transform;
                    rendererTransform.SetParent(transform, true);
                    rendererTransform.SetSiblingIndex(0);
                }
            }
        }
    }

    /// <summary>
    /// Always true due to <see cref="ShadowAsSibling"/>. Do not use!
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool Cutout
    {
        get => !shadowAsSibling || cutout;
        set => cutout = value;
    }

    /// <summary>
    /// Not implemented. Do not use
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public bool Baked
    {
        get => baked;
        // set
        // {
        //     baked = value;
        //
        //     if (baked)
        //     {
        //         BakeShadows();
        //     }
        //     else
        //     {
        //         RemoveBakedShadow();
        //         SetTextureDirty();
        //     }
        // }
    }

    [SerializeField] List<Sprite> bakedShadows;

    public Vector2 Offset => offset;

    internal ShadowRenderer shadowRenderer;

    internal Mesh           SpriteMesh     { get; set; }
    internal Graphic        Graphic        { get; set; }
    internal CanvasRenderer CanvasRenderer { get; set; }
    internal RectTransform  RectTransform  { get; private set; }

    internal Texture Content
    {
        get
        {
            switch (Graphic)
            {
            case Image image:
                var sprite = image.overrideSprite;
                return sprite ? sprite.texture : null;
            case RawImage rawImage: return rawImage.texture;
            case Text text:         return text.mainTexture;
            default:                return null;
            }
        }
    }

    int textureRevision;

    internal int ContentHash
    {
        get
        {
            int insetHash = Inset ? 1 : 0;

            // Hack until we have separated cutout cache, or proper sibling mode
            int cutoutHash = HashUtils.CombineHashCodes(
                Cutout ? 1 : 0,
                (int) (Offset.x * 100),
                (int) (Offset.y * 100)
            );

            var clearColor = ClearColor;
            var imageColor = Graphic.color;
            int colorHash = HashUtils.CombineHashCodes(
                ignoreCasterColor ? 1 : 0,
                (int) ColorBleedMode,
                (int) (imageColor.r * 255),
                (int) (imageColor.g * 255),
                (int) (imageColor.b * 255),
                (int) (imageColor.a * 255),
                (int) (clearColor.r * 255),
                (int) (clearColor.g * 255),
                (int) (clearColor.b * 255),
                (int) (clearColor.a * 255)
            );

            // Pixel not lineup when rotating.
            var rotationHash = Cutout ? transform.rotation.GetHashCode() : 0;

            var commonHash = HashUtils.CombineHashCodes(
                textureRevision,
                insetHash,
                cutoutHash,
                colorHash,
                rotationHash
            );

            switch (Graphic)
            {
            case Image image:
                int spriteHash = 0;
                if (image.sprite)
                    spriteHash = image.sprite.GetHashCode();

                int imageHash = HashUtils.CombineHashCodes(
                    (int) image.type,
                    (int) (image.fillAmount * 360 * 20),
                    (int) image.fillMethod,
                    image.fillOrigin,
                    image.fillClockwise ? 1 : 0
                );

                return HashUtils.CombineHashCodes(
                    commonHash,
                    spriteHash,
                    imageHash
                );
            case RawImage rawImage:
                var textureHash = 0;
                if (rawImage.texture)
                    textureHash = rawImage.texture.GetHashCode();

                return HashUtils.CombineHashCodes(
                    commonHash,
                    textureHash,
                    textureRevision
                );
            default: return commonHash;
            }
        }
    }

#if LETAI_TRUESHADOW_DEBUG
    public bool alwaysRender;
#endif

    ShadowContainer shadowContainer;

    bool          textureDirty;
    bool          layoutDirty;
    internal bool hierachyDirty;

    protected override void Awake()
    {
#if UNITY_EDITOR
        UnityEditor.Undo.undoRedoPerformed += ApplySerializedData;
#endif
        if (ShadowAsSibling)
            ShadowSorter.Instance.Register(this);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        RectTransform  = GetComponent<RectTransform>();
        Graphic        = GetComponent<Graphic>();
        CanvasRenderer = GetComponent<CanvasRenderer>();
        if (!SpriteMesh) SpriteMesh = new Mesh();

        InitializePlugins();

        if (bakedShadows == null)
            bakedShadows = new List<Sprite>(4);

        InitInvalidator();

        ShadowRenderer.Initialize(this, ref shadowRenderer);

        Canvas.willRenderCanvases += OnWillRenderCanvas;

        if (Graphic)
            Graphic.SetVerticesDirty();

#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
#endif
    }

    public void ApplySerializedData()
    {
        Size            = size;
        OffsetAngle     = offsetAngle;
        OffsetDistance  = offsetDistance;
        BlendMode       = blendMode;
        ShadowAsSibling = shadowAsSibling;

        SetHierachyDirty();
        SetLayoutDirty();
        SetTextureDirty();

        if (shadowRenderer) shadowRenderer.SetMaterialDirty();
    }

    protected override void OnDisable()
    {
        Canvas.willRenderCanvases -= OnWillRenderCanvas;
        TerminateInvalidator();

        if (shadowRenderer) shadowRenderer.gameObject.SetActive(false);
    }

    protected override void OnDestroy()
    {
        ShadowSorter.Instance.UnRegister(this);
        if (shadowRenderer) shadowRenderer.Dispose();

        ShadowFactory.Instance.ReleaseContainer(shadowContainer);

#if UNITY_EDITOR
        UnityEditor.Undo.undoRedoPerformed -= ApplySerializedData;
#endif
    }

    bool ShouldPerformWorks()
    {
        bool areCanvasRenderersCulled = CanvasRenderer && CanvasRenderer.cull &&
                                        shadowRenderer.CanvasRenderer && shadowRenderer.CanvasRenderer.cull;
        return isActiveAndEnabled && !areCanvasRenderersCulled;
    }

    void LateUpdate()
    {
        if (!ShouldPerformWorks())
            return;

        CheckTransformDirtied();
    }

    public void Rebuild(CanvasUpdate executing)
    {
        // Debug.Assert(true, "This should not be called in child mode");
        if (!ShouldPerformWorks()) return;

        if (executing == CanvasUpdate.PostLayout)
        {
            if (layoutDirty)
            {
                shadowRenderer.ReLayout();
                layoutDirty = false;
            }
        }
    }

    void OnWillRenderCanvas()
    {
#if LETAI_TRUESHADOW_DEBUG
        if (alwaysRender) textureDirty = true;
#endif

        if (!ShouldPerformWorks()) return;

        if (textureDirty)
        {
            if (!Baked)
            {
                ShadowFactory.Instance.Get(new ShadowRenderingRequest(this), ref shadowContainer);
                shadowRenderer.SetTexture(shadowContainer?.Texture);
            }
            else
            {
                if (bakedShadows != null && bakedShadows.Count > 0)
                    shadowRenderer.SetSprite(bakedShadows[0]);
            }

            textureDirty = false;
        }

        if (!shadowAsSibling)
        {
            if (shadowRenderer.transform.parent != transform)
                shadowRenderer.transform.SetParent(RectTransform, true);

            if (shadowRenderer.transform.GetSiblingIndex() != 0)
                shadowRenderer.transform.SetSiblingIndex(0);

            UnSetHierachyDirty();

            if (layoutDirty)
            {
                shadowRenderer.ReLayout();
                layoutDirty = false;
            }
        }
    }

    public void LayoutComplete() { }

    public void GraphicUpdateComplete() { }

    public void SetTextureDirty()
    {
        textureDirty = true;
        unchecked
        {
            textureRevision++;
        }
    }

    public void SetLayoutDirty()
    {
        layoutDirty = true;
    }

    public void SetHierachyDirty()
    {
        hierachyDirty = true;
    }

    internal void UnSetHierachyDirty()
    {
        hierachyDirty = false;
    }
}
}
