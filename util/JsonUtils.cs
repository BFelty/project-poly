using System.Text.Json;

namespace LastPolygon.Util;

public static class JsonUtils
{
	private static readonly JsonSerializerOptions _options = new()
	{
		PropertyNameCaseInsensitive = true,
	};

	public static T Deserialize<T>(string json)
	{
		return JsonSerializer.Deserialize<T>(json, _options);
	}
}
