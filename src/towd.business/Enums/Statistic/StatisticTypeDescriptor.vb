Imports towd.data

Friend Class StatisticTypeDescriptor
    Implements IStatisticType
    Sub New(statisticType As StatisticType, name As String)
        Me.StatisticType = statisticType
        Me.Name = name
    End Sub

    Public ReadOnly Property StatisticType As StatisticType Implements IStatisticType.StatisticType

    Public ReadOnly Property Name As String Implements IStatisticType.Name
End Class
