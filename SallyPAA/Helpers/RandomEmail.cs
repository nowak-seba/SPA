namespace SallyPAA.Helpers
{
    public static class RandomEmail
    {
        public static string GenerateEmail(string domain, int length)
        {
            return RandomString.GenerateRandomString(length).ToLowerInvariant() + "@" + domain;
        }
    }
}
