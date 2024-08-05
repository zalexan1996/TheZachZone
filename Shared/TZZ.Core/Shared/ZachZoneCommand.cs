namespace TZZ.Core.Shared;

public class ZachZoneCommand
{
    public IDictionary<string, string> Infos { get; set; } = new Dictionary<string, string>();
    public IDictionary<string, string> Warnings { get; set; } = new Dictionary<string, string>();
    public IDictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    public virtual bool IsValid => !Errors.Any();

    public static ZachZoneCommand Failure(string key, string value) => new ZachZoneCommand()
    {
        Errors = {
            { key, value }
        }
    };
    public static ZachZoneCommand<TResult> Failure<TResult>(string key, string value) => new ZachZoneCommand<TResult>(default)
    {
        Errors = {
            { key, value }
        }
    };

    public static ZachZoneCommand<TResult> Failure<TResult>(IDictionary<string, string> errors) => new ZachZoneCommand<TResult>(default)
    {
        Errors = errors
    };

    public static ZachZoneCommand Success() => new ZachZoneCommand();
    public static ZachZoneCommand<TResult> Success<TResult>(TResult result) => new ZachZoneCommand<TResult>(result);
}

public class ZachZoneCommand<TResult> : ZachZoneCommand
{
    public ZachZoneCommand(TResult result)
    {
        Result = result;
    }

    public TResult? Result { get; set; }
    public override bool IsValid => Result is not null && base.IsValid;
}