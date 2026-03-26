using Pharmacy.Debugging;

namespace Pharmacy;

public class PharmacyConsts
{
    public const string LocalizationSourceName = "Pharmacy";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "ef0d3d7633d9435e9456417233f28958";
}
