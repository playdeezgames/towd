Public Interface IRecipeType
    ReadOnly Property RecipeType As RecipeType
    Function CanCraft(character As ICharacter) As Boolean
    Sub Craft(character As ICharacter)
    ReadOnly Property Name As String
End Interface
