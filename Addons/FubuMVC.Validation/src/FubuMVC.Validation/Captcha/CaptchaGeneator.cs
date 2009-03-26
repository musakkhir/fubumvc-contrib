using System;
using System.Collections.Generic;
using FubuMVC.Core;

namespace FubuMVC.Validation.Captcha
{
    public class CaptchaGeneator
    {
        private readonly IList<CaptchaOpperator> _opperators;
        private readonly Random _random;

        public CaptchaGeneator()
        {
            _opperators = new List<CaptchaOpperator>();
            _random = new Random();
        }

        public CaptchaGeneator ConfigureToUse(CaptchaOpperator opperator)
        {
            _opperators.Add(opperator);
            return this;
        }

        public override string ToString()
        {
            if (_opperators.Count == 0)
                return string.Empty;

            var leftSide = _random.Next(10);
            var rightSide = _random.Next(10);

            var opperator = _opperators[0];
            if (_opperators.Count != 1)
                opperator = _opperators[_random.Next(_opperators.Count - 1)];

            return "{0} {1} {2}".ToFormat(leftSide, opperator.AsString(), rightSide);
        }
    }
}