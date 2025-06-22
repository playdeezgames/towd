Imports towd.data

Friend Class CookingFireRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.CookingFire, 1)
        SetInput(ItemType.Rock, 8)
        SetInput(ItemType.Stick, 8)
        SetRequiredLocation(LocationType.Dirt)
        SetRequiredLocation(LocationType.Grass)
        SetLocationTypeOutput(LocationType.CookingFire)
    End Sub
End Class
