using Castle.DynamicProxy;
using Microsoft.CSharp.RuntimeBinder;

namespace ExpressionFunction
{
  public static class BuilderExtension
  {

    internal static T Unwrap<T>(this T proxy)
    {
      if (!ProxyUtil.IsProxy(proxy))
        return proxy;

      try
      {
        dynamic dynamicProxy = proxy;
        return dynamicProxy.__target;
      }
      catch (RuntimeBinderException)
      {
        return proxy;
      }
    }
  }
}
