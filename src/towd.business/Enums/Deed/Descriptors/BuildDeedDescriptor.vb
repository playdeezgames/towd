Friend Class BuildDeedDescriptor
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
            "build",
            business.StatisticType.BuildCounter,
            requiredCount,
            xp,
            needed)
    End Sub
End Class
