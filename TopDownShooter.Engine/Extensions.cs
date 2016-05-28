// <copyright file="Extensions.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace TopDownShooter.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Container for extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Remove all items from this enumerable that match the given <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TType">Type of enumerable</typeparam>
        /// <param name="list">Enumerable reference</param>
        /// <param name="predicate">Remove all that match this predicate</param>
        public static void RemoveAll<TType>(this IList<TType> list, Func<TType, bool> predicate)
        {
            var items = from item in list
                        where predicate(item)
                        select item;

            foreach (var item in items)
            {
                list.Remove(item);
            }
        }
    }
}
