Friend Class ForageVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Forage, "Forage")
    End Sub

    Public Overrides Sub Perform(character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return False
    End Function
End Class
