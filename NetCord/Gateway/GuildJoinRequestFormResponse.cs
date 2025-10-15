using System.Text.Json;

namespace NetCord.Gateway;

public abstract class GuildJoinRequestFormResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel) : IJsonModel<JsonModels.JsonGuildJoinRequestFormResponse>
{
    JsonModels.JsonGuildJoinRequestFormResponse IJsonModel<JsonModels.JsonGuildJoinRequestFormResponse>.JsonModel => jsonModel;

    public GuildJoinRequestFormResponseFieldType FieldType => jsonModel.FieldType;
    public string Label => jsonModel.Label;
    public bool Required => jsonModel.Required;
    public object RawResponse => jsonModel.Response;
    public IReadOnlyList<string> Values => jsonModel.Values;

    public static GuildJoinRequestFormResponse Create(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    {
        return jsonModel.FieldType switch
        {
            GuildJoinRequestFormResponseFieldType.Terms => new GuildJoinRequestFormTermsResponse(jsonModel),
            GuildJoinRequestFormResponseFieldType.TextInput or GuildJoinRequestFormResponseFieldType.Paragraph => new GuildJoinRequestFormTextResponse(jsonModel),
            GuildJoinRequestFormResponseFieldType.MultipleChoice => new GuildJoinRequestFormMultipleChoiceResponse(jsonModel),
            _ => throw new NotImplementedException($"Field type {jsonModel.FieldType} is not implemented."),
        };
    }
}

public sealed class GuildJoinRequestFormTermsResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public bool Response
    {
        get
        {
            if (jsonModel.Response is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.True)
                return true;
            if (jsonModel.Response is JsonElement jsonElementFalse && jsonElementFalse.ValueKind == JsonValueKind.False)
                return false;
            throw new InvalidOperationException("Response is not a boolean.");
        }
    }
}

public sealed class GuildJoinRequestFormTextResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public string Response
    {
        get
        {
            if (jsonModel.Response is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.String)
                return jsonElement.GetString()!;
            throw new InvalidOperationException("Response is not a string.");
        }
    }
}

public sealed class GuildJoinRequestFormMultipleChoiceResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public int Response
    {
        get
        {
            if (jsonModel.Response is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.Number)
                return jsonElement.GetInt32();
            throw new InvalidOperationException("Response is not a number.");
        }
    }
}
