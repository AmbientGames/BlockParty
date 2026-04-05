using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Client.Domain.SceneGraph;
using Ambient.Plugins.Properties;
using SkiaSharp;

namespace Ambient.Plugins;

/// <summary>
/// The paint top plugin.
/// </summary>
public class DrawOriented : IPlugin
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
            return PluginCategory.Draw;
        }
    }

    /// <summary>
    /// Gets the description of the plug-in.
    /// </summary>
    string IPlugin.Description
    {
        get
        {
            return "Draw blocks oriented along the block surface clicked. Draws one layer at a time.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.DrawOriented32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Draw Oriented";
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
        // the blocks to be drawn are highlighted
        int highlightCount = world.GetHighlightCount();
        for (int blockId = 0; blockId < highlightCount; blockId++)
        {
            // get the highlighted block
            var highlightedBlock = world.GetHighlightItem(blockId);

            // draw the block (but only if it isn't there already)
            if (world.GetVoxel(highlightedBlock) == 0)
            {
                // draw the block using the current pattern
                // we'll flip x and z to make things more attractive when the orientation is along the X axis
                if (editorState.ApplyNormal.X != 0)
                {
                    world.SetVoxel(highlightedBlock, BlockPattern.GetBlockFromPattern(new Voxel3(highlightedBlock.Z, highlightedBlock.Y, highlightedBlock.X), editorState.Pattern));
                }
                else
                {
                    world.SetVoxel(highlightedBlock, BlockPattern.GetBlockFromPattern(highlightedBlock, editorState.Pattern));
                }
            }
        }

    }

    #endregion
}