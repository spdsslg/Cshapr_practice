using System;
using System.Reflection;
using MathPlugin;
namespace Lab04_practice
{
    class Program
    {
        public static object? CreateSimpleValue(Type t)
        {
            if (t == typeof(int))
            {
                return 3;
            }

            if (t == typeof(double)) return 3.14;
            if(t == typeof(string)) return "Hello";

            return null;
        }
        
        public static object? Run(object obj, string methodName, params object[] args)
        {
            var type = obj.GetType();
            var argTypes = args.Select(a => a.GetType()).ToArray();
            var method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, types: argTypes);
            if (method == null)
            {
                throw new InvalidOperationException($"Method {methodName} not found");
            }
            var result = method?.Invoke(method.IsStatic?null:obj,args);
            return result;
        }

        public static void findInfo()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.GetCustomAttribute<InfoAttribute>() != null)
                .ToList();
            foreach (var type in types)
            {
                var a = Activator.CreateInstance(type);
                Console.WriteLine($"Author: {type.GetCustomAttribute<InfoAttribute>()?.Author}, genre: {type.GetProperty("Genre").GetValue(a)}");
            }
        }

        public static void findOperations()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods().Where(m => m.GetCustomAttribute<MathOperationAttribute>() != null);
                foreach (var method in methods)
                {
                    Console.WriteLine($"Method: {method.Name}");
                }
            }
        }

        public static void useMathPlugin()
        {
            // var baseDir = AppContext.BaseDirectory;
            // var path = Path.Combine(baseDir, "MathPlugin.dll");
            var asm = Assembly.Load("MathPlugin");
            var types = asm.GetTypes();
            var calculatorType = types.Where(t=>t.Name == "Calculator").First();
            var calculator = Activator.CreateInstance(calculatorType);
            var result = calculatorType.GetMethod("Add").Invoke(calculator, [2, 3]);
            Console.WriteLine($"using MathPlugin we get: 2+3={result}");
        }

        public static void allFunctions()
        {
            var asm = Assembly.Load("MathPlugin");
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                var methods = type.GetMethods().Where(m=>m.GetParameters().Length==2 && m.GetParameters()[0].ParameterType == typeof(int) &&  m.GetParameters()[1].ParameterType == typeof(int));
                foreach (var method in methods)
                {
                    var a = Activator.CreateInstance(type);
                    method.Invoke(a, [10, 5]);
                }
            }
        }

        public static void findWithPlugin()
        {
            var asm = Assembly.Load("MathPlugin");
            var types = asm.GetTypes().Where(t=>t.GetCustomAttribute<PluginAttribute>()!=null);
            foreach (var type in types)
            {
                var a = Activator.CreateInstance(type);
                var methods = type.GetMethods(BindingFlags.Public);
                foreach (var method in methods)
                {
                    method.GetParameters()
                }
            }
        }
        
        static void Main(string[] args)
        {
            var currAssem = Assembly.GetExecutingAssembly();
            var types = currAssem.GetTypes();
            foreach (var type in types)
            {
                Console.WriteLine($"Type: {type.Namespace}.{type.Name}");
                var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
                foreach (var method in methods)
                {
                    Console.WriteLine($"Method: {method.Name}");
                }
            }
            
            Console.WriteLine(new string('-', 20));
            
            var person_type =  typeof(Person);
            var person = Activator.CreateInstance(person_type);
            if (person_type.GetProperty("Name") != null)
            {
                person_type.GetProperty("Name").SetValue(person, "John Doe");
                person_type.GetProperty("Age").SetValue(person, 18);
                person_type.GetMethod("SayHello").Invoke(person, null);
            }
            
            Console.WriteLine(new string('-', 20));

            Run(person, "Work", ["manager"]);
            
            Console.WriteLine(new string('-', 20));
            
            findInfo();
            
            Console.WriteLine(new string('-', 20));
            
            findOperations();
            
            Console.WriteLine(new string('-', 20));
            
            useMathPlugin();
            
            Console.WriteLine(new string('-', 20));
            
            allFunctions();
        }
    }
}