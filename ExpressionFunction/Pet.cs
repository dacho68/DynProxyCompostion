using System;
using Newtonsoft.Json;

namespace ExpressionFunction
{
  public interface IPet
  {
    string Name { get; set; }
    int Age { get; set; }
    string[] Friends { get; set; }
    Feet[] Foot { get; set; }
  }

  public class Pet : IComposite
  {
    public virtual string Name { get; set; }
    public virtual int Age { get; set; }
    public virtual string[] Friends { get; set; }
    public virtual Feet[] Foot { get; set; }

    public virtual void DoStuff()
    {
      Console.WriteLine("Inside of DoStuff()");
    }

    public virtual Head BigHead { get; set; }

    [JsonIgnore]
    public IComposite Parent { get; set; }
  }
}
