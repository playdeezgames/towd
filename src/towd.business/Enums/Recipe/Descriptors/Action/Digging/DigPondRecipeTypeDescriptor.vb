Friend Class DigPondRecipeTypeDescriptor
    Inherits DigRecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            RecipeType.DigPond,
            "Dig(Clay)",
            data.LocationType.Pond,
            data.ItemType.Clay)
    End Sub
End Class
