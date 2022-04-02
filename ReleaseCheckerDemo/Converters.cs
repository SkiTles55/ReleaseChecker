﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace ReleaseCheckerDemo
{
    public class FileSizeConverter : IValueConverter
    {
        private readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            if (value is not long size) return null;
            return SizeConverter(size);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();

        private string SizeConverter(long value)
        {
            if (value < 0) { return "-" + SizeConverter(-value); }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue / 1024) >= 1)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0:n1} {1}", dValue, SizeSuffixes[i]);
        }
    }
}