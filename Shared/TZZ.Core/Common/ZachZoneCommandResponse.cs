namespace TZZ.Core.Common;

public class ZachZoneCommandResponse
{
  public IDictionary<string, string> Infos { get; init; } = new Dictionary<string, string>();
  public IDictionary<string, string> Warnings { get; init; } = new Dictionary<string, string>();
  public IDictionary<string, string> Errors { get; init; } = new Dictionary<string, string>();
  public virtual bool IsValid => !Errors.Any();

  public static ZachZoneCommandResponse Failure(string key, string value) => new ZachZoneCommandResponse()
  {
    Errors = {
      { key, value }
    }
  };

  public static ZachZoneCommandResponse<TResult> Failure<TResult>(string key, string value) where TResult : new()
    => new ZachZoneCommandResponse<TResult>(new())
    {
      Errors = {
      { key, value }
    }
    };

  public static ZachZoneCommandResponse<TResult> Failure<TResult>(IDictionary<string, string> errors) where TResult : new()
    => new ZachZoneCommandResponse<TResult>(new())
    {
      Errors = errors
    };
  public static ZachZoneCommandResponse Failure(IDictionary<string, string> errors)
    => new ZachZoneCommandResponse()
    {
      Errors = errors
    };

  public static ZachZoneCommandResponse Success() => new ZachZoneCommandResponse();
  public static ZachZoneCommandResponse<TResult> Success<TResult>(TResult result) => new ZachZoneCommandResponse<TResult>(result);
}

public class ZachZoneCommandResponse<TResult> : ZachZoneCommandResponse
{
  public ZachZoneCommandResponse()
  {

  }

  public ZachZoneCommandResponse(TResult result)
  {
    Result = result;
  }

  public TResult? Result { get; set; }
  public override bool IsValid => Result is not null && base.IsValid;
}