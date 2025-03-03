Public Class RecipeTypeDescriptor
    Implements IRecipeType
    Sub New(recipeType As RecipeType)
        Me.RecipeType = recipeType
    End Sub
    Public ReadOnly Property RecipeType As RecipeType Implements IRecipeType.RecipeType
End Class
