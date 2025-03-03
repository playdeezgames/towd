Friend Class AddFuelVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.AddFuel, "Add Fuel")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return False
    End Function
End Class
