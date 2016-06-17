using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ExpressionFunction
{
  public class Feet :  IComposite
  {
    public virtual int Color { get; set; }

    [JsonIgnore]
    public IComposite Parent { get; set; }
  }
}
