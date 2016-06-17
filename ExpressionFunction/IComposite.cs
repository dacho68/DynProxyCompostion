using Newtonsoft.Json;

namespace ExpressionFunction
{
  public interface IComposite
  {
    [JsonIgnore]
    IComposite Parent { get; set; }
  }
}
