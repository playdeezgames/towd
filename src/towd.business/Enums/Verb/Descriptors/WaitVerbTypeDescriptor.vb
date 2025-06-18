Friend Class WaitVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Wait, "Wait", 1, "Bide your time in the Wastes. 
Rest to recover health or wait for better odds, but danger lurks. 
Every moment tests your patience and luck.")
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
