namespace HarpenTech.Services.Theme;

// This interface defines a contract for managing the application theme, specifically setting the status bar color.
public interface ITheme
{
    /// <summary>
    /// Sets the color of the status bar and specifies whether to use a dark tint for the status bar content.
    /// </summary>
    /// <param name="color">The color to set for the status bar.</param>
    /// <param name="darkStatusBarTint">A boolean indicating whether to use a dark tint for the status bar content.</param>
    void SetStatusBarColor(Color color, bool darkStatusBarTint);
}
