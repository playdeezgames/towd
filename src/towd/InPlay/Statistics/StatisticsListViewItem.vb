Imports System.Text
Imports towd.business

Public Class StatisticsListViewItem
    Public ReadOnly Property StatisticType As IStatisticType
    Private ReadOnly character As ICharacter
    Sub New(character As ICharacter, statisticType As IStatisticType)
        Me.statisticType = statisticType
        Me.character = character
    End Sub
    Public Overrides Function ToString() As String
        Dim builder As New StringBuilder
        builder.Append($"{statisticType.Name}: {character.GetStatistic(statisticType.StatisticType)}")
        Dim maximum = character.GetStatisticMaximum(statisticType.StatisticType)
        If maximum < Integer.MaxValue Then
            builder.Append($"/{maximum}")
        End If
        Return builder.ToString
    End Function
End Class
