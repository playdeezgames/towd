Friend Class WaitVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Wait, "Wait", 1)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.AddMessage("You wait.")
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return Nothing
    End Function
End Class
