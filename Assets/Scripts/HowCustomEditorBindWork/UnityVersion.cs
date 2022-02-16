using UnityEngine;

public static class UnityVersion
{
    static UnityVersion()
    {
        string[] array = Application.unityVersion.Split(new char[]
        {
            '.'
        });
        if (array.Length < 2)
        {
            Debug.LogError("Could not parse current Unity version '" + Application.unityVersion + "'; not enough version elements.");
            return;
        }
        if (!int.TryParse(array[0], out Major))
        {
            Debug.LogError(string.Concat(new string[]
            {
                "Could not parse major part '",
                array[0],
                "' of Unity version '",
                Application.unityVersion,
                "'."
            }));
        }
        if (!int.TryParse(array[1], out UnityVersion.Minor))
        {
            Debug.LogError(string.Concat(new string[]
            {
                "Could not parse minor part '",
                array[1],
                "' of Unity version '",
                Application.unityVersion,
                "'."
            }));
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnsureLoaded()
    {
    }

    /// <summary>
    /// Tests current Unity version is equal or greater.
    /// </summary>
    /// <param name="major">Minimum major version.</param>
    /// <param name="minor">Minimum minor version.</param>
    /// <returns><c>true</c> if the current Unity version is greater. Otherwise <c>false</c>.</returns>
    public static bool IsVersionOrGreater(int major, int minor)
    {
        return Major > major || Major == major && Minor >= minor;
    }

    /// <summary>
    /// The current Unity version major.
    /// </summary>
    public static readonly int Major;

    /// <summary>
    /// The current Unity version minor.
    /// </summary>
    public static readonly int Minor;
}