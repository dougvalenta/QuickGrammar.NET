// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System.Collections.Generic;
namespace QuickGrammar
{
    public class Grammar : IGrammar
    {
        
        protected readonly Dictionary<string, Rule> Rules = new Dictionary<string, Rule>();

        public virtual Rule this[string name]
        {
            get
            {
                Rule rule;
                if (Rules.TryGetValue(name, out rule))
                {
                    return rule;
                }
                else
                {
                    return Rule.Empty;
                }
            }
            set
            {
                Rules[name] = value;
            }
        }

    }
}
