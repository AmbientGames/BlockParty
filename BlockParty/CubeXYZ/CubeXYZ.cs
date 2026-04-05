using Ambient.Client.Domain.Plugins;

namespace Ambient.Plugins;

/// <summary>
/// The CubeXYZ plugin.
/// </summary>
public class CubeXYZ : IPluginImport
{
    // *********************************************************************************
    #region Explicit Interface Properties

    /// <summary>
    /// Gets the file type imported by this plug-in.
    /// </summary>
    string IPluginImport.FileType
    {
        get
        {
            return "xyz";
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
    /// <param name="importPath">
    /// The path of the file to be imported.
    /// </param>
    public void Import(VoxelWorld world, EditorState editorState, string importPath)
    {
        world.SetVoxel(new Voxel3(), 0); // air
    }

    #endregion
}