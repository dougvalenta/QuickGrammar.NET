// Copyright 2018 Doug Valenta
// Licensed under the terms of the MIT License.
using System;
namespace QuickGrammar
{
    public interface IGrammar
    {

        Rule this[string name] { get; set; }

    }
}
