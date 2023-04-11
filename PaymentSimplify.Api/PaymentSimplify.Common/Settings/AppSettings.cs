namespace PaymentSimplify.Common.Settings;

public class AppSettings
{
    public SettingsDb SettingsDb { get; set; } = null!;
    public AuthenticatedSettings AuthenticatedSettings { get; set; } = null!;
    public string IdUserAdmin { get; set; } = null!;
}