using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Client.Domain.SceneGraph;
using Ambient.Plugins.Properties;
using SkiaSharp;

namespace Ambient.Plugins;

/// <summary>
/// The paint Oriented plugin.
/// </summary>
public class PaintOriented : IPlugin
{
    /// <summary>
    /// Gets the plug-in category.
    /// </summary>
    PluginCategory IPlugin.Category
    {
        get
        {
            return PluginCategory.Paint;
        }
    }

    /// <summary>
    /// Gets the description of the plug-in.
    /// </summary>
    string IPlugin.Description
    {
        get
        {
            return "Paint oriented along the block surface clicked. Use this tool when you want to paint the side of a wall with a wide brush.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.PaintOriented32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Paint Oriented";
        }
    }

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
        // the blocks to be painted are highlighted
        int highlightCount = world.GetHighlightCount();
        for (int blockId = 0; blockId < highlightCount; blockId++)
        {
            // get the highlighted block
            var highlightedItem = world.GetHighlightItem(blockId);

            // paint the block (but only if one exists where highlighted)
            if (world.GetVoxel(highlightedItem) != 0)
            {
                // paint the block using the current pattern
                // we'll flip x and z to make things more attractive when the orientation is along the X axis
                if (editorState.ApplyNormal.X != 0)
                {
                    world.SetVoxel(highlightedItem, BlockPattern.GetBlockFromPattern(new Voxel3(highlightedItem.Z, highlightedItem.Y, highlightedItem.X), editorState.Pattern));
                }
                else
                {
                    world.SetVoxel(highlightedItem, BlockPattern.GetBlockFromPattern(highlightedItem, editorState.Pattern));
                }
            }
        }
    }
}