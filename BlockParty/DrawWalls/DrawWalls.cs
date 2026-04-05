using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Client.Domain.SceneGraph;
using Ambient.Plugins.Properties;
using SkiaSharp;

namespace Ambient.Plugins;

/// <summary>
/// The paint top plugin.
/// </summary>
public class DrawWalls : IPlugin
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
            return "Draw walls for a building.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.DrawWalls32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Draw Walls";
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

        // the size of shape to draw is in the AvantiSettings as StackMinimum, and StackMaximum

        for (int x = (int)editorState.StackMinimum.X; x < editorState.StackMaximum.X; x++)
        {
            for (int y = (int)editorState.StackMinimum.Y; y < editorState.StackMaximum.Y; y++)
            {
                for (int z = (int)editorState.StackMinimum.Z; z < editorState.StackMaximum.Z; z++)
                {
                    if (x == editorState.StackMinimum.X || x == editorState.StackMaximum.X - 1 || z == editorState.StackMinimum.Z || z == editorState.StackMaximum.Z - 1)
                    {
                        var voxel = new Voxel3(x, y, z);
                        world.SetVoxel(voxel, BlockPattern.GetBlockFromPattern(voxel, editorState.Pattern));
                    }
                }
            }
        }
    }


    #endregion

}