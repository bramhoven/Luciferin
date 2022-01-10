using System;

namespace Luciferin.BusinessLayer.Firefly.Models
{
    public class FireflyTag
    {
        #region Properties

        public DateTime Created { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public long Latitude { get; set; }

        public double Longitude { get; set; }

        public string Tag { get; set; }

        public string Updated { get; set; }

        public int ZoomLevel { get; set; }

        #endregion
    }
}