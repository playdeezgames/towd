Friend Class StatisticBasedDeedDescriptor
    Inherits DeedDescriptor
    Private ReadOnly requiredCount As Integer
    Private ReadOnly actionName As String
    Private ReadOnly statisticType As String

    Public Sub New(
                  deed As String,
                  name As String,
                  actionName As String,
                  statisticType As String,
                  requiredCount As Integer,
                  xp As Integer,
                  needed As String())
        MyBase.New(deed, name, xp, needed)
        Me.actionName = actionName
        Me.requiredCount = requiredCount
        Me.statisticType = statisticType
    End Sub

    Public Overrides ReadOnly Property Description As String
        Get
            Return $"Successfully {actionName} {requiredCount} times."
        End Get
    End Property

    Protected Overrides Sub OnDo(character As ICharacter)
    End Sub

    Public Overrides Function HasDone(character As ICharacter) As Boolean
        Return character.GetStatistic(statisticType) >= requiredCount
    End Function
End Class
