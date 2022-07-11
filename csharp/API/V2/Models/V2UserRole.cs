using System.ComponentModel;
using System.Text.Json.Serialization;

namespace API.V2;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum V2UserRole
{
    USR,
    SUP,
    ADM
}