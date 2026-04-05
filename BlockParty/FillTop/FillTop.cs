using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Client.Domain.SceneGraph;
using Ambient.Plugins.Properties;
using SkiaSharp;

namespace Ambient.Plugins;

/// <summary>
/// The paint top plugin.
/// </summary>
public class FillTop : IPlugin
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
            return PluginCategory.Selection;
        }
    }

    /// <summary>
    /// Gets the description of the plug-in.
    /// </summary>
    string IPlugin.Description
    {
        get
        {
            return "Fills the top blocks of the current selection.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.FillTop32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Fill Top";
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
        // run through each selected voxel
        int selectionCount = world.GetSelectionCount();
        for (int blockId = 0; blockId < selectionCount; blockId++)
        {
            // get the selected voxel
            var selectedItem = world.GetSelectionItem(blockId);

            // ofsset by 1 vertically
            selectedItem.Y++;

            // if the voxel immediately above is air, then this is a "top" voxel
            if (world.GetVoxel(selectedItem) == 0)
            {
                world.SetVoxel(selectedItem, BlockPattern.GetBlockFromPattern(selectedItem, editorState.Pattern));
            }
        }
    }

    #endregion
}