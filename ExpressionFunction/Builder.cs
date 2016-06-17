using System;
using System.Linq.Expressions;
using System.Reflection;
//using ExpressionTreeViewer;
using Castle.DynamicProxy;

namespace ExpressionFunction
{
  public class Builder<T> where T : class
  {
    private dynamic mProxy;
    private ProxyGenerator mProxyGenerator;
   
    public Builder()
    {
      mProxyGenerator = new ProxyGenerator();
      var wTarget = Activator.CreateInstance<T>();
      mProxy = mProxyGenerator.CreateClassProxyWithTarget<T>(wTarget, new MyInterceptorAspect());
    }


    private PropertyInfo GetProperty<U>(Expression<Func<T, U>> expression)
    {
      var lambda = (LambdaExpression)expression;

      if (lambda.Body.NodeType != ExpressionType.MemberAccess)
      {
        throw new InvalidOperationException("Expression must be a MemberExpression.");
      }
      var memberExpression = (MemberExpression)lambda.Body;
      var propertyInfo = memberExpression.Member as PropertyInfo;
      if (propertyInfo == null)
      {
        throw new InvalidOperationException("Expression must be a property reference.");
      }
      return propertyInfo;
    }

    public Builder<T> Bind<U>(Expression<Func<T, U>> expression, string iLabel)
    {
      var wPropery =   GetProperty(expression);
      return this;
    }

    public Builder<U> Compose<U>(Expression<Func<T, U[]>> expression, int iSize) where U : class
    {
      Type wType;
      var wPropety = GetProperty(expression);
      if (wPropety.PropertyType.IsArray)
      {
        wType = wPropety.PropertyType.GetElementType();
      }
      else
      {
        throw new InvalidOperationException("property must be array.");
      }

  
      var  wArrayInstances = Array.CreateInstance(wType, iSize);
      for (var i = 0; i < iSize; i++)
      {
        var wTargetItem = Activator.CreateInstance(wType);
        var wProxyItem = mProxyGenerator.CreateClassProxyWithTarget((U) wTargetItem, new MyInterceptorAspect());
        wArrayInstances.SetValue(wProxyItem, i);
      }
      wPropety.SetValue(Object, wArrayInstances);  
      var wRet = new Builder<U>();
      return wRet;
    }

    public Builder<U> Compose<U>(Expression<Func<T, U>> expression) where U : class
    {
      return new Builder<U>();
    }

    public Builder<U> Aggregate<U>(Expression<Func<T, U>> expression, int iSize) where U : class
    {
      return new Builder<U>();
    }

    public Builder<U> Aggregate<U>(Expression<Func<T, U[]>> expression, int iSize) where U : class
    {
      return new Builder<U>();
    }

    public T Proxy 
    {
      get { return mProxy; }
    }


    public T Object
    {
      get
      {
        return mProxy.__target;
      }
    }

    
  }
}
