Imports towd.data

Friend Class KnifeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Knife, 1)
        SetInput(ItemType.Blade, 1)
        SetInput(ItemType.Twine, 1)
        SetInput(ItemType.Stick, 1)
        SetItemTypeOutput(ItemType.Knife, 1)
    End Sub
End Class
