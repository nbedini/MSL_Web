namespace MCLauncher.WebUI.Entities
{
    public class Statics_WebUI
    {
        #region Auth

        public static string UsernameLogged { get; set; } = string.Empty;
        public static bool UserLogged { get; set; } = false;

        public static bool IsUserLogged()
        {
            return !string.IsNullOrEmpty(UsernameLogged) && UserLogged;
        }

        #endregion
    }
}