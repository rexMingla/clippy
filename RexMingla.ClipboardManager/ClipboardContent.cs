﻿using System.Linq;
using System.Collections.Generic;

namespace RexMingla.ClipboardManager
{
    public sealed class ClipboardContent
    {
        public List<ClipboardData> Data;

        public override string ToString() => $"<ClipboardContent #items={Data.Count}>";

        public override bool Equals(object obj)
        {
            var other = obj as ClipboardContent;
            return other != null && Data.Count == other.Data.Count && Data.All(d => other.Data.FirstOrDefault(d2 => d.Equals(d2)) != null);
        }

        public override int GetHashCode()
        {
            return Data.Sum(d => d.GetHashCode());
        }
    }

    public sealed class ClipboardData
    {
        public string DataFormat { get; set; }
        public object Content { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ClipboardData;
            return other != null && string.Equals(DataFormat, other.DataFormat) && object.Equals(Content, other.Content);
        }

        public override int GetHashCode()
        {
            var hash = 19;
            hash += 31 + (DataFormat?.GetHashCode() ?? 0);
            hash += 31 + (Content?.GetHashCode() ?? 0);
            return hash;
        }
    }
}
