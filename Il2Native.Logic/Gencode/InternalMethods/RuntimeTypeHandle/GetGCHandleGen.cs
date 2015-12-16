﻿namespace Il2Native.Logic.Gencode.InternalMethods.RuntimeTypeHandler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PEAssemblyReader;
    using SynthesizedMethods;

    public static class GetGCHandleGen
    {
        public static readonly string Name = "System.IntPtr System.RuntimeTypeHandle.GetGCHandle(System.RuntimeTypeHandle, System.Runtime.InteropServices.GCHandleType)";

        public static IEnumerable<Tuple<string, Func<IMethod, IMethod>>> Generate(ICodeWriter codeWriter)
        {
            var ilCodeBuilder = new IlCodeBuilder();

            var gcHandleType = codeWriter.ResolveType("System.Runtime.InteropServices.GCHandle");
            var constructor = Logic.IlReader.Constructors(
                gcHandleType,
                codeWriter).First(c => c.GetParameters().Count() == 2);

            ilCodeBuilder.LoadNull();
            ilCodeBuilder.LoadArgument(1);
            ilCodeBuilder.New(constructor);

            ilCodeBuilder.CallDirect(OpCodeExtensions.GetFirstMethodByName(gcHandleType, "ToIntPtr", codeWriter));

            ilCodeBuilder.Add(Code.Ret);

            yield return ilCodeBuilder.Register(Name, codeWriter);
        }
    }
}