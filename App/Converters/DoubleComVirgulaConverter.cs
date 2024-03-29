﻿using Newtonsoft.Json;
using System;
using System.Globalization;

namespace App.Converters;

public class DoubleComVirgulaConverter : JsonConverter<double?>
{
    public override double? ReadJson(
        JsonReader reader,
        Type objectType,
        double? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer
    )
    {
        string s = (string)reader.Value!;
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        try
        {
            return double.Parse(s.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
        catch
        {
            return 0;
        }
    }

    public override void WriteJson(JsonWriter writer, double? value, JsonSerializer serializer)
    {
        if (value.HasValue)
        {
            writer.WriteValue(value.Value.ToString(CultureInfo.InvariantCulture));
        }
        else
        {
            writer.WriteNull();
        }
    }
}
