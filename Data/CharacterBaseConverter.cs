using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using W04.Models;

namespace W04.Data
{
    internal class CharacterBaseConverter: JsonConverter<CharacterBase>
    {
        public override CharacterBase Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                var typeProperty = root.GetProperty("$type").GetString();
                Type type = typeProperty switch
                {
                    "W04.Models.Player" => typeof(Player),
                    "W04.Models.Archer" => typeof(Archer),
                    "W04.Models.Dragon" => typeof(Dragon),
                    "W04.Models.Ghost" => typeof(Ghost),
                    "W04.Models.Goblin" => typeof(Goblin),
                    "W04.Models.Wizard" => typeof(Wizard),
                    "W04.Models.Rogue" => typeof(Rogue),
                    "W04.Models.Healer" => typeof(Healer),
                    _ => throw new NotImplementedException(),

                    // _ => throw new NotSupportedException($"Type {typeProperty} is not supported")
                };
                return (CharacterBase?)JsonSerializer.Deserialize(root.GetRawText(), type, options)
                ?? throw new JsonException("Failed to deserialize character.");
            }
        }

        public override void Write(Utf8JsonWriter writer, CharacterBase value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }
    }
}
