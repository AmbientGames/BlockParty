using Ambient.Client.Domain.Enums;
using Ambient.Client.Domain.Plugins;
using Ambient.Plugins.Properties;
using SkiaSharp;
using System;

namespace Ambient.Plugins;

/// <summary>
/// The alternate plugin.
/// </summary>
public class Alternate : IPlugin
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
            return "Alternates Block 1 and Block 2 across the current selection.";
        }
    }

    /// <summary>
    /// Gets the icon for the plug-in.
    /// </summary>
    SKBitmap IPlugin.Icon
    {
        get
        {
            return SKBitmap.Decode(Resources.Alternate32);
        }
    }

    /// <summary>
    /// Gets the name of the plug-in.
    /// </summary>
    string IPlugin.Name
    {
        get
        {
            return "Alternate";
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
        // setup a pattern of blocks
        var blockPattern = new ushort[]
        {
            editorState.PrimaryBlock, editorState.SecondaryBlock, editorState.SecondaryBlock, editorState.PrimaryBlock, editorState.SecondaryBlock, editorState.PrimaryBlock, editorState.PrimaryBlock, editorState.SecondaryBlock
        };

        // run through each selected voxel
        int selectionCount = world.GetSelectionCount();
        for (int voxelId = 0; voxelId < selectionCount; voxelId++)
        {
            // get the selected voxel
            var selection = world.GetSelectionItem(voxelId);

            var voxelX = (int)selection.X;
            var voxelY = (int)selection.Y;
            var voxelZ = (int)selection.Z;

            // determine where this block is in the pattern
            int patternID = (Math.Abs(voxelZ % 2) * 4) + (Math.Abs(voxelY % 2) * 2) + (Math.Abs(voxelX) % 2);

            // set the block
            world.SetVoxel(selection, blockPattern[patternID]);
        }
    }

    #endregion
}