Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Forage, "Forage")
    End Sub

    Public Overrides Sub Perform(character As ICharacter)
        character.AddMessage("You forage.")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return character.Location.GetStatistic(data.StatisticType.Foraging) > 0
    End Function
End Class
