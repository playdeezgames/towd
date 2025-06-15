Friend Class AddFuelVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.AddFuel, "Add Fuel", 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return False
    End Function

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return 0
    End Function
End Class
