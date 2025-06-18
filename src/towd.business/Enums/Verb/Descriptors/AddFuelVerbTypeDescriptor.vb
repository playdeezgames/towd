Friend Class AddFuelVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.AddFuel, "Add Fuel", 1, "Keep the fire alive. 
Toss wood or debris into your camp’s blaze to ward off the cold. 
More fuel, more warmth—run out, and the night claims you.")
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
