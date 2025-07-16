Imports towd.data

Friend Class ForageDeedDescriptor
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
            "forage",
            business.StatisticType.ForagingCounter,
            requiredCount,
            xp,
            needed)
    End Sub
End Class
