Friend Class CraftVerbTypeDescriptor
    Inherits VerbTypeDescriptor

    Public Sub New()
        MyBase.New(VerbType.Craft, "Craft", 0)
    End Sub

    Protected Overrides Sub OnPerform(character As ICharacter)
        character.SetFlag(data.FlagType.CraftMenu, True)
    End Sub

    Public Overrides Function CanPerform(character As ICharacter) As Boolean
        Return RecipeTypes.Descriptors.Values.Any(Function(x) x.CanCraft(character))
    End Function

    Public Overrides Function GetPerformCount(character As ICharacter) As Integer?
        Return Nothing
    End Function
End Class
