Friend Class MoveDeedDescriptor
    Inherits StatisticBasedDeedDescriptor

    Public Sub New(
                  deed As String,
                  name As String,
                  requiredCount As Integer,
                  xp As Integer,
                  needed() As String)
        MyBase.New(
            deed,
            name,
            "complete a move action",
            business.StatisticType.Steps,
            requiredCount,
            xp,
            needed)
    End Sub
End Class
