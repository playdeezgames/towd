Friend Class DigGrassRecipeTypeDescriptor
    Inherits DigRecipeTypeDescriptor

    Public Sub New()
        MyBase.New(
            RecipeType.DigGrass,
            "Dig(Grubs)",
            data.LocationType.Grass,
            data.ItemType.Grub)
    End Sub
End Class
