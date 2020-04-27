namespace WebApi.Helpers.Token
{
    public class TokenConfiguration
    {
        public readonly int LifeTime;
        public readonly string Audience;
        public readonly string Issuer;
        public readonly string TokenKey;

        public TokenConfiguration(int lifeTime, string audience, string issuer, string tokenKey)
        {
            LifeTime = lifeTime;
            Audience = audience;
            Issuer = issuer;
            TokenKey = tokenKey;
        }
    }
}