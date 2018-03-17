// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
namespace QuickGrammar
{
    public interface IRuleCompiler
    {
        Rule Compile(string ruletext);
    }
}
