Friend Class CraftDeedDescriptor
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
            "craft",
            business.StatisticType.CraftCounter,
            requiredCount,
            xp,
            needed)
    End Sub
End Class
