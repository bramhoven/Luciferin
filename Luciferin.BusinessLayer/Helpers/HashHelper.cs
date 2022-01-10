namespace Luciferin.BusinessLayer.Helpers
{
    public static class HashHelper
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Calculates a Knuth hash from a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static ulong CalculateKnuthHash(string input)
        {
            var hashedValue = 3074457345618258791ul;
            foreach (var c in input)
            {
                hashedValue += c;
                hashedValue *= 3074457345618258799ul;
            }

            return hashedValue;
        }

        #endregion

        #endregion
    }
}