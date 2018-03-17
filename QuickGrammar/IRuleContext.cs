// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
namespace QuickGrammar
{
    public interface IRuleContext : IGrammar
    {

        bool Optional();

        int Select(int count);

    }
}
