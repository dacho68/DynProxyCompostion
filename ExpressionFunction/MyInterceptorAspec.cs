using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;

namespace ExpressionFunction
{
  public class MyInterceptorAspect : IInterceptor
  {
    public void Intercept(IInvocation invocation)
    {
      var methodName = invocation.Method.Name;
      Console.WriteLine("Before {0}", methodName);
      invocation.Proceed();
      Console.WriteLine("After {0}", methodName);
    }
  }

  //static public class Extension
  //{
  //  public static PropertyInfo GetProperty(this MethodInfo method)
  //  {
  //    bool takesArg = method.GetParameters().Length == 1;
  //    bool hasReturn = method.ReturnType != typeof(void);
  //    if (takesArg == hasReturn) return null;
  //    if (takesArg)
  //    {
  //      return method.DeclaringType.GetProperties()
  //                   .Where(prop => prop.GetSetMethod() == method).FirstOrDefault();
  //    }
  //    else
  //    {
  //      return method.DeclaringType.GetProperties()
  //                   .Where(prop => prop.GetGetMethod() == method).FirstOrDefault();
  //    }
  //  }
  //}

}
