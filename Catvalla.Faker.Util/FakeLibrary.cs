using Catvalla.String.Utils;
using System.Reflection;
using System.Reflection.Emit;

namespace Catvalla.Faker.Util
{
    /// <summary>
    /// This is a simple fake type and object creator. This is for occasions when you don't have
    /// an instance or type predefined and need to create it on the fly.
    /// </summary>
    public static class FakeLibrary
    {
        /// <summary>
        /// Builds the fake object with corrections. This will correct for things like Column names (COLUMN_NAME).
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="objectTypeName">Name of the object type.</param>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static Type BuildFakeTypeWithCorrections(string nameSpace, string objectTypeName, string[] properties)
        {
            List<string> correctedProperties = new List<string>();
            foreach (var property in properties)
            {
                correctedProperties.Add(property.ToPascalCase());
            }

            return BuildFakeType(nameSpace, objectTypeName, correctedProperties.ToArray());
        }

        /// <summary>
        /// Builds the fake type with corrections. And gets the values.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="objectTypeName">Name of the object type.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        public static Tuple<Type, object> BuildFakeTypeWithCorrections(string nameSpace, string objectTypeName, string[] properties, string[] values)
        {
            List<string> correctedProperties = new List<string>();
            foreach (var property in properties)
            {
                correctedProperties.Add(property.ToPascalCase());
            }

            return BuildFakeType(nameSpace, objectTypeName, correctedProperties.ToArray(), values);
        }

        /// <summary>
        /// Builds the type and preloads it with values.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="objectTypeName">Name of the object type.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Instance of {nameSpace}.{objectTypeName} cannot be created</exception>
        public static Tuple<Type, object> BuildFakeType(string nameSpace, string objectTypeName, string[] properties, string[] values)
        {
            var fakeType = BuildFakeType(nameSpace, objectTypeName, properties);

            var instance = Activator.CreateInstance(fakeType);

            if (instance != null)
            {
                for (int i = 0; i < properties.Length; i++)
                {
                    var propertyInfo = instance.GetType().GetProperty(properties[i]);

                    if (propertyInfo != null)
                    {
                        if (i < values.Length - 1)
                        {
                            propertyInfo.SetValue(instance, values[i]);
                        }
                        else
                        {
                            propertyInfo.SetValue(instance, values[values.Length - 1]);
                        }
                    }
                }

                return new Tuple<Type, object>(fakeType, instance);
            }
            else
            {
                throw new NullReferenceException($"Instance of {nameSpace}.{objectTypeName} cannot be created");
            }
        }

        /// <summary>
        /// Builds the fake object.
        /// see https://learn.microsoft.com/en-us/dotnet/api/system.reflection.emit.assemblybuilder?view=net-8.0 for reference
        /// </summary>
        /// <param name="nameSpace">The namespace.</param>
        /// <param name="objectTypeName">Name of the object type.</param>
        /// <param name="properties">The properties.</param>
        /// <returns></returns>
        public static Type BuildFakeType(string nameSpace, string objectTypeName, string[] properties)
        {
            AssemblyName assemblyName = new AssemblyName(nameSpace);
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder mb = ab.DefineDynamicModule(assemblyName?.Name);
            TypeBuilder myTypeBuilder = mb.DefineType(objectTypeName, TypeAttributes.Public);

            foreach (var prop in properties)
            {
                try
                {
                    FieldBuilder fieldBuilder = myTypeBuilder.DefineField("_" + prop,
                                                        typeof(string),
                                                        FieldAttributes.Private);

                    PropertyBuilder propertyBuilder = myTypeBuilder.DefineProperty(
                        prop,
                        PropertyAttributes.None,
                        typeof(string),
                        null);

                    MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                    MethodBuilder getPropertyMethodBuilder = myTypeBuilder.DefineMethod("get_" + prop,
                                       getSetAttr,
                                       typeof(string),
                                       Type.EmptyTypes);

                    ILGenerator getIL = getPropertyMethodBuilder.GetILGenerator();

                    getIL.Emit(OpCodes.Ldarg_0);
                    getIL.Emit(OpCodes.Ldfld, fieldBuilder);
                    getIL.Emit(OpCodes.Ret);

                    MethodBuilder setPropertyMethodBuilder = myTypeBuilder.DefineMethod("set_" + prop,
                                       getSetAttr,
                                       null,
                                       new Type[] { typeof(string) });

                    ILGenerator setIL = setPropertyMethodBuilder.GetILGenerator();

                    setIL.Emit(OpCodes.Ldarg_0);
                    setIL.Emit(OpCodes.Ldarg_1);
                    setIL.Emit(OpCodes.Stfld, fieldBuilder);
                    setIL.Emit(OpCodes.Ret);

                    propertyBuilder.SetGetMethod(getPropertyMethodBuilder);
                    propertyBuilder.SetSetMethod(setPropertyMethodBuilder);
                }
                catch
                {
                    throw;
                }
            }

            Type retType = myTypeBuilder.CreateType();

            return retType;
        }
    }
}
