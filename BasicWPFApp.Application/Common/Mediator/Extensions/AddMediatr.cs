using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.Reflection;

namespace BasicWPFApp.Application;

internal static class AddMediatrHelper
{
    public static void AddMediatr(this IKernel kernel, Assembly assembly)
    {
        kernel.Bind<IServiceProvider>().ToMethod(ctx => ctx.Kernel);
        kernel.Bind<IMediator>().To<Mediator>();

        var config = new MediatRServiceConfiguration();
        var assembliesToScan = new List<Assembly>() { assembly };

        ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>), kernel, assembliesToScan, false, config);
        ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>), kernel, assembliesToScan, false, config);
        ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>), kernel, assembliesToScan, true, config);
        ConnectImplementationsToTypesClosing(typeof(IStreamRequestHandler<,>), kernel, assembliesToScan, false, config);
        ConnectImplementationsToTypesClosing(typeof(IRequestExceptionHandler<,,>), kernel, assembliesToScan, true, config);
        ConnectImplementationsToTypesClosing(typeof(IRequestExceptionAction<,>), kernel, assembliesToScan, true, config);

        ConnectImplementationsToTypesClosing(typeof(IRequestPreProcessor<>), kernel, assembliesToScan, true, config);
        ConnectImplementationsToTypesClosing(typeof(IRequestPostProcessor<,>), kernel, assembliesToScan, true, config);

        var multiOpenInterfaces = new List<Type>
        {
            typeof(INotificationHandler<>),
            typeof(IRequestExceptionHandler<,,>),
            typeof(IRequestExceptionAction<,>),
            typeof(IRequestPreProcessor<>),
            typeof(IRequestPostProcessor<,>)
        };

        foreach (var multiOpenInterface in multiOpenInterfaces)
        {
            var arity = multiOpenInterface.GetGenericArguments().Length;

            var concretions = assembliesToScan
                .SelectMany(a => a.DefinedTypes)
                .Where(type => type.FindInterfacesThatClose(multiOpenInterface).Any())
                .Where(type => type.IsConcrete() && type.IsOpenGeneric())
                .Where(type => type.GetGenericArguments().Length == arity)
                .Where(config.TypeEvaluator)
                .ToList();

            foreach (var type in concretions)
            {
                //services.AddTransient(multiOpenInterface, type);
                kernel.Bind(multiOpenInterface).To(type).InTransientScope();
            }
        }
    }

    private static void ConnectImplementationsToTypesClosing(Type openRequestInterface,
        IKernel kernel,
        IEnumerable<Assembly> assembliesToScan,
        bool addIfAlreadyExists,
        MediatRServiceConfiguration configuration)
    {
        var concretions = new List<Type>();
        var interfaces = new List<Type>();
        foreach (var type in assembliesToScan.SelectMany(a => a.DefinedTypes).Where(t => !t.IsOpenGeneric()).Where(configuration.TypeEvaluator))
        {
            var interfaceTypes = type.FindInterfacesThatClose(openRequestInterface).ToArray();
            if (!interfaceTypes.Any()) continue;

            if (type.IsConcrete())
            {
                concretions.Add(type);
            }

            foreach (var interfaceType in interfaceTypes)
            {
                interfaces.Fill(interfaceType);
            }
        }

        foreach (var @interface in interfaces)
        {
            var exactMatches = concretions.Where(x => x.CanBeCastTo(@interface)).ToList();
            if (addIfAlreadyExists)
            {
                foreach (var type in exactMatches)
                {
                    //services.AddTransient(@interface, type);
                    kernel.Bind(@interface).To(type).InTransientScope();
                }
            }
            else
            {
                if (exactMatches.Count > 1)
                {
                    exactMatches.RemoveAll(m => !IsMatchingWithInterface(m, @interface));
                }

                foreach (var type in exactMatches)
                {
                    //services.TryAddTransient(@interface, type);
                    kernel.Bind(@interface).To(type).InTransientScope();
                }
            }

            if (!@interface.IsOpenGeneric())
            {
                AddConcretionsThatCouldBeClosed(@interface, concretions, kernel);
            }
        }
    }

    private static bool IsMatchingWithInterface(Type? handlerType, Type handlerInterface)
    {
        if (handlerType == null || handlerInterface == null)
        {
            return false;
        }

        if (handlerType.IsInterface)
        {
            if (handlerType.GenericTypeArguments.SequenceEqual(handlerInterface.GenericTypeArguments))
            {
                return true;
            }
        }
        else
        {
            return IsMatchingWithInterface(handlerType.GetInterface(handlerInterface.Name), handlerInterface);
        }

        return false;
    }

    private static void AddConcretionsThatCouldBeClosed(Type @interface, List<Type> concretions, IKernel kernel)
    {
        foreach (var type in concretions
                     .Where(x => x.IsOpenGeneric() && x.CouldCloseTo(@interface)))
        {
            try
            {
                //services.TryAddTransient(@interface, type.MakeGenericType(@interface.GenericTypeArguments));
                kernel.Bind(@interface, type.MakeGenericType(@interface.GenericTypeArguments));
            }
            catch (Exception)
            {
            }
        }
    }

    internal static bool CouldCloseTo(this Type openConcretion, Type closedInterface)
    {
        var openInterface = closedInterface.GetGenericTypeDefinition();
        var arguments = closedInterface.GenericTypeArguments;

        var concreteArguments = openConcretion.GenericTypeArguments;
        return arguments.Length == concreteArguments.Length && openConcretion.CanBeCastTo(openInterface);
    }

    private static bool CanBeCastTo(this Type pluggedType, Type pluginType)
    {
        if (pluggedType == null) return false;

        if (pluggedType == pluginType) return true;

        return pluginType.IsAssignableFrom(pluggedType);
    }

    private static bool IsOpenGeneric(this Type type)
    {
        return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
    }

    internal static IEnumerable<Type> FindInterfacesThatClose(this Type pluggedType, Type templateType)
    {
        return FindInterfacesThatClosesCore(pluggedType, templateType).Distinct();
    }

    private static IEnumerable<Type> FindInterfacesThatClosesCore(Type pluggedType, Type templateType)
    {
        if (pluggedType == null) yield break;

        if (!pluggedType.IsConcrete()) yield break;

        if (templateType.IsInterface)
        {
            foreach (
                var interfaceType in
                pluggedType.GetInterfaces()
                    .Where(type => type.IsGenericType && type.GetGenericTypeDefinition() == templateType))
            {
                yield return interfaceType;
            }
        }
        else if (pluggedType.BaseType!.IsGenericType &&
                 pluggedType.BaseType!.GetGenericTypeDefinition() == templateType)
        {
            yield return pluggedType.BaseType!;
        }

        if (pluggedType.BaseType == typeof(object)) yield break;

        foreach (var interfaceType in FindInterfacesThatClosesCore(pluggedType.BaseType!, templateType))
        {
            yield return interfaceType;
        }
    }

    private static bool IsConcrete(this Type type)
    {
        return !type.IsAbstract && !type.IsInterface;
    }

    private static void Fill<T>(this IList<T> list, T value)
    {
        if (list.Contains(value)) return;
        list.Add(value);
    }
}
