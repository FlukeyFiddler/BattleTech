using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace nl.flukeyfiddler.Utils
{  /*
    * TODO revisit this when I have a better understanding of it
    
    public sealed class RuntimeInjector
    {
        // Singleton lazy instantiation
        private RuntimeInjector() { Init();  }

        public static RuntimeInjector Instance { get { return Nested.instance; } }
        private class Nested
        {
            static Nested() { }
            internal static readonly RuntimeInjector instance = new RuntimeInjector();
        }
        // End Singleton

        // k filePath v sourceCode 
        private Dictionary<string, string> sourceFiles;
        //private string[] sources;
        private CSharpCodeProvider compiler;
        private CompilerParameters parameters;
     

        private void Init()
        {
            sourceFiles = new Dictionary<string,string>();
            InitializeParameters();
            InitializeCompiler();
        }

        
        
        public void Inject(string filePath)
        {
            try
            {
                // new source file?
                if (!sourceFiles.ContainsKey(filePath)) { 
                    AddNewSourceCode(filePath);
                    CompileAndExecute();
                } else {
                    UpdateSourceCodeIfChanged(filePath);
                    CompileAndExecute();
                }
            } catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        private void AddNewSourceCode(string filePath)
        {
            string sourceCode = FileOps.GetFileAsString(filePath);
            Logger.LogLine("61 adding new sourcecode: " + sourceCode);
            sourceFiles[filePath] = sourceCode;
        }

        private void UpdateSourceCodeIfChanged(string filePath)
        {
            string newSourceCode = FileOps.GetFileAsString(filePath);
            Logger.LogLine("68 checking for changes");

            if (!newSourceCode.Equals(sourceFiles[filePath])) {
                Logger.LogLine("70 replacing sourcecode: " + newSourceCode);
                sourceFiles[filePath] = newSourceCode;
            }

        }

        private void CompileAndExecute()
        {

            // No try / catch as Mono will throw an exception that Google
            // stated can be ignored:
            //  An exception was thrown by the type initializer for Mono.CSharp.CSharpCodeCompiler
            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, sourceFiles.Values.ToArray());

            if (results.Errors.HasErrors)
            {
                foreach(CompilerError error in results.Errors)
                {
                    Logger.LogLine("line: " + error.Line);
                    Logger.LogLine("err. no.: " + error.ErrorNumber);
                    Logger.LogLine("err text" + error.ErrorText);
                    Logger.LogLine(error.ToString());
                }
            }
            else
            {
            Logger.LogLine("Success!");
                // results.CompiledAssembly.CreateInstance(Assembly.GetExecutingAssembly().GetName().ToString());
                //Assembly.Load(results.CompiledAssembly.GetName());//compiler.//results.CompiledAssembly.GetType().InvokeMember()
            }

            Logger.LogLine("hgmm");
            /*
            using (var ms = new MemoryStream())
            {
              
                EmitResult result = compiler.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Logger.LogLine(diagnostic.Id + ": " + diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Execute(assembly, type);
                }
             
        }

        /*
        private void Execute(Assembly assembly, Type type)
        {
            assembly.CreateInstance(type.ToString());
        }
        
       

        private void InitializeParameters()
        {
            Logger.LogLine("Line 115 initialize Parameters");
            
            parameters = new CompilerParameters
            {
                GenerateInMemory = true,
                
            };

            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().FullName);


            foreach (AssemblyName assemblyName in assemblyNames)
            {
                parameters.ReferencedAssemblies.Add(assemblyName.FullName);
            }
        }

        private void InitializeCompiler()
        {
            compiler = new CSharpCodeProvider();
        }
    }
*/
}
