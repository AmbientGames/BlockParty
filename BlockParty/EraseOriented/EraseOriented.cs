using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Plugins.Properties;
using SkiaSharp;

namespace Ambient.Plugins;

/// <summary>
/// The paint top plugin.
/// </summary>
public class EraseOriented : IPlugin
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
            return PluginCategory.Erase;
        }
    }

    /// <summary>
    /// Gets the description of the plug-in.
    /// </summary>
    string IPlugin.Description
    {
        get
        {
            return "Erase oriented along the block surface clicked. Erases blocks starting with the first layer clicked and forward, to avoid accidental drilling of holes.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.EraseOriented32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Erase Oriented";
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
        int x = 0;
        int y = 0;
        int z = 0;

        // the blocks to be erased are highlighted
        int highlightCount = world.GetHighlightCount();
        for (int blockId = 0; blockId < highlightCount; blockId++)
        {
            // get the highlighted block
            var highlightedItem = world.GetHighlightItem(blockId);

            // erase the block
            world.SetVoxel(highlightedItem, 0);
        }
    }

    #endregion
}