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
            _ => new GuildJoinRequestFormUnknownResponse(jsonModel)
        };
    }
}

public sealed class GuildJoinRequestFormTermsResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public bool Response => (bool)jsonModel.Response;
}

public sealed class GuildJoinRequestFormTextResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public string Response => (string)jsonModel.Response;
}

public sealed class GuildJoinRequestFormMultipleChoiceResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public int Response => (int)jsonModel.Response;
}

public sealed class GuildJoinRequestFormUnknownResponse(JsonModels.JsonGuildJoinRequestFormResponse jsonModel)
    : GuildJoinRequestFormResponse(jsonModel)
{
    public object Response => jsonModel.Response;
}
