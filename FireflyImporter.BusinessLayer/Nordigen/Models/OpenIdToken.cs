using System;

namespace FireflyImporter.BusinessLayer.Nordigen.Models
{
    public class OpenIdToken
    {
        #region Properties

        public string AccessToken { get; set; }

        public TimeSpan AccessTokenExpires { get; set; }

        public DateTime Created { get; set; }

        public string RefreshToken { get; set; }

        public TimeSpan RefreshTokenExpires { get; set; }

        #endregion
    }
}