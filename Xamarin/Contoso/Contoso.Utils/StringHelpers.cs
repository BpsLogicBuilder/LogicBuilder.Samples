namespace Contoso.Utils
{
    public static class StringHelpers
    {
        public static string NullString = null;

        public static bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
