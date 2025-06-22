Friend Class CraftVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Craft, "Craft", 0, "Turn junk into treasure. 
Combine items from your pack to make tools or gear. 
A steady hand and some luck can turn twigs into a lifeline—fail, and you’re stuck with splinters.")
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.SetFlag(data.FlagType.CraftMenu, True)
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return True
    End Function

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return Nothing
    End Function
End Class
