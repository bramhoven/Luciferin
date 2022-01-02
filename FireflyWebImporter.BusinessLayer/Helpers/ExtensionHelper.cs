using System;

namespace FireflyWebImporter.BusinessLayer.Helpers
{
    public static class ExtensionHelper
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Gets a consistent hash from a two valued ValueTuple.
        /// </summary>
        /// <param name="valueTuple">The value tuple.</param>
        /// <typeparam name="T1">Value 1.</typeparam>
        /// <typeparam name="T2">Value 2.</typeparam>
        /// <returns></returns>
        public static ulong GetConsistentHash<T1, T2>(this ValueTuple<T1, T2> valueTuple)
        {
            var (item1, item2) = valueTuple;
            var hashableString = $"{item1}{item2}";
            return HashHelper.CalculateKnuthHash(hashableString);
        }

        /// <summary>
        /// Gets a consistent hash from a three valued ValueTuple.
        /// </summary>
        /// <param name="valueTuple">The value tuple.</param>
        /// <typeparam name="T1">Value 1.</typeparam>
        /// <typeparam name="T2">Value 2.</typeparam>
        /// <typeparam name="T3">Value 3.</typeparam>
        /// <returns></returns>
        public static ulong GetConsistentHash<T1, T2, T3>(this ValueTuple<T1, T2, T3> valueTuple)
        {
            var (item1, item2, item3) = valueTuple;
            var hashableString = $"{item1}{item2}{item3}";
            return HashHelper.CalculateKnuthHash(hashableString);
        }

        #endregion

        #endregion
    }
}