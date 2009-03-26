using System;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.JScript;
using Convert=System.Convert;

namespace FubuMVC.Validation.Captcha
{
    public class CaptchaEvaluator
    {
        private readonly Type _evaluatorType;
        private const string _jscriptSource = 
            @"package CaptchaEvaluator
                {
                    class CaptchaEvaluator
                    {
                        public function Eval(expr : String) : String 
                        { 
                            return eval(expr); 
                        }
                    }
                }";

        public CaptchaEvaluator()
        {
            ICodeCompiler compiler = new JScriptCodeProvider().CreateCompiler();

            CompilerParameters parameters = new CompilerParameters {GenerateInMemory = true};

            CompilerResults results = compiler.CompileAssemblyFromSource(parameters, _jscriptSource);
 
            Assembly assembly = results.CompiledAssembly;
            _evaluatorType = assembly.GetType("CaptchaEvaluator.CaptchaEvaluator");
        }

        public bool IsValid(string statement)
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