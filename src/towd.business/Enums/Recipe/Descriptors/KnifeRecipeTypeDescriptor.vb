Imports towd.data

Friend Class KnifeRecipeTypeDescriptor
    Inherits RecipeTypeDescriptor

    Public Sub New()
        MyBase.New(RecipeType.Knife, 1)
        SetItemTypeInput(ItemType.Blade, 1)
        SetItemTypeInput(ItemType.Twine, 1)
        SetItemTypeInput(ItemType.Stick, 1)
        SetItemTypeOutputGenerator(ItemType.Knife, New FixedGenerator(1))
    End Sub
End Class
