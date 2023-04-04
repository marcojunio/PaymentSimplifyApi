﻿using System.Text;
using PaymentSimplify.Application.Common.Interfaces;

namespace PaymentSimplify.Infra.File;

public class CsvBuilderFile : ICsvBuilderFile
{
    public byte[] GenerateCsv(IEnumerable<object> data,Encoding encoding)
    {
        var typeList = data.Select(f => f.GetType()).FirstOrDefault();

        if (typeList is null)
            return Array.Empty<byte>();

        var builderString = new StringBuilder();

        builderString.Append(WriteCsv(typeList,true));
        builderString.Append(WriteCsv(typeList));

        return encoding.GetBytes(builderString.ToString());;
    }

    private string WriteCsv(Type props,bool writeHeader = false)
    {
        var agregated = string.Empty;
        var separator = string.Empty;
        
        foreach (var prop in props.GetProperties())
        {
            agregated += separator + (writeHeader ? prop.Name : prop.GetValue(prop));
            separator = ";";
        }

        return agregated;
    }
    
}