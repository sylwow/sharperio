using System.Text.Json.Serialization;

namespace CleanArchitecture.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CharacterClass
{
    Paladin,
    Hunter,
    Warior,
    Mage
}
