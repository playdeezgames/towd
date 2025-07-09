Imports towd.data

Friend Class StatisticTypeDescriptor
    Implements IStatisticType
    Sub New(
           statisticType As String,
           name As String,
           description As String)
        Me.StatisticType = statisticType
        Me.Name = name
        Me.Description = description
    End Sub

    Public ReadOnly Property StatisticType As String Implements IStatisticType.StatisticType

    Public ReadOnly Property Name As String Implements IStatisticType.Name

    Public ReadOnly Property Description As String Implements IStatisticType.Description
End Class
