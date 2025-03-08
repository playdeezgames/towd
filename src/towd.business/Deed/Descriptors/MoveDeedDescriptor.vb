Imports towd.data

Friend Class MoveDeedDescriptor
    Inherits DeedDescriptor
    Private ReadOnly stepCount As Integer

    Public Sub New(deed As Deed, name As String, stepCount As Integer, needed As data.Deed())
        MyBase.New(deed, name, needed)
        Me.stepCount = stepCount
    End Sub

    Public Overrides ReadOnly Property Description As String
        Get
            Return $"Successfully complete a move action {stepCount} times."
        End Get
    End Property

    Protected Overrides Sub OnDo(character As ICharacter)
    End Sub

    Public Overrides Function IsAvailable(character As ICharacter) As Boolean
        Return MyBase.IsAvailable(character)
    End Function

    Public Overrides Function HasDone(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.Steps) >= stepCount
    End Function
End Class
