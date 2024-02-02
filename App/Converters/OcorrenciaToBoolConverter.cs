using System;
using System.Globalization;
using App.Models;

namespace App.Converters;

public class OcorrenciaToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var ocorrencia = parameter as OcorrenciaModel;
        var ocorrenciaSelecionada = value as OcorrenciaModel;
        return ocorrencia != null
            && ocorrenciaSelecionada != null
            && ocorrencia.Id == ocorrenciaSelecionada.Id;
    }

    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        if (value is bool boolValue && boolValue)
        {
            return parameter;
        }
        return null;
    }
}
