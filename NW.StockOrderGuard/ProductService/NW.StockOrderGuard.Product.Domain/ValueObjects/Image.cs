using NW.StockOrderGuard.SharedKernel;
using System;
using System.Text.RegularExpressions;

namespace NW.StockOrderGuard.Product.Domain.ValueObjects
{
    public class Image : IValueObject, IEquatable<Image>
    {
        public string Url { get; }

        public Image(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                Url = null;
                return;
            }
            if (!Regex.IsMatch(url, @"^https?:\/\/.+\.(jpg|jpeg|png|gif)$", RegexOptions.IgnoreCase))
                throw new ArgumentException("Invalid image URL format.");
            Url = url;
        }

        public override bool Equals(object obj) => Equals(obj as Image);

        public bool Equals(Image other) => other != null && Url == other.Url;

        public override int GetHashCode() => Url.GetHashCode();
    }
} 