using System;
using System.Diagnostics;
using Microsoft.VisualStudio.DebuggerVisualizers;
using Newtonsoft.Json;

namespace ExpressionFunction
{
  public class Program
  {
    static void Main(string[] args)
    {

      var wPetBuilder = new Builder<Pet>();

      wPetBuilder.Bind(p => p.Friends, "lab1")
                 .Bind(p => p.Age, "lab2");

      wPetBuilder.Compose(p => p.Foot, 3)
                 .Bind(f => f.Color, "lab3");

      wPetBuilder.Compose(p => p.BigHead).Bind(x => x.Color, "lab4");

      var wPetProxy = wPetBuilder.Proxy;

      wPetProxy.Age = 343;
      wPetProxy.Name = "toto";
      wPetProxy.Foot[0].Color = 1;
      wPetProxy.Foot[1].Color = 2;
      wPetProxy.Foot[2].Color = 4;

      var wToken = JsonConvert.SerializeObject(wPetProxy.Unwrap());

      Console.WriteLine(wToken.ToString());

    //   new VisualizerDevelopmentHost(queryable.Expression, typeof(ExpressionTreeVisualizer), typeof(ExpressionTreeObjectSource)).ShowVisualizer();
    }
  }
}
