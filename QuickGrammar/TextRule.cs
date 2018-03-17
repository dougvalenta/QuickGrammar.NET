// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    class TextRule : Rule
    {
        readonly string Text;

        internal TextRule(string text)
        {
            Text = text;
        }

        public override void Evaluate(IRuleContext context, StringBuilder builder)
        {
            builder.Append(Text);
        }
    }
}
