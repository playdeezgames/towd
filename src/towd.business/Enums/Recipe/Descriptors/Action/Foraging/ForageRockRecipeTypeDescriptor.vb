Friend Class ForageRockRecipeTypeDescriptor
    Inherits ForageRecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            RecipeType.ForageRock,
            "Forage(Rocks)",
            data.LocationType.Rock,
            data.ItemType.Rock)
    End Sub
End Class
