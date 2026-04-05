using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Client.Domain.SceneGraph;
using Ambient.Plugins.Properties;
using SkiaSharp;

namespace Ambient.Plugins;

/// <summary>
/// The paint top plugin.
/// </summary>
public class DrawRoof : IPlugin
{
    // *********************************************************************************
    #region Explicit Interface Properties

    /// <summary>
    /// Gets the plug-in category.
    /// </summary>
    PluginCategory IPlugin.Category
    {
        get
        {
            return PluginCategory.Shape;
        }
    }

    /// <summary>
    /// Gets the description of the plug-in.
    /// </summary>
    string IPlugin.Description
    {
        get
        {
            return "Draw a roof.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.DrawRoof32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Draw Roof";
        }
    }

    #endregion

    #region Explicit Interface Methods

    /// <summary>
    /// Implements application of the plug-in
    /// </summary>
    /// <param name="world">
    /// The world.
    /// </param>
    /// <param name="editorState">
    /// The current editor settings.
    /// </param>
    public void Apply(VoxelWorld world, EditorState editorState)
    {
        // the size of shape to draw is in the editorStateSettings as StackMinimum, and StackMaximum
        int roofHeight = 0;
        for (int y = (int)editorState.StackMinimum.Y; y < editorState.StackMaximum.Y; y++)
        {
            for (int x = (int)editorState.StackMinimum.X + roofHeight - 1; x < editorState.StackMaximum.X - roofHeight + 1; x++)
            {
                for (int z = (int)editorState.StackMinimum.Z + roofHeight - 1; z < editorState.StackMaximum.Z - roofHeight + 1; z++)
                {
                    // this condition makes the roof hollow
                    if ((x == editorState.StackMinimum.X + roofHeight - 1) || (x == editorState.StackMaximum.X - roofHeight) || (z == editorState.StackMinimum.Z + roofHeight - 1) || (z == editorState.StackMaximum.Z - roofHeight))
                    {
                        var voxel = new Voxel3(x, y, z);
                        world.SetVoxel(voxel, BlockPattern.GetBlockFromPattern(voxel, editorState.Pattern));
                    }
                }
            }

            roofHeight++;
        }
    }

    #endregion

}