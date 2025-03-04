Imports towd.data

Friend Class BladeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Blade)
        SetInput(ItemType.SharpRock, 1)
        SetInput(ItemType.Hammer, 1)
        SetInputDurability(ItemType.Hammer, 1)
        SetOutput(ItemType.Hammer, 1)
        SetOutput(ItemType.Blade, 1)
    End Sub
End Class
