using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.JScript;
using Convert=System.Convert;

namespace FubuMVC.Validation.Captcha
{
    public class Evaluator
    {
        private readonly Type _evaluatorType;
        private const string _jscriptSource = 
            @"package Evaluator
                {
                    class Evaluator
                    {
                        public function Eval(expr : String) : String 
                        { 
                            return eval(expr); 
                        }
                    }
                }";

        public Evaluator()
        {
            ICodeCompiler compiler = new JScriptCodeProvider().CreateCompiler();

            CompilerParameters parameters = new CompilerParameters {GenerateInMemory = true};

            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, _jscriptSource);
 
            Assembly assembly = results.CompiledAssembly;
            _evaluatorType = assembly.GetType("Evaluator.Evaluator");
        }

        public bool Validate(string statement)
        {
            try
            {
                object evaluator = Activator.CreateInstance(_evaluatorType);
                return Convert.ToBoolean(_evaluatorType.InvokeMember("Eval", BindingFlags.InvokeMethod, null, evaluator, new object[] { statement }));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}