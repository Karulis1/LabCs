﻿public abstract class Token
{
    public string Value { get; set; }

    protected Token(string value)
    {
        Value = value;
    }
}