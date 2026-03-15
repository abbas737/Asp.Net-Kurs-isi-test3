using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyJsonConverter : JsonConverter<DateTime>
{
    private readonly string _format;

    public DateOnlyJsonConverter(string format = "yyyy-MM-dd")
    {
        _format = format;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        return DateTime.Parse(str!); // lazım olsa TryParse istifadə edə bilərsən
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}
