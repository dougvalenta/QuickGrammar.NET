// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Text;
namespace QuickGrammar
{
    public abstract class Rule
    {

        class EmptyRule : Rule
        {

            public override void Evaluate(IRuleContext context, StringBuilder builder)
            {
                // NOP
            }

        }
        static readonly Rule EMPTY = new EmptyRule();

        public static Rule Empty
        {
            get
            {
                return EMPTY;
            }
        }

        public virtual string Evaluate(IRuleContext context)
        {
            StringBuilder builder = new StringBuilder();
            Evaluate(context, builder);
            return builder.ToString();
        }

        public abstract void Evaluate(IRuleContext context, StringBuilder builder);

    }
}
